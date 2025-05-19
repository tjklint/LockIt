// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Connects to Azure IoT Hub via Event Hub to process and stream IoT device data into the application.

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
    /// Handles the connection and data processing from Azure IoT Hub via Event Hub.
    /// Updates the UI and local data repository with live sensor readings.
    /// </summary>
    public class HubService
    {
        private static string EventHubConnectionString = "Endpoint=sb://ihsuprodyqres019dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=2hiXTvTbsjS9rO2VngSIY4V1WQap6xK8VAIoTMWlWcA=;EntityPath=iothub-ehub-joshuakrav-55394611-27e5154dcc";
        private static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=projectleshabitants;AccountKey=9Sbl22xiRCrtTsrSMikGMp2pYhQUV10kpUNFSRh6awfbNWM2rXft05BkGe/Q+RRyFuCihxHjqzjo+AStXixtLg==;EndpointSuffix=core.windows.net";
        private static string BlobContainerName = "project";
        private static string EventHubName = "joshuakravitz-iot-hub";
        private static string ConsumerGroup = "$Default";

        private readonly BlobContainerClient _storageClient;
        private EventProcessorClient _processor;
        private readonly MenuPageViewModel _viewModel;
        private CancellationTokenSource _cts = new();
        private bool _isProcessing = false;

        public UserDataRepo _repo;

        public HubService(UserDataRepo repo, MenuPageViewModel viewModel)
        {
            _repo = repo;
            _viewModel = viewModel;
            _storageClient = new BlobContainerClient(StorageConnectionString, BlobContainerName);

            InitializeProcessor();
            NetworkChange.NetworkAvailabilityChanged += NetworkAvailabilityChanged;
        }

        private void InitializeProcessor()
        {
            _processor = new EventProcessorClient(_storageClient, ConsumerGroup, EventHubConnectionString);
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;
        }

        /// <summary>
        /// Starts processing data and monitors connectivity.
        /// </summary>
        public async Task ProcessData()
        {
            if (_isProcessing) return;

            try
            {
                _isProcessing = true;
                await _processor.StartProcessingAsync(_cts.Token);
                await Task.Delay(Timeout.Infinite, _cts.Token);
            }
            catch (TaskCanceledException)
            {
                Debug.WriteLine("Processor cancelled.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during processing: {ex.Message}");
            }
            finally
            {
                _isProcessing = false;
                await _processor.StopProcessingAsync();
            }
        }

        /// <summary>
        /// Handles network changes and restarts processing on reconnect.
        /// </summary>
        private async void NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            if (e.IsAvailable && !_isProcessing)
            {
                Debug.WriteLine("Network available. Attempting to restart Event Hub processor...");
                _cts = new();
                InitializeProcessor();
                await ProcessData();
            }
        }

        /// <summary>
        /// Parses Event Hub messages and updates app state.
        /// </summary>
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
            catch (Exception e)
            {
                Debug.WriteLine($"Event processing error: {e.Message}");
            }
        }

        /// <summary>
        /// Logs errors during event processing.
        /// </summary>
        public async Task ProcessErrorHandler(ProcessErrorEventArgs args)
        {
            Debug.WriteLine($"Event Hub error: {args.Exception.Message}");
        }
    }
}