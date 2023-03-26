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
          //  GetLoggedInUser();
          /*  updateUser();
            updateUserPoints();
            updateUserRanking();*/
            DisplayAlert("Logged In!","User Is: ", "OK!");

            
           // loggedInUsername.Text = GetLoggedInUser().Result.name;
          //  noPoints.Text = GetLoggedInUser().Result.points.ToString();
        
        }

        public static string getLoggedInUserName() {
            Home h = new Home();
            return h.loggedInUsername.Text;
        }

      public static User GetLoggedInUser()
      {
            Home h = new Home();
            Task<User> returnedValue = FirebaseHelper.GetUsrFromName(h.loggedInUsername.Text);
            return returnedValue.Result;
            
      }

        public static User GetUser()
        {
            Home h = new Home();
            User specifiedUser = FirebaseHelper.GetUsrFromName(h.loggedInUsername.Text).Result;
            return specifiedUser;
        }

          public static void updateUser()
          {
                noPosts.Text = GetLoggedInUser().posts.ToString();
                noPoints.Text = GetLoggedInUser().points.ToString();    
          }
       
           public static void updateUserRanking()
           {
                Home h = new Home();
                var GetUser = FirebaseHelper.GetUsrFromName(h.loggedInUsername.Text).Result.name;

                noRanking.Text = GetUser;
           }

            public static void updateUserPoints()
            {
                Home H = new Home();
                noPoints.Text = GetLoggedInUser().ranking.ToString();
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
                noPosts.Text = GetLoggedInUser().posts.ToString();
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





/*   public async static Task<User> GetLoggedInUser()
   {
       FirebaseHelper firebase = new FirebaseHelper();
       return await firebase.GetUserByName(LoginPage.currentLoggedInUser);

       /*

         DEPRECATED: Above code will implemetn it with new way (Firebase DB)
        return App.MyDatabase.getSpecificUser(LoginPage.currentLoggedInUser);

   }*/