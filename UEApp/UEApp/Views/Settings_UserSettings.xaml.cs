using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

// Settings for the user like changing password/email
 
namespace UEApp
{
    public partial class Settings_UserSettings : ContentPage
    {
        public Settings_UserSettings()
        {
            InitializeComponent();

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { Title = "Home" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });
        }

        private async void Email_Button_Clicked(object sender, EventArgs e)
        {
            var email_change_page = new UserSettings_Email_Popup();
            await Navigation.PushPopupAsync(email_change_page);
        }

        private async void Password_Button_Clicked(object sender, EventArgs e)
        {
            var password_change_page = new UserSettings_Password_Popup();
            await Navigation.PushPopupAsync(password_change_page);
        }
    }
}