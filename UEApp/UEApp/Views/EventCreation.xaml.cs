using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Plugin.Media;

// Popup page for creating a new event, accessed through plus button on main menu
// TODO: Store the file on the db
//       Add extended entry for an about

namespace UEApp
{
    public partial class EventCreation : PopupPage
    {
        EventsDataModel dm;

        public EventCreation()
        {
            InitializeComponent();

            this.BindingContext = dm = new EventsDataModel();

            timepickle.Time = new TimeSpan(DateTime.Now.Hour + 1, 0, 0);
        }

        private async void OnImageSelect(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Photo Picker", "No Pick photo available :(", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null) return;

            //TODO: Store the file on the db here

            // Not working atm, not sure if it will be needed
            //var bannerPath = await Plugin.CropImage.CrossCropImage.Current.SmartCrop(file.Path, 100, 200, "-banner-100x200");
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        // Checks a lot of things to see if they are okay, if everything passes it pops the page
        private void OnSubmit(object sender, EventArgs e)
        {
            int flag = 0;
            
            // Cleans out past warnings
            Warn_Layout.Children.Clear();

            // Checks if any of the fields are empty; if they are warns the user
            if (String.IsNullOrEmpty(Event_Title.Text) || String.IsNullOrEmpty(Event_Place.Text))
            {
                if (String.IsNullOrEmpty(Event_Title.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter a Title for the event", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(Event_Place.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter an Place for the event", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                flag = 1;
            }

            // Checks to make sure the date and time picked are in the future
            if (DateTime.Now.Day.CompareTo(datepickle.Date.Day) == 0)
            {
                if (timepickle.Time.Hours < DateTime.Now.Hour)
                {
                    Label Warn_Label = new Label { Text = "Please enter a time in the future", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                    flag = 1;
                }
                else if ((timepickle.Time.Hours == DateTime.Now.Hour) && (timepickle.Time.Minutes < DateTime.Now.Minute))
                {
                    Label Warn_Label = new Label { Text = "Please enter a time in the future", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                    flag = 1;
                }
            }

            // Checks to see if any tags are dupes, if any found it gets rid of em
            if (pickle0.SelectedIndex == pickle1.SelectedIndex || pickle0.SelectedIndex == pickle2.SelectedIndex ||
                pickle1.SelectedIndex == pickle2.SelectedIndex)
            {
                if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle0.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle1.SelectedIndex = -1;
                    pickle2.SelectedIndex = -1;
                    pickle2.IsVisible = false;
                }
                else if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle2.SelectedIndex == -1)
                {
                    pickle1.SelectedIndex = -1;
                    pickle2.IsVisible = false;
                }
                else if (pickle0.SelectedIndex == pickle1.SelectedIndex && pickle2.SelectedIndex != -1)
                {
                    pickle1.SelectedIndex = pickle2.SelectedIndex;
                    pickle2.SelectedIndex = -1;
                }
                else if (pickle0.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle2.SelectedIndex = -1;
                }
                else if (pickle1.SelectedIndex == pickle2.SelectedIndex)
                {
                    pickle2.SelectedIndex = -1;
                }
            }

            if(flag == 1)
            {
                Warn_Layout.IsVisible = true;
            }
            else
            {
                PopupNavigation.PopAsync();
            }
        }

        private void CategoryPicked(object sender, System.EventArgs e)
        {
            Picker pick = (Picker)sender;

            if (pick.Title.Contains("Tag #1") && pickle1.IsVisible == false)
            {
                pickle1.IsVisible = true;
            }

            if (pick.Title.Contains("Tag #2") && pickle2.IsVisible == false)
            {
                pickle2.IsVisible = true;
            }
        }
    }
}