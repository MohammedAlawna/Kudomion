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
          
            getLoggedInUser();
            updateUser();
            updateUserPoints();
            updateUserRanking();
            //numberOfPoints = noPoints.Text;
            //numberOfPosts = noPosts.Text;

            
            loggedInUsername.Text = getLoggedInUser().Result.name;
            noPoints.Text = getLoggedInUser().Result.points.ToString();
            //  noPosts.Text = getLoggedInUser().posts.ToString();
            //updateUser();


            //Load Profile Image.
            //prof.Source = "https://i.imgur.com/hFEz0Fv.png";
        }

      

        public static void updateUser()
        {
             noPosts.Text = getLoggedInUser().Result.posts.ToString();
             noPoints.Text = getLoggedInUser().Result.points.ToString();
             
        }

        public static void updateUserRanking()
        {
            noRanking.Text = getLoggedInUser().Result.ranking.ToString();
        }

        public static void updateUserPoints()
        {
            noPoints.Text = getLoggedInUser().Result.ranking.ToString();
        }


        public async static Task<User> getLoggedInUser()
        {
            FirebaseHelper firebase = new FirebaseHelper();
            return await firebase.GetUserByName(LoginPage.currentLoggedInUser);

            /*
           
              DEPRECATED: Above code will implemetn it with new way (Firebase DB)
             return App.MyDatabase.getSpecificUser(LoginPage.currentLoggedInUser);
         */
        }


        private async void Profile_Tapped(object sender, EventArgs e)
        {
             await DisplayAlert("Not Available", "Profile Settings not available in this release.", "OK!");
             Console.WriteLine("Profile Tapped");
        }

        private async void Post_Tapped(object sender, EventArgs e)
        {
            Console.WriteLine("Post Tapped");
            //updatePostsValue(getLoggedInUser().posts.ToString());
            noPosts.Text = getLoggedInUser().Result.posts.ToString();
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
           
            var url = "https://discord.gg/mpvmEP8";
            await Browser.OpenAsync(url);
            Console.WriteLine("Discord Tapped");
        }

        private async void onClickDecks(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DecksList());
        }

        private async void onClickYGOGuide(object sender, EventArgs e)
        {
            await DisplayAlert("Not Available", "YGO Guide not available in this release.", "OK!");

            Console.WriteLine("YGO Guide! Clicked!");
        
        }


        private async void onClickTournies(object sender,  EventArgs e)
        {
            await DisplayAlert("Not Available", "Tournaments not available in this release.", "OK!");

            Console.WriteLine("Tournies! Clicked!");
        }


        private async void Logout_Tapped(object sender, EventArgs e)
        {
            LoginPage.currentLoggedInUser = string.Empty;
            await Navigation.PushAsync(new LoginPage());
        }
    }
}