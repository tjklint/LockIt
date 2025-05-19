// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Connects to Azure IoT Hub via Event Hub, streams live sensor data, handles network disconnects, and shows alerts on connectivity changes.

using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using LockIt.DataRepos;
using LockIt.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace LockIt.Services
{
    /// <summary>
    /// Handles connection to Azure IoT Hub through Event Hub, listens for live data updates,
    /// processes incoming messages, and handles reconnection on network status changes.
    /// </summary>
    public class HubService
    {
        private static readonly string EventHubConnectionString = "Endpoint=sb://ihsuprodyqres019dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=2hiXTvTbsjS9rO2VngSIY4V1WQap6xK8VAIoTMWlWcA=;EntityPath=iothub-ehub-joshuakrav-55394611-27e5154dcc";
        private static readonly string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=projectleshabitants;AccountKey=9Sbl22xiRCrtTsrSMikGMp2pYhQUV10kpUNFSRh6awfbNWM2rXft05BkGe/Q+RRyFuCihxHjqzjo+AStXixtLg==;EndpointSuffix=core.windows.net";
        private static readonly string BlobContainerName = "project";
        private static readonly string ConsumerGroup = "$Default";

        private readonly BlobContainerClient _storageClient;
        private EventProcessorClient _processor;
        private readonly MenuPageViewModel _viewModel;
        private bool _wasPreviouslyOnline = NetworkInterface.GetIsNetworkAvailable();

        /// <summary>
        /// Local data repository for storing processed sensor data.
        /// </summary>
        public UserDataRepo _repo;

        /// <summary>
        /// Initializes the HubService and hooks up network change events.
        /// </summary>
        /// <param name="repo">The data repository to update.</param>
        /// <param name="viewModel">The ViewModel to update UI bindings in real time.</param>
        public HubService(UserDataRepo repo, MenuPageViewModel viewModel)
        {
            _repo = repo;
            _viewModel = viewModel;

            _storageClient = new BlobContainerClient(StorageConnectionString, BlobContainerName);
            _processor = new EventProcessorClient(_storageClient, ConsumerGroup, EventHubConnectionString);
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;

            NetworkChange.NetworkAvailabilityChanged += OnNetworkAvailabilityChanged;
        }

        /// <summary>
        /// Begins listening for incoming events from Azure Event Hub.
        /// </summary>
        /// <returns>Task that runs the event loop.</returns>
        public async Task ProcessData()
        {
            try
            {
                await _processor.StartProcessingAsync();
                await Task.Delay(Timeout.Infinite);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR while processing: {e.Message}");
            }
        }

        /// <summary>
        /// Restarts the Event Hub processor.
        /// </summary>
        /// <returns>Task representing the restart process.</returns>
        public async Task RestartProcessing()
        {
            try
            {
                await _processor.StopProcessingAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error while stopping: {e.Message}");
            }

            try
            {
                _processor = new EventProcessorClient(_storageClient, ConsumerGroup, EventHubConnectionString);
                _processor.ProcessEventAsync += ProcessEventHandler;
                _processor.ProcessErrorAsync += ProcessErrorHandler;
                await _processor.StartProcessingAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error while restarting: {e.Message}");
            }
        }

        /// <summary>
        /// Handles network availability changes and shows alerts or restarts event processing accordingly.
        /// </summary>
        private void OnNetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (e.IsAvailable && !_wasPreviouslyOnline)
                {
                    _wasPreviouslyOnline = true;
                    await Shell.Current.DisplayAlert("Network", "Connection Restored", "OK");
                    await RestartProcessing();
                }
                else if (!e.IsAvailable && _wasPreviouslyOnline)
                {
                    _wasPreviouslyOnline = false;
                    await Shell.Current.DisplayAlert("Network", "Connection Lost", "OK");
                }
            });
        }

        /// <summary>
        /// Handles each incoming event, updates local data and UI bindings.
        /// </summary>
        /// <param name="args">Incoming Event Hub message.</param>
        /// <returns>A Task for async handling.</returns>
        public async Task ProcessEventHandler(ProcessEventArgs args)
        {
            try
            {
                JObject json = JObject.Parse(args.Data.EventBody.ToString());
                _repo.UpdateFromJson(json);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _viewModel.UpdateData(json);
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR while processing event: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs errors that occur during processing.
        /// </summary>
        /// <param name="args">Error arguments from Event Hub.</param>
        /// <returns>A Task for error processing.</returns>
        public async Task ProcessErrorHandler(ProcessErrorEventArgs args)
        {
            Debug.WriteLine($"ERROR: {args.Exception.Message}");
        }
    }
}
