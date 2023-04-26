using Kudomion.FirebaseManager;
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
    public partial class SignUpPage : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper();
        public SignUpPage()
        {
            InitializeComponent();
            userName.ReturnCommand = new Command(() => userName.Focus());
            password.ReturnCommand = new Command(() => password.Focus());
            confirmPassword.ReturnCommand = new Command(() => confirmPassword.Focus());


        }

        
        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            List<string> stringsL = new List<string>();
            List<User> allUsers = await firebase.GetAllUsers();
            //await DisplayAlert("Alert!", $"Number Of Users: ${allUsers.Count}", "OK!");

            //Check if User Exists and The !allUsers.Count == 0.
            List<string> userStrings = new List<string>();
            if (allUsers.Count > 0)
            {
                bool isUserExists = false;
                foreach (User item in allUsers)
                {
                    userStrings.Add(item.name);
                }

                //Assign Bool Value If Exist..
                isUserExists = userStrings.Contains(userName.Text);

                //Check Point.
                if (isUserExists)
                {
                    await DisplayAlert("User Exists!", "Sorry. This Name Already Registered, Choose another name..", "OK!");
                    return;
                }

                //If User Not Exist Then Add The User.
                if (!isUserExists)
                {
                    RegisterNewUser();
                }
               

            }

            //Check If Input (User, Pass) is/are Empty.
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                await DisplayAlert("Invalid", "Whitespace or Blank Value is Invalid.", "OK!");
                return;
            }

            //Check If Credentials Are Mis-Matched.
            if (password.Text != confirmPassword.Text)
            {
                
                await DisplayAlert("Password Mismatch!", "Please Make Sure That Passwords Are Match!", "OK!");
                return;
            }


            //CheckRegisterNewUser(); if List of Users Empty..
            if (allUsers.Count == 0)
            {
                RegisterNewUser();
            }
        }

        private async void CheckIfUserExist()
        {
            //Check if user already exists.
            List<string> userStrings = new List<string>();
            var listAllUsers = await firebase.GetAllUsers();
            if (!listAllUsers.Any())
            {
                await DisplayAlert("Success!", "User Registered Succesffully!", "OK!");
                RegisterNewUser();
                userName.Text = string.Empty;
                password.Text = string.Empty;
                confirmPassword.Text = string.Empty;
                return;
            }
            foreach (User item in listAllUsers)
            {
                userStrings.Add(item.name);
            }
            User enteredUser = await FirebaseHelper.GetUsrFromName(userName.Text);
            string enteredUserString = enteredUser.name;
            bool userExits = userStrings.Contains(enteredUserString);
            Console.WriteLine("User Status: " + userExits);

            if (userExits == true)
            {
                await DisplayAlert("User Exists!", "Sorry. This Name Already Registered, Choose another name..", "OK!");
                return;
            }
        }

        private async void RegisterNewUser()
        {
            //Add User To DB.
            await firebase.AddUser(userName.Text, password.Text);
            await DisplayAlert("Success!", "User Registered Succesffully!", "OK!");

            //Reset Values
            userName.Text = string.Empty;
            password.Text = string.Empty;
            confirmPassword.Text = string.Empty;
        }


    }
}