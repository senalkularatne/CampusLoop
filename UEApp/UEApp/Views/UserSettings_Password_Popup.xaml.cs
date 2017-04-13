using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

//User Settings accessed from settingspage

// TODO: Check against actual user password
//       Change actual user password
//       Give select amount of attempts to change pass
//       Tell user when pass successfully changed
//       Add reveal password toggle
//       Set more password reqs: password can't contain university or events, can't be in top 10k most used passwords

namespace UEApp
{
    public partial class UserSettings_Password_Popup : PopupPage
    {
        public UserSettings_Password_Popup()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        // Checks a lot of things to see if they are okay, if everything passes it pops the page
        private void OnSubmit(object sender, EventArgs e)
        {
            // Cleans out past warnings
            Warn_Layout.Children.Clear();

            // Checks if any of the fields are clear; if they are warns the user
            if (String.IsNullOrEmpty(CurPass.Text) || String.IsNullOrEmpty(NewPass.Text) || String.IsNullOrEmpty(ReNewPass.Text))
            {
                if (String.IsNullOrEmpty(CurPass.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter current password", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(NewPass.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter a new password", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(ReNewPass.Text))
                {
                    Label Warn_Label = new Label { Text = "Please confirm your new password", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                Warn_Layout.IsVisible = true;
                return;
            }

            if (CurPass_Check.IsVisible && NewPass_Check.IsVisible && ReNewPass_Check.IsVisible)
            {
                PopupNavigation.PopAsync();
                // Add web functionality
                return;
            }

            // Makes sure all fields are valid; if not warns the user
            if (CurPass_X.IsVisible)
            {
                Label Cur_Warn_Label = new Label { Text = "Current password incorrect", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Cur_Warn_Label);
            }
            if (!(Pass_Strength_Check(NewPass.Text)) && NewPass_X.IsVisible)
            {
                Label New_Warn_Label1 = new Label { Text = "New password must be at least 10 characters long", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(New_Warn_Label1);
            }
            if (CurPass.Text.Equals(NewPass.Text) && NewPass_X.IsVisible)
            {
                Label New_Warn_Label2 = new Label { Text = "New password cannot match the old password", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(New_Warn_Label2);
            }
            if (ReNewPass_X.IsVisible)
            {
                Label ReNew_Warn_Label = new Label { Text = "Password confirmation does not match", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(ReNew_Warn_Label);
            }

            Warn_Layout.IsVisible = true;
        }

        // Simply makes sure the new password is 10 chars long
        private bool Pass_Strength_Check(String pass)
        {
            if (String.IsNullOrEmpty(pass))
            {
                return false;
            }
            else if (pass.Length < 10)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // Gives image feedback based on if valid
        private void CurPassword_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            // TODO: check against actual password
            String testPass = "DickTracy007";

            if (String.IsNullOrEmpty(CurPass.Text))
            {
                CurPass_X.IsVisible = false;
                CurPass_Check.IsVisible = false;
            }
            else if (CurPass.Text.Equals(testPass))
            {
                CurPass_X.IsVisible = false;
                CurPass_Check.IsVisible = true;
            }
            else
            {
                CurPass_Check.IsVisible = false;
                CurPass_X.IsVisible = true;
            }
        }

        // Gives image feedback based on new password validity
        private void NewPassword_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(NewPass.Text))
            {
                NewPass_X.IsVisible = false;
                NewPass_Check.IsVisible = false;
            }
            else if (Pass_Strength_Check(NewPass.Text) && !CurPass.Text.Equals(NewPass.Text))
            {
                NewPass_X.IsVisible = false;
                NewPass_Check.IsVisible = true;
                Advise_Label.IsVisible = false;
            }
            else
            {
                NewPass_Check.IsVisible = false;
                NewPass_X.IsVisible = true;
                Advise_Label.IsVisible = true;
            }
            if (!(String.IsNullOrEmpty(ReNewPass.Text)))
            {
                if (NewPass.Text.Equals(ReNewPass.Text))
                {
                    ReNewPass_X.IsVisible = false;
                    ReNewPass_Check.IsVisible = true;
                }
                else
                {
                    ReNewPass_Check.IsVisible = false;
                    ReNewPass_X.IsVisible = true;
                }
            }
        }

        // Gives image feedback based on confirmation password validity
        private void ReNewPassword_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(ReNewPass.Text))
            {
                ReNewPass_X.IsVisible = false;
                ReNewPass_Check.IsVisible = false;
            }
            else if (NewPass.Text.Equals(ReNewPass.Text))
            {
                ReNewPass_X.IsVisible = false;
                ReNewPass_Check.IsVisible = true;
            }
            else
            {
                ReNewPass_Check.IsVisible = false;
                ReNewPass_X.IsVisible = true;
            }
        }
    }
}