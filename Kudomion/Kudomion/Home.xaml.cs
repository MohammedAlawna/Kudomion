using Kudomion.FirebaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            loggedInUsername.Text = LoginPage.currentLoggedInUser;
            LoadDuelistProfile();
        }

        public async void LoadDuelistProfile()
        {
            //Get Current Logged-In User/Duelist.
            User getDuelist = await FirebaseHelper.GetUsrFromName(loggedInUsername.Text);

            //Get & Assign The Name (New one if Updated).
            loggedInUsername.Text = getDuelist.name;

            //Get & Assign The Number Of Points.
            string numberOfPoints = getDuelist.points.ToString();
            noPoints.Text = numberOfPoints;

            //Get & Assign The Number of Duels.
            string numberOfDuels = getDuelist.duels.ToString();
            noDuels.Text = numberOfDuels;

            //Get & Assign The Ranking.
            string duelistRanking = getDuelist.ranking.ToString();
            noRanking.Text = duelistRanking;
        }

        private async void Profile_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSettings());
        }

        private async void Post_Tapped(object sender, EventArgs e)
        {     
            await Navigation.PushAsync(new Posts());
        }

        private async void Lobby_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Challenges());
        }

        private async void Leaderboard_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Leaderboard());
        }

        private async void Discord_Tapped(object sender, EventArgs e)
        {

            string url = "https://discord.gg/mpvmEP8";
            await Browser.OpenAsync(url);
            Console.WriteLine("Discord Tapped");
        }

        private async void OnClickDecks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DecksList());
        }

        private async void OnClickYGOGuide(object sender, EventArgs e)
        {
            await DisplayAlert("Not Available", "YGO Guide not available in this release.", "OK!");
        }


        private async void OnClickTournies(object sender, EventArgs e)
        {
            await DisplayAlert("Not Available", "Tournaments not available in this release.", "OK!");
        }


        private async void Logout_Tapped(object sender, EventArgs e)
        {
            LoginPage.currentLoggedInUser = string.Empty;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}