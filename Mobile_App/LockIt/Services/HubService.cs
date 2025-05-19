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
        private readonly EventProcessorClient _processor;
        private readonly MenuPageViewModel _viewModel;

        /// <summary>
        /// Local repository for updating application data.
        /// </summary>
        public UserDataRepo _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="HubService"/> class and sets up event handlers.
        /// </summary>
        /// <param name="repo">The repository to update with parsed data.</param>
        /// <param name="viewModel">The view model to update the UI in real-time.</param>
        public HubService(UserDataRepo repo, MenuPageViewModel viewModel)
        {
            _repo = repo;
            _viewModel = viewModel;
            _storageClient = new BlobContainerClient(StorageConnectionString, BlobContainerName);
            _processor = new EventProcessorClient(_storageClient, ConsumerGroup, EventHubConnectionString);
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;
        }

        /// <summary>
        /// Starts processing data from the Event Hub and handles cleanup on termination.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ProcessData()
        {
            try
            {
                await _processor.StartProcessingAsync();
                await Task.Delay(Timeout.Infinite); // Keeps processor running
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR while processing event: {e.Message}");
            }

            try
            {
                await _processor.StopProcessingAsync();
            }
            finally
            {
                _processor.ProcessEventAsync -= ProcessEventHandler;
                _processor.ProcessErrorAsync -= ProcessErrorHandler;
            }
        }

        /// <summary>
        /// Handles incoming messages from the Event Hub, parses them, and updates local state.
        /// </summary>
        /// <param name="args">Event data received from Event Hub.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Logs any parsing or update exceptions to Debug.</exception>
        public async Task ProcessEventHandler(ProcessEventArgs args)
        {
            try
            {
                Console.WriteLine(args.Data.EventBody);
                JObject json = JObject.Parse(args.Data.EventBody.ToString());
                _repo.UpdateFromJson(json);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _viewModel.UpdateData(json);
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR while processing event: {e.Message}");
            }
        }

        /// <summary>
        /// Handles any errors that occur during Event Hub processing.
        /// </summary>
        /// <param name="args">Error event arguments.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ProcessErrorHandler(ProcessErrorEventArgs args)
        {
            Debug.WriteLine($"ERROR: {args}");
        }
    }
}
