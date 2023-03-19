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
           
        }

        public string GetUserName()
        {
            return userNameText.Text;
        }

        public string GetUserPassword()
        {
            return passwordText.Text;
        }





        private void LoginButton_Clicked(object sender, EventArgs e)
        {
            var checkCredentials = firebase.ValidateUserLogin(userNameText.Text, passwordText.Text);
            /*
             DEPRECATED: Adobe code implementing the sign in feature using Firebase DB.
            var checkCredentials = App.MyDatabase.LoginValidate(userNameText.Text, passwordText.Text);
            */


            if (checkCredentials != null)
            {
                Console.WriteLine("Found!! " + checkCredentials);
                currentLoggedInUser = userNameText.Text;
                Navigation.PushAsync(new HomePage());
            
            }

            if (checkCredentials == null)
            {
                Console.WriteLine("Not Found, which means is null! " + checkCredentials);
                DisplayAlert("Wrong Credentials!", "You Entered Wrong Credentials, Check UserName or Password", "OK!") ;
            }
          
        }


     

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignUpPage());
        }
        
    }
}