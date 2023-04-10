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
            User getCurrentUser = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);

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

            //Reset Values
            nUser.Text = String.Empty;
            nEmail.Text = String.Empty;
            nPass.Text = String.Empty;
            nPassConfirm.Text = String.Empty;

            //Display Alert
            await DisplayAlert("User Updated!", "User Information changed successfully.", "OK!");

            return;
        }
    }
}