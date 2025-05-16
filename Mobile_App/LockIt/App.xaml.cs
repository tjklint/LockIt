using LockIt.DataRepos;
using LockIt.Services;
using LockIt.Views;

namespace LockIt
{
    public partial class App : Application
    {
        public HubService _hubService { get; set; }
        public App(HubService hubService)
        {
            InitializeComponent();

            MainPage = new AppShell();
            
            _hubService = hubService;
         
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                await Shell.Current.GoToAsync(nameof(RegisterPage));
            });

        }
        protected override void OnStart()
        {
            base.OnStart();

            Task.Run(async () => await _hubService.ProcessData());
        }
    }
}
