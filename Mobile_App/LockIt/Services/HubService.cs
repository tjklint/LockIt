using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace LockIt.Services
{
    public static class HubService
    {
        private static string EventHubConnectionString = "Endpoint=sb://ihsuprodyqres019dednamespace.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=2hiXTvTbsjS9rO2VngSIY4V1WQap6xK8VAIoTMWlWcA=;EntityPath=iothub-ehub-joshuakrav-55394611-27e5154dcc";
        private static string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=projectleshabitants;AccountKey=9Sbl22xiRCrtTsrSMikGMp2pYhQUV10kpUNFSRh6awfbNWM2rXft05BkGe/Q+RRyFuCihxHjqzjo+AStXixtLg==;EndpointSuffix=core.windows.net";
        private static string BlobContainerName = "project";
        private static string EventHubName = "joshuakravitz-iot-hub";
        private static string ConsumerGroup = "$Default";

        public static BlobContainerClient _storageClient = new BlobContainerClient(StorageConnectionString, BlobContainerName);
        private static EventProcessorClient _processor = new EventProcessorClient(_storageClient, ConsumerGroup, EventHubConnectionString);
        

        public static async Task ProcessData()
        {
            _processor.ProcessEventAsync += ProcessEventHandler;
            _processor.ProcessErrorAsync += ProcessErrorHandler;
            try
            {
                await _processor.StartProcessingAsync();
                // The processor performs its work in the background;

                // to allow processing to take place.

                await Task.Delay(Timeout.Infinite);
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
                // To prevent leaks, the handlers should be removed when processing is complete.

                _processor.ProcessEventAsync -= ProcessEventHandler;
                _processor.ProcessErrorAsync -= ProcessErrorHandler;
            }
        }
        public static async Task ProcessEventHandler(ProcessEventArgs args)
        {
            try
            {
                Console.WriteLine(args.Data.EventBody);
                JObject json = JObject.Parse(args.Data.EventBody.ToString());
                string measurement = json["measurement"]?.ToString();
                var value = json["value"];
                Console.WriteLine("Measurement: " + measurement + "Value: " + value);
                // This is where the repo parsing the data should come into place. 
                // or a helper method which helps route the data to the appropriate repo.
            }
            catch (Exception e)
            {
                Debug.WriteLine($"ERROR while processing event: {e.Message}");
            }
        }

        public static async Task ProcessErrorHandler(ProcessErrorEventArgs args)
        {
            Debug.WriteLine($"ERROR: {args}");
        }
    }
}
