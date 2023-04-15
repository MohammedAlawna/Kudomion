using Kudomion.FirebaseManager;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public static string currentLoggedInUser;
        FirebaseHelper firebase = new FirebaseHelper();

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            // firebase.GetUserByName(Home.GetLoggedInUser().Result.name);
            
        }

        public string GetUserName()
        {
            return userNameText.Text;
        }

        public string GetUserPassword()
        {
            return passwordText.Text;
        }

        private async void SignInClicked(object sender, EventArgs e)
        {
            try
            {

                currentLoggedInUser = userNameText.Text;
            var outPut = await FirebaseHelper.GetUsrFromName(userNameText.Text);
            Console.WriteLine("This OUTPUT!!" + outPut.name);

                if (userNameText.Text == outPut.name && passwordText.Text == outPut.password)
                {
                    await Navigation.PushAsync(new LoggedInPage());
                    currentLoggedInUser = outPut.name;
                    return;

                }

                if (outPut == null)
                {

                    await DisplayAlert("User Not Found!", "No such user exist!", "OK!");
                    return;
                }


            
            else
            {
              
               await DisplayAlert("Wrong Credentials!", "Either Password or Useranme incorrect!", "OK!");
                    return;
                }
               
            } catch(NullReferenceException n)
            {
                await DisplayAlert("Exception!", "Null Reference Exception caught!", "OK!");
                return;
            }
          
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            
           
        }


     

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        
    }
}