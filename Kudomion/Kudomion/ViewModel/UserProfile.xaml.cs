using Kudomion.FirebaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion.ViewModel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
         
        public UserProfile(string username)
        {
            InitializeComponent();

            //Load User Profile.
            LoadUserProfile(username);

            //Load Picker Items.
            LoadPickerItems();

            //Switch Visibility As Default.
            stats.IsVisible = true;
            posts.IsVisible = false;
            signature.IsVisible = false;
            duels.IsVisible = false;
        }

        private async void LoadUserProfile(string name)
        {
            loggedInUsername.Text = name;
            User getLoggedInUser = await FirebaseHelper.GetUsrFromName(name);

            //Assign Profile Values. :D 
            noDuels.Text = "Duels: " + getLoggedInUser.duels.ToString();
            noPoints.Text = "Points: " + getLoggedInUser.points.ToString();
            noPosts.Text = "Posts: " + getLoggedInUser.posts.ToString();
            noRanking.Text = "Ranking: " + getLoggedInUser.ranking.ToString();
        } 

        private async void ShowUserDuels(string username)
        {
            FirebaseHelper firebase = new FirebaseHelper();
            List<Room> allRooms = await firebase.GetAllRooms();

            List<Room> roomsForSpecifiedUser = allRooms.Where(n => n.p1 == username || n.p2 == username).ToList();

            if(roomsForSpecifiedUser.Count == 0 || allRooms.Count == 0)
            {
               await DisplayAlert("Room List Alert!", "There are currently no duels for this user!", "OK!");
               return;
            }

            if(roomsForSpecifiedUser.Count > 0)
            {
                await DisplayAlert("Duels List!", "Username: " + roomsForSpecifiedUser[0].p1 + " , " + roomsForSpecifiedUser[0].p2,"OK!");
            }
           
        }

        private void LoadPickerItems()
        {
            userStatPicker.Items.Add("Stats");
            userStatPicker.Items.Add("Duels");
            userStatPicker.Items.Add("Posts");
            userStatPicker.Items.Add("Signature");

            //Set To First One.
            userStatPicker.SelectedIndex = 0;

            userStatPicker.SelectedIndexChanged += (sender, args) =>
            {
                if(userStatPicker.SelectedIndex == 0)
                {
                    //TODO Show user Profile Stats.
                    

                    //Switch Visibility For StackLayout.
                    stats.IsVisible = true;
                    posts.IsVisible = false;
                    signature.IsVisible = false;
                    duels.IsVisible = false;
                }

                if(userStatPicker.SelectedIndex == 1)
                {
                    //TODO Show user Duels.
                    ShowUserDuels(loggedInUsername.Text);

                    //Switch Visibility for StackLayout.
                    stats.IsVisible = false;
                    posts.IsVisible = false;
                    signature.IsVisible = false;
                    duels.IsVisible = true;
                }

                if(userStatPicker.SelectedIndex == 2)
                {
                    //TODO Show user Posts.
                    

                    //Switch Visibility for StackLayout.
                    stats.IsVisible = false;
                    posts.IsVisible = true;
                    signature.IsVisible = false;
                    duels.IsVisible = false;
                }

                if(userStatPicker.SelectedIndex == 3)
                {
                    //TODO Show user Signature.
                    

                    //Switch Visibility for StackLayout.
                    stats.IsVisible = false;
                    posts.IsVisible = false;
                    signature.IsVisible = true;
                    duels.IsVisible = false;
                }
            };
        }

        private void InviteToDuelClicked(object sender, EventArgs e)
        {
            //Change BG of Button.
            invDuel.BackgroundColor = Color.White;

            //Prompt Alert (Testing Purposes).
            DisplayAlert("Success!", $"Invitation Sent To Your Opponent Notifications!", "OK!");
        }

        private void SendChatRequest(object sender, EventArgs e)
        {
            //Change BG of Button.
            chatReq.BackgroundColor = Color.White;
           
            //Prompt Alert (Testing Purposes)
            DisplayAlert("Success!", "Chat Invitation Has Been Sent!", "OK!");
        }
    }
}