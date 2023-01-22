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
    public partial class Challenges : ContentPage
    {
        public Challenges()
        {
            InitializeComponent();
            p1.Text = Home.getLoggedInUser().name;
            p1.IsEnabled = false;
            
            p2.ItemsSource = App.MyDatabase.ReadUsers();
            roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.getLoggedInUser().name);
            
        }

        private async void Reset_Clicked(object sender, EventArgs e)
        {
            p2.SelectedItem = null;
            
        }

        

        private async void AdmitDefeat_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("You Lost!", "You just admit defeated! Duel Records Will be changed!", "OK");
            var getSelectedRoom = App.MyDatabase.GetActiveRoom(Home.getLoggedInUser().name);
            
            var getSelectedUser = App.MyDatabase.getSpecificUser(getSelectedRoom[0].p1);
            Console.WriteLine("Second Player To Add Points To IS:" + getSelectedRoom[0].p1);
            App.MyDatabase.DecideWinner(getSelectedRoom[0], getSelectedUser);  
        }

        private async void CreateRoom_Clicked(object sender, EventArgs e)
        {
            if(p2 == null)
            {
                await DisplayAlert("Missing Player!", "Please Enter Your Opponent Name", "OK!");
                return;
            }
            checkIfUserExistInLobby();

            Room roomToCreate = new Room();
            roomToCreate.p1 = p1.Text;
            roomToCreate.p2 = p2.Items[p2.SelectedIndex];
            roomToCreate.status = false;
            //roomToCreate.winner = "";

            App.MyDatabase.CreateRoom(roomToCreate);
            //TODO Update Number of Duels for both players by 1.
            await DisplayAlert("Success!", "Room Added! Waiting for your opponent..", "OK!");
        }
        
        bool checkIfUserExistInLobby()
        {

            var getAllRooms = App.MyDatabase.ReadAllRoomsToString();
            if(getAllRooms.Contains(p1.Text) || getAllRooms.Contains(p2.SelectedItem))
            {
                DisplayAlert("User Already Exists!", "Finish Your Current Duel. You can't have more than one room inside Lobby!", "OK!");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}