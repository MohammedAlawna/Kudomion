using Kudomion.FirebaseManager;
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
    public partial class Leaderboard : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Leaderboard()
        {
            InitializeComponent();
            RankAllUsers();
        }

        public async void RankAllUsers()
        {
            List<User> allUsers = await firebaseHelper.GetAllUsers();
            var rankedUsers = allUsers.OrderByDescending(p => p.points);
            userRankingsToLoad.ItemsSource = rankedUsers;
        }

        private void UsernameClicked(object sender, EventArgs e)
        {
            var parent = (Label)sender;
            
            //Displaying The User Clicked (Debuggin) -> Done: Is Working.
            DisplayAlert("Alert!", $"Sender is: {parent.Text}", "OK!");
        }
    }
    }