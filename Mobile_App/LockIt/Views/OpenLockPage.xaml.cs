using System.Text.Json;
using LockIt.Helpers;
using Microsoft.Azure.Devices;

namespace LockIt.Views;

public partial class OpenLockPage : ContentPage
{
    private bool isLocked = true;
    private string deviceId;
    private string methodName;
    private string connectionString;

    public OpenLockPage()
    {
        InitializeComponent();
        LoadIoTSettings();
    }

    private void LoadIoTSettings()
    {
        var root = AppSettingsLoader.Load();
        var iotSection = root.GetProperty("IoTHub");

        connectionString = iotSection.GetProperty("DeviceConnectionString").GetString();
        deviceId = iotSection.GetProperty("DeviceId").GetString();
        methodName = iotSection.GetProperty("MethodName").GetString();
    }

    private async void OnLockControlClicked(object sender, EventArgs e)
    {
        try
        {
            using var client = ServiceClient.CreateFromConnectionString(connectionString);
            var payload = new { action = "LOCK", value = isLocked ? 0 : 1 };
            var cloudToDeviceMethod = new CloudToDeviceMethod(methodName)
            {
                ResponseTimeout = TimeSpan.FromSeconds(10)
            };
            cloudToDeviceMethod.SetPayloadJson(JsonSerializer.Serialize(payload));

            var response = await client.InvokeDeviceMethodAsync(deviceId, cloudToDeviceMethod);

            isLocked = !isLocked;
            LockControlButton.Text = isLocked ? "Unlock" : "Lock";

            await DisplayAlert("Success", $"Command sent: {response.Status}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}