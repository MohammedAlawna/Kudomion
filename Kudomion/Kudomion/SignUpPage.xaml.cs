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
        
        public SignUpPage()
        {
            InitializeComponent();
            userName.ReturnCommand = new Command(() => userName.Focus());
            password.ReturnCommand = new Command(() => password.Focus());
            confirmPassword.ReturnCommand = new Command(() => confirmPassword.Focus());


        }

        private async void SignUpButton_Clicked(object sender, EventArgs e)
        {
            if (password.Text != confirmPassword.Text)
            {
                await DisplayAlert("Password Mismatch!", "Please Make Sure That Passwords Are Match!", "OK!");
            }

            if (string.IsNullOrWhiteSpace(userName.Text) || string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(confirmPassword.Text))
            {
                await DisplayAlert("Invalid", "Whitespace or Blank Value is Invalid.", "OK!");

            }
            else
                registerNewUser();
            await DisplayAlert("Success!", "User Registered Succesffully!", "OK!");
            userName.Text = string.Empty;
            password.Text = string.Empty;
            confirmPassword.Text = string.Empty;   
        }

         void registerNewUser()
        {
            App.MyDatabase.CreateUser(new User
            {
                name = userName.Text,
                password = password.Text,
                posts = 0,
                ranking = 0,
                points = 0,
                duels = 0,
                usertype = "User",
            });
        }

        //Funnctions for re-using later on.

        //1- Show All The Users.
        private void ShowAllUsers()
        {
            //myCollectionView.ItemsSource = await App.MyDatabase.ReadUsers();
        }



            }
        }

    
