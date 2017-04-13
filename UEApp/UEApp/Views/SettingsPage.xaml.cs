using System;
using Xamarin.Forms;

/* Settings page accessed from toolbar
 * 
 * TODO: Fix renderer for back button in this stack hierarchy
 *       Fill in webpage for user manual
 *       Relate notification and location toggles to real events
 */      

namespace UEApp
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            this.Title = "Settings";

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { Title="Home" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });
        }

        private void User_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings_UserSettings() { Title = "User Settings" });
        }

        private void Notif_Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Settings_Notif() { Title = "Notifications" });
        }

        private void Location_Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Manual_Button_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}