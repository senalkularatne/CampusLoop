using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Share;
// Shows detailed event page after creation to a user

/* TODO: Add edit button for page owner
 *       Create popup page for choosing which social site to share to
    */

namespace UEApp
{
    public partial class EventView : ContentPage
    {
        // items source for the picker to change time before event to get a reminder
        public List<string> attend_status = new List<string>
        {
            "Going",
            "Undecided",
            "Can't Go"
        };

        public EventView()
        {
            InitializeComponent();

            this.Title = "Event";

            var share_action = new Command(() 
                => CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                // Change to event page url
                Text = "Share event page",
                Title = "Share"
            }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_share_white_36dp.png", Command = share_action });

            var Goto_MainPage = new Command(() => Navigation.PushModalAsync(new MainPage() { Title="Home" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_home_white_36dp.png", Command = Goto_MainPage });

            pickle.ItemsSource = attend_status;
        }

        private void pickle_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (pickle.SelectedIndex != -1)
            {
                Attend_Button.Text = pickle.Items[pickle.SelectedIndex];
            }
        }

        private void Attend_Button_Clicked(object sender, System.EventArgs e)
        {
            pickle.IsEnabled = true; pickle.Focus();
        }

        private void Attenders_Button_Clicked(object sender, System.EventArgs e)
        {
            DisplayAlert("Attenders button clicked", "Add popup window with list of attenders", "Cancel");
        }
    }
}