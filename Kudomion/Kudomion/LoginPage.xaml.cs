using Kudomion.FirebaseManager;
using SQLite;
using System;
using System.Collections.Generic;
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
            var outPut = await FirebaseHelper.GetUsrFromName(userNameText.Text);
            Console.WriteLine("This OUTPUT!!" + outPut.name);
        }

        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            
            var GetUser = firebase.GetUserByName(userNameText.Text);
            Console.WriteLine("Specfified User: " + GetUser.Result);
            //var checkCredentials = firebase.ValidateUserLogin(userNameText.Text, passwordText.Text);
         
            /*
             DEPRECATED: Adobe code implementing the sign in feature using Firebase DB.
            var checkCredentials = App.MyDatabase.LoginValidate(userNameText.Text, passwordText.Text);
            */


          /*  if (checkCredentials != null)
            {
                Console.WriteLine("Found!! " + checkCredentials.Result);
               // currentLoggedInUser = userNameText.Text;
                // Navigation.PushAsync(new HomePage());
            }

            if (checkCredentials == null)
            {
                Console.WriteLine("Not Found, which means is null! " + checkCredentials.Result);
                DisplayAlert("Wrong Credentials!", "You Entered Wrong Credentials, Check UserName or Password", "OK!") ;
            }*/
          
        }


     

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        
    }
}