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
         
        public UserProfile()
        {
            InitializeComponent();

            //Load Picker Items.
            LoadPickerItems();

            //Switch Visibility As Default.
            stats.IsVisible = true;
            posts.IsVisible = false;
            signature.IsVisible = false;
            duels.IsVisible = false;
        }

        void LoadPickerItems()
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
                    DisplayAlert("Alert!", "You are vieweing user Stats!", "OK!");

                    //Switch Visibility For StackLayout.
                    stats.IsVisible = true;
                    posts.IsVisible = false;
                    signature.IsVisible = false;
                    duels.IsVisible = false;
                }

                if(userStatPicker.SelectedIndex == 1)
                {
                    //TODO Show user Duels.
                    DisplayAlert("Alert!", "You are vieweing user Duels!", "OK!");

                    //Switch Visibility for StackLayout.
                    stats.IsVisible = false;
                    posts.IsVisible = false;
                    signature.IsVisible = false;
                    duels.IsVisible = true;
                }

                if(userStatPicker.SelectedIndex == 2)
                {
                    //TODO Show user Posts.
                    DisplayAlert("Alert!", "You are viewing user Posts!", "OK!");

                    //Switch Visibility for StackLayout.
                    stats.IsVisible = false;
                    posts.IsVisible = true;
                    signature.IsVisible = false;
                    duels.IsVisible = false;
                }

                if(userStatPicker.SelectedIndex == 3)
                {
                    //TODO Show user Signature.
                    DisplayAlert("Alert!", "You are viewing user Signature", "OK!");

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