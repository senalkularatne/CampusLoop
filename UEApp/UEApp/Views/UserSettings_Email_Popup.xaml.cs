using System;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

// Email change form accessed from settingspage/usersettings/changeemail

// TODO: Check against actual user email
//       Tell user when email successfully changed

namespace UEApp
{
    public partial class UserSettings_Email_Popup : PopupPage
    {
        public UserSettings_Email_Popup()
        {
            InitializeComponent();
        }

        private void OnCancel(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }

        // Checks a lot of things to see if they are okay, if everything validates it pops the page
        private void OnSubmit(object sender, EventArgs e)
        {
            // Cleans out past warnings
            Warn_Layout.Children.Clear();

            // Checks if any of the fields are empty; if they are warns the user
            if (String.IsNullOrEmpty(CurEmail.Text) || String.IsNullOrEmpty(NewEmail.Text) || String.IsNullOrEmpty(ReNewEmail.Text))
            {
                if (String.IsNullOrEmpty(CurEmail.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter current Email", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(NewEmail.Text))
                {
                    Label Warn_Label = new Label { Text = "Please enter a new Email", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                if (String.IsNullOrEmpty(ReNewEmail.Text))
                {
                    Label Warn_Label = new Label { Text = "Please confirm your new Email", HorizontalOptions = LayoutOptions.CenterAndExpand };
                    Warn_Layout.Children.Add(Warn_Label);
                }
                Warn_Layout.IsVisible = true;
                return;
            }

            if (CurEmail_Check.IsVisible && NewEmail_Check.IsVisible && ReNewEmail_Check.IsVisible)
            {
                PopupNavigation.PopAsync();
                //TODO: Add web functionality
                return;
            }

            // Makes sure all fields are valid; if not warns the user
            if (CurEmail_X.IsVisible)
            {
                Label Cur_Warn_Label = new Label { Text = "Current email incorrect", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(Cur_Warn_Label);
            }
            if (!(Email_Address_Check(NewEmail.Text)) && NewEmail_X.IsVisible)
            {
                Label New_Warn_Label1 = new Label { Text = "New email invalid", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(New_Warn_Label1);
            }
            if (CurEmail.Text.ToLower().Equals(NewEmail.Text.ToLower()) && NewEmail_X.IsVisible)
            {
                Label New_Warn_Label2 = new Label { Text = "New email cannot match the old email", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(New_Warn_Label2);
            }
            if (ReNewEmail_X.IsVisible)
            {
                Label ReNew_Warn_Label = new Label { Text = "Email confirmation does not match", HorizontalOptions = LayoutOptions.CenterAndExpand };
                Warn_Layout.Children.Add(ReNew_Warn_Label);
            }

            Warn_Layout.IsVisible = true;
        }

        // Makes sure the email doesn't contain many '@' and is a uc email address
        private bool Email_Address_Check(String email_adr)
        {
            if (String.IsNullOrEmpty(email_adr))
            {
                return false;
            }
            else if (email_adr.Contains("@"))
            {
                email_adr = email_adr.ToLower();
                String[] email_adr_split = email_adr.Split('@');
                if (email_adr_split[1].Contains("@"))
                {
                    return false;
                }
                else if (String.IsNullOrEmpty(email_adr_split[1]))
                {
                    return false;
                }
                else if (email_adr_split[1].Equals("mail.uc.edu"))
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        // Gives image feedback based on if valid
        private void CurEmail_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            // TODO: check against actual Email
            String testEmail = "donkey@mail.uc.edu";

            if (String.IsNullOrEmpty(CurEmail.Text))
            {
                CurEmail_X.IsVisible = false;
                CurEmail_Check.IsVisible = false;
            }
            else if (CurEmail.Text.ToLower().Equals(testEmail))
            {
                CurEmail_X.IsVisible = false;
                CurEmail_Check.IsVisible = true;
            }
            else
            {
                CurEmail_Check.IsVisible = false;
                CurEmail_X.IsVisible = true;
            }
        }

        // Gives image feedback based on new email validity
        private void NewEmail_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(NewEmail.Text))
            {
                NewEmail_X.IsVisible = false;
                NewEmail_Check.IsVisible = false;
            }
            else if (Email_Address_Check(NewEmail.Text) && !(CurEmail.Text.ToLower().Equals(NewEmail.Text.ToLower())) )
            {
                NewEmail_X.IsVisible = false;
                NewEmail_Check.IsVisible = true;
                Advise_Label.IsVisible = false;
            }
            else
            {
                NewEmail_Check.IsVisible = false;
                NewEmail_X.IsVisible = true;
                Advise_Label.IsVisible = true;
            }
            if (!(String.IsNullOrEmpty(ReNewEmail.Text)))
            {
                if (NewEmail.Text.ToLower().Equals(ReNewEmail.Text.ToLower()))
                {
                    ReNewEmail_X.IsVisible = false;
                    ReNewEmail_Check.IsVisible = true;
                }
                else
                {
                    ReNewEmail_Check.IsVisible = false;
                    ReNewEmail_X.IsVisible = true;
                }
            }
        }

        // Gives image feedback based on confirmation email validity
        private void ReNewEmail_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(ReNewEmail.Text))
            {
                ReNewEmail_X.IsVisible = false;
                ReNewEmail_Check.IsVisible = false;
            }
            else if (NewEmail.Text.ToLower().Equals(ReNewEmail.Text.ToLower()))
            {
                ReNewEmail_X.IsVisible = false;
                ReNewEmail_Check.IsVisible = true;
            }
            else
            {
                ReNewEmail_Check.IsVisible = false;
                ReNewEmail_X.IsVisible = true;
            }
        }
    }
}