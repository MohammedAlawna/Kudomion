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
            bool allowUserCreation = false;
            //TODO Check If User Exist
            List<string> stringsL = new List<string>();
            List<User> allUsers = await firebase.GetAllUsers();
            await DisplayAlert("Alert!", $"Number Of Users: ${allUsers.Count}", "OK!");
            //RegisterNewUser();
            
            //CheckRegisterNewUser(); if List of Users Empty..
            if (allUsers.Count == 0)
            {
                
                allowUserCreation = true;
                
            }

            //Check If Credentials Are Mis-Matched.
            if (password.Text != confirmPassword.Text)
            {
                allowUserCreation = false;
                await DisplayAlert("Password Mismatch!", "Please Make Sure That Passwords Are Match!", "OK!");
            }

            //Check If Input (User, Pass) is/are Empty.
            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                await DisplayAlert("Invalid", "Whitespace or Blank Value is Invalid.", "OK!");

            }

            if (allowUserCreation == true)
            {
                RegisterNewUser();
            }
           


            /*  

             
              else
                  RegisterNewUser();
              await DisplayAlert("Success!", "User Registered Succesffully!", "OK!");
              userName.Text = string.Empty;
              password.Text = string.Empty;
              confirmPassword.Text = string.Empty;   */
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

        async void RegisterNewUser()
        {

            await firebase.AddUser(userName.Text, password.Text);
           /*
           Deprecated:: Above Code should handle the DB with Firebase.
            App.MyDatabase.CreateUser(new User
            {
                name = userName.Text,
                password = password.Text,
                posts = 0,
                ranking = 0,
                points = 0,
                duels = 0,
                usertype = "User",
            });*/
        }

        //Funnctions for re-using later on.

        //1- Show All The Users.
        private void ShowAllUsers()
        {
            //myCollectionView.ItemsSource = await App.MyDatabase.ReadUsers();
        }



            }
        }

    
