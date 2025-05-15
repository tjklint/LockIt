using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Storage.Blobs;
using LockIt.DataRepos;
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
                JObject json = JObject.Parse(args.Data.EventBody.ToString());
                UserDataRepo.UpdateFromJson(json);
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
