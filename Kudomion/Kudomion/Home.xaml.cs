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
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            getLoggedInUser();

            loggedInUsername.Text = getLoggedInUser().name;
            noPoints.Text = getLoggedInUser().points.ToString();
            noPosts.Text = getLoggedInUser().posts.ToString();

            
            //Load Profile Image.
            //prof.Source = "https://i.imgur.com/hFEz0Fv.png";
        }

       public static User getLoggedInUser()
        {
            return App.MyDatabase.getSpecificUser(LoginPage.currentLoggedInUser);
        }

        private async void Profile_Tapped(object sender, EventArgs e)
        {
             Console.WriteLine("Profile Tapped");
        }

        private async void Post_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("Post Tapped");
        }

        private async void Lobby_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("Lobby Tapped");
        }

        private async void Leaderboard_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("Leaderboard Tapped");
        }

        private async void Discord_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("Discord Tapped");
        }

        private async void onClickDecks(object sender, EventArgs e)
        {
            Console.WriteLine("Decks! Clicked!");
        }

        private async void onClickYGOGuide(object sender, EventArgs e)
        {
            Console.WriteLine("YGO Guide! Clicked!");
        }

        private async void onClickTournies(object sender,  EventArgs e)
        {
            Console.WriteLine("Tournies! Clicked!");
        }


        private async void Logout_Tapped(object sender, EventArgs e)
        {
            LoginPage.currentLoggedInUser = string.Empty;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}