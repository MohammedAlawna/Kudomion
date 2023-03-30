using Kudomion.FirebaseManager;
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
        FirebaseHelper firebase = new FirebaseHelper();
        public Challenges()
        {
            InitializeComponent();

            LoadDuelistsData();
            // Console.WriteLine("Name in Challenges: ");
            ProcessRoomCreation();
            Console.WriteLine("Player 1 Is: " + LoginPage.currentLoggedInUser);
            // p2.ItemsSource = firebase.GetAllUsers().Result; **issue
            //CheckRooms(); **issue
            // roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().name); **issue
            
            //Detected Issue: Any line of code that uses the new getLoggedInUser throws an error (lag).
        }

        private async void ProcessRoomCreation()
        {
           // await firebase.CreateRoom(new Room { p1 = "KUDO", p2="Mezo" });
            await firebase.GetAllRooms();
           // var desiredRoom = await firebase.GetPlayerRoom("Mezo");
           // Console.WriteLine(desiredRoom.p1 + " ||___VS___ || " + desiredRoom.p2);
        }
        private async void LoadDuelistsData()
        {
            //Load LoggedInUser Name.
            var loggedInUserName = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);
            p1.Text = loggedInUserName.name;
            p1.IsEnabled = false;

            //Load AllUsersIntoSelector.
            var listOfUsers = await firebase.GetAllUsers();
            p2.ItemsSource = listOfUsers;


            //Load All Rooms In The Lobby.
            roomsCollectionView.ItemsSource = await firebase.GetAllRooms();

            //Debugging Line.
            Console.Write("LOGGED IN USER IS: " + loggedInUserName.name);
           
        }

        public void CheckRooms()
        {
            var allRoomsVisual = roomsCollectionView.ItemsSource;
            var allRoomsLogic = App.MyDatabase.ReadRooms();
            var getLoggedInUser = Home.GetLoggedInUser().name;
            foreach(var r in allRoomsLogic)
            {
                if(r.p1 != Home.GetLoggedInUser().name || r.p2 != Home.GetLoggedInUser().name)
                {
                    
                    r.disabled = true;
                 

                }
             
            }
        }

        public void UpdateRoomsList()
        {
            roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().name);

        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            p2.SelectedItem = null;
            CheckRooms();
            
        }

        

        private async void AdmitDefeat_Clicked(object sender, EventArgs e)
        {
            //First:: Get Selected Room (The One You Clicked At).
            var getPlayerRoom =  await firebase.GetPlayerRoom(LoginPage.currentLoggedInUser);

            //Second:: Get Selected User From That Room.
            var getSelectedPlayer = getPlayerRoom.p1;

            //Third:: Decide Winner.


            //Fourth:: Display Alert!
            await DisplayAlert("You Lost!", "You just admit defeated! Duel Records Will be changed!", "OK");



            var getSelectedRoom = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().name);
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