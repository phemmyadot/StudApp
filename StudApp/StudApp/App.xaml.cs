using FreshMvvm;
using StudApp.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudApp
{
    public partial class App : Application
    {
        public App()
        {

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppConstant.Constants.SyncfusionLicense);
            InitializeComponent();

            Page page = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            FreshNavigationContainer basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
