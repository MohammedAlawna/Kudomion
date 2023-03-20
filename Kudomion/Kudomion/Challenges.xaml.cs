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
            p1.Text = Home.GetLoggedInUser().Result.name;
            p1.IsEnabled = false;
          //  checkIfUserExistInLobby();
           //Console.WriteLine(checkIfUserExistInLobby());
            p2.ItemsSource = App.MyDatabase.ReadUsers();
            CheckRooms();
            roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().Result.name);
            
        }

        public void CheckRooms()
        {
            var allRoomsVisual = roomsCollectionView.ItemsSource;
            var allRoomsLogic = App.MyDatabase.ReadRooms();
            var getLoggedInUser = Home.GetLoggedInUser().Result.name;
            foreach(var r in allRoomsLogic)
            {
                if(r.p1 != Home.GetLoggedInUser().Result.name || r.p2 != Home.GetLoggedInUser().Result.name)
                {
                    
                    r.disabled = true;
                 

                }
             
            }
        }

        public void UpdateRoomsList()
        {
            roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().Result.name);

        }

        private async void Reset_Clicked(object sender, EventArgs e)
        {
            p2.SelectedItem = null;
            CheckRooms();
            
        }

        

        private async void AdmitDefeat_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("You Lost!", "You just admit defeated! Duel Records Will be changed!", "OK");
            var getSelectedRoom = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().Result.name);
            
            var getSelectedUser = App.MyDatabase.getSpecificUser(getSelectedRoom[0].p1);
            Console.WriteLine("Second Player To Add Points To IS:" + getSelectedRoom[0].p1);
            //Work at the following 'status' update with the refresh method! o3o
            //getSelectedRoom[0].status = true;
            //CheckRooms();
            App.MyDatabase.DecideWinner(getSelectedRoom[0], getSelectedUser);  
        
        }

       

        bool checkIfUserExistInLobby()
        {
            
            var getAllRooms = App.MyDatabase.ReadAllActiveRoomsToString();
            //   Console.WriteLine(getAllRooms[0]);

            if (getAllRooms.Count == 0)
            {
                return false;
            }
           
            if (getAllRooms.Contains(p1.Text) || getAllRooms.Contains(p2.SelectedItem))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void CreateRoom_Clicked(object sender, EventArgs e)
        {
            if(p2 == null)
            {
                await DisplayAlert("Missing Player!", "Please Enter Your Opponent Name", "OK!");
                return;
            }
            
            if(checkIfUserExistInLobby() == true)
            {
                await DisplayAlert("User Exist!", "You can't have more than one room at once. Please, complete your first match..", "OK!");
              
                return;
            }

            if (p1.Text == p2.Items[p2.SelectedIndex])
            {
                await DisplayAlert("Same User!", "You can't duel yourself! C'mon! Please Specify Another Opponent..", "OK!");
                return;
            }

            Room roomToCreate = new Room();
            roomToCreate.p1 = p1.Text;
            // roomToCreate.p2 = "opp";
            roomToCreate.p2 = p2.Items[p2.SelectedIndex];
            roomToCreate.status = false;
            //roomToCreate.winner = "";

            App.MyDatabase.CreateRoom(roomToCreate);
            //TODO Update Number of Duels for both players by 1.
            UpdateRoomsList();
            CheckRooms();
            await DisplayAlert("Success!", "Room Added! Waiting for your opponent..", "OK!");

         

             }
        
       
    }
}