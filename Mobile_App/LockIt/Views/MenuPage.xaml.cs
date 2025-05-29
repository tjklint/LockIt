// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Controls the main homeowner menu page, allowing navigation to lock settings and visitor management.

using LockIt.Helpers;
using Microsoft.Azure.Devices;
using System.Reflection;

namespace LockIt.Views
{
    /// <summary>
    /// Represents the main homeowner menu page that provides navigation to lock and visitor management settings.
    /// </summary>
    public partial class MenuPage : ContentPage
    {
        private string deviceId;
        private string methodName;
        private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPage"/> class and sets up the UI components.
        /// </summary>
        public MenuPage()
        {
            InitializeComponent();
            LoadIoTSettings();
        }

        /// <summary>
        /// Handles the event when the "Set Lock" button is clicked.
        /// Navigates to the <see cref="SetLockPage"/>.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnSetLockedClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SetLockPage));
        }

        /// <summary>
        /// Handles the event when the "Manage Visitor" button is clicked.
        /// Navigates to the <see cref="ManageVisitor"/> page.
        /// </summary>
        /// <param name="sender">The button that was clicked.</param>
        /// <param name="e">The event data.</param>
        private async void OnManageVisitorClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ManageVisitor));
        }

        private void LoadIoTSettings()
        {
            var root = AppSettingsLoader.Load();
            var iotSection = root.GetProperty("IoTHub");

            connectionString = iotSection.GetProperty("DeviceConnectionString").GetString();
            deviceId = iotSection.GetProperty("DeviceId").GetString();
            methodName = iotSection.GetProperty("MethodName").GetString();
        }

        private async void CheckOnlineClicked(object sender, EventArgs e)
        {
            //try
            //{
            //    using var client = ServiceClient.CreateFromConnectionString(connectionString);
            //    var cloudToDeviceMethod = new CloudToDeviceMethod("is_online")
            //    {
            //        ResponseTimeout = TimeSpan.FromSeconds(10)
            //    };

            //    var response = await client.InvokeDeviceMethodAsync(deviceId, cloudToDeviceMethod);

            //    if (response.Status == 200)
            //    {
            //        await DisplayAlert("Device Status", "Your IoT device is online and connected.", "OK");
            //    }
            //    else
            //    {
            //        await DisplayAlert("Device Status", $"Unexpected response: {response.Status}", "OK");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await DisplayAlert("Error", $"Failed to check device status: {ex.Message}", "OK");
            //}
        }

    }
}
