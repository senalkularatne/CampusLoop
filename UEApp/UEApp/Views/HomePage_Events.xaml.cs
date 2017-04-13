using System;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;

/* Main homepage of the app, first tab
   Shows the feed of most recent/upcoming events
 */

namespace UEApp
{
    public partial class HomePage_Events : ContentPage
    {
        public HomePage_Events()
        {
            InitializeComponent();
            // Sets the item source for the page as the events data
            list.ItemsSource = UEApp.EventsDataModel.All;

            // Adds search icon to toolbar and command to go to its page
            // PushModalASync gets rid of the tab and nav bar
            //var Goto_CreationPage = new Command(() => Navigation.PushPopupAsync(new EventCreation()));
            //this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_add_circle_outline_white_36dp.png", Command = Goto_CreationPage });

            var Goto_SettingsPage = new Command(() => Navigation.PushModalAsync(new NavigationPage(new SettingsPage()) { Title="Settings" }));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_settings_white_36dp.png", Command = Goto_SettingsPage });

            var Goto_SearchPage = new Command(() => Navigation.PushModalAsync(new SearchPage()));
            this.ToolbarItems.Add(new ToolbarItem { Icon = "ic_search_white_36dp.png", Command = Goto_SearchPage });

            /*
            var layout = new RelativeLayout();

            var normalFab = new FAB.Forms.FloatingActionButton();
            normalFab.Source = "ic_add_circle.png";
            normalFab.Size = FabSize.Normal;

            layout.Children.Add(
                normalFab,
                xConstraint: Constraint.RelativeToParent((parent) => { return (parent.Width - normalFab.Width) - 16; }),
                yConstraint: Constraint.RelativeToParent((parent) => { return (parent.Height - normalFab.Height) - 16; })
            );

            normalFab.SizeChanged += (sender, args) => { layout.ForceLayout(); };
        */
             }

        // Custom function for displaying different background colors on alternating cells
        private bool isRowEven;
        private void Alt_Cell_Colors(object sender, EventArgs e)
        {
            if (this.isRowEven)
            {
                var viewCell = (ViewCell)sender;
                if (viewCell.View != null)
                {
                    viewCell.View.BackgroundColor = Color.FromHex("F5F5F5");
                }
            }
            this.isRowEven = !this.isRowEven;
        }

        // Called when page loads
        protected override void OnAppearing()
        {
            // Clears last page for memory after home button has been pressed
            Navigation.PopModalAsync();
        }

        // Using this temporarily to look at the eventview page
        /*This can be used to have an item in a list as clicked
        * Add to listview propertied ItemSelected="OnItemSelected" */
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return; // has been set to null, do not 'process' tapped event
            Navigation.PushModalAsync(new NavigationPage(new EventView()) { Title="Event" });
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        /* This can be used to set specific areas of a cell as clicked
        public void OnCellClicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var t = b.CommandParameter;
            ((ContentPage)((ListView)((StackLayout)b.ParentView).ParentView).ParentView).DisplayAlert("Clicked", t + " button was clicked", "OK");
            Debug.WriteLine("clicked" + t);
        }*/

        void Handle_FabClicked(object sender, System.EventArgs e)
        {
            Navigation.PushPopupAsync(new EventCreation());
        }
    }
}