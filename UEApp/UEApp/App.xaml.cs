using Xamarin.Forms;

// Iniatilizer of the app, not a page

namespace UEApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Sets the root page of the app
            MainPage = new UEApp.MainPage();
        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}