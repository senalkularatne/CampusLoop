using System;
using System.Collections.Generic;
using Xamarin.Forms;

/* User Settings accessed from settingspage
 */

namespace UEApp
{
    public partial class Settings_Notif : ContentPage
    {
        // items source for the picker to change time before event to get a reminder
        public List<string> times = new List<string>
        {
            "5",
            "10",
            "15",
            "30",
            "45",
            "60",
            "120"
        };

        public Settings_Notif()
        {
            InitializeComponent();

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });

            pickle.ItemsSource = times;            
        }

        // Called when page loads
        protected override void OnAppearing()
        {
            if (ReminderSwitch.IsToggled)
            {
                pickle.IsVisible = true;
            }
            else
            {
                pickle.IsVisible = false;
            }
        }



        private void All_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (AllSwitch.IsToggled)
            {
                InviteSwitch.IsToggled = true;
                ReminderSwitch.IsToggled = true;
            }
            else
            {
                InviteSwitch.IsToggled = false;
                ReminderSwitch.IsToggled = false;
            }
        }

        private void Invite_Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }

        private void Reminder_Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (ReminderSwitch.IsToggled)
            {
                pickle.IsVisible = true;
            }
            else
            {
                pickle.IsVisible = false;
            }
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}