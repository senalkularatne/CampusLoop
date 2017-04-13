using Xamarin.Forms;

// Search page
// TODO: Complete redo

namespace UEApp
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchButtonPressed(object sender, System.EventArgs e)
        {
            resultsLab.IsVisible = true;
            resultsLab.Text = sBar.Text + " was searched.";
        }
    }
}