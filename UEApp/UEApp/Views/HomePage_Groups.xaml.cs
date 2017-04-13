using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

// Filler page

namespace UEApp
{
    public partial class HomePage_Groups : ContentPage
    {
        public HomePage_Groups()
        {
            InitializeComponent();

            // Adds search icon to toolbar and command to go to its page
            // PushModalASync gets rid of the tab and nav bar
            var Goto_CreationPage = new Command(() => Navigation.PushPopupAsync(new EventCreation()));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_add_circle_outline_white_36dp.png", Command = Goto_CreationPage });

            var Goto_SettingsPage = new Command(() => Navigation.PushModalAsync(new NavigationPage(new SettingsPage()) { Title = "Settings" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_settings_white_36dp.png", Command = Goto_SettingsPage });

            var Goto_SearchPage = new Command(() => Navigation.PushModalAsync(new SearchPage()));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_search_white_36dp.png", Command = Goto_SearchPage });
        }
    }
}
