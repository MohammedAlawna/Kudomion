using Kudomion.FirebaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class UserSettings : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        public UserSettings()
        {
            InitializeComponent();
        }

        private async void SubmitButton(object sender, EventArgs e)
        {
            try
            {
                if (AreEntriesEmpty() == true) return;
            
            User getCurrentUser = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);

                //Assign Current User To Top.
                currentUser.Text = LoginPage.currentLoggedInUser;

            //Assign New Values To The Current Logged In User.
            User userToBeUpdated = new User {
            name = nUser.Text,
            password = nPass.Text,
            points = getCurrentUser.points,
            posts = getCurrentUser.posts,
            duels = getCurrentUser.duels,
            usertype = getCurrentUser.usertype,
            //TODO Email.
            //TODO ImageSrc.
            };

            //Update User Via Firebase.
            await firebase.UpdateUser(LoginPage.currentLoggedInUser, userToBeUpdated);

            //Update Current Logged In User Name.
            LoginPage.currentLoggedInUser = nUser.Text;

            //Update User Profile.
            Home.loggedInUsername.Text = nUser.Text;
            currentUser.Text = nUser.Text;

            //Reset Values
            nUser.Text = String.Empty;
            nEmail.Text = String.Empty;
            nPass.Text = String.Empty;
            nPassConfirm.Text = String.Empty;


                //Display Alert
                await DisplayAlert("User Updated!", "User Information changed successfully.", "OK!");
            }
            catch(NullReferenceException er)
            {
                await DisplayAlert("Error!", $"An Error Occured.. Missing Info..{er}", "OK!");
            }
        }

        bool AreEntriesEmpty()
        {
            if (string.IsNullOrEmpty(nUser.Text))
            {
                DisplayAlert("Missing Credentials!", "Please Enter The New Username..", "OK!");
                return true;
            }

            if(string.IsNullOrEmpty(nPass.Text))
            {
                DisplayAlert("Missing Credentials!", "Please Enter The New Password..", "OK!");
                return true;
            }

            if(string.IsNullOrEmpty(nPassConfirm.Text))
            {
                DisplayAlert("Missing Credentials!", "Please Enter The New Password Confirmation..", "OK!");
                return true;
            }

            return false;
            /* TODO:: When Email Authentication Implemented In The Project.
            if(nEmail.Text == "")
            {
                DisplayAlert("Missing Credentials!", "Please Enter The New Email..", "OK!");
                return;
            }*/
        }

        private void OnClickAvatar(object sender, EventArgs e)
        {
            DisplayAlert("Feature Not Available", "You cant change your profile picture now. Feature will be available in futural release.", "OK!");
        }
    }
}