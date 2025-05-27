using Microsoft.Azure.Devices;
using System.Text.Json;
 
public class LockControlService
{
    private readonly string _iotHubConnectionString;
 
    public LockControlService(string iotHubConnectionString)
    {
        _iotHubConnectionString = iotHubConnectionString;
    }
 
    public async Task<bool> ToggleLockAsync(string deviceId, bool unlock)
    {
        using var serviceClient = ServiceClient.CreateFromConnectionString(_iotHubConnectionString);
 
        var methodInvocation = new CloudToDeviceMethod("toggle_lock")
        {
            ResponseTimeout = TimeSpan.FromSeconds(10)
        };
 
        methodInvocation.SetPayloadJson(JsonSerializer.Serialize(new
        {
            value = unlock ? 1 : 0
        }));
 
        var response = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
        return response.Status == 200;
    }
}