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

           // loggedInUserName.name = "droumi2";
           // await firebase.UpdateUser(LoginPage.currentLoggedInUser ,loggedInUserName);
            //Load AllUsersIntoSelector.
            var listOfUsers = await firebase.GetAllUsers();
            p2.ItemsSource = listOfUsers;

            Room trialRoom = new Room {
            p1 = "j1", 
            p2 = "j2",
            isDone = false,
            };
            await firebase.CreateRoom(trialRoom);

            //Load All Rooms In The Lobby.
            roomsCollectionView.ItemsSource = await firebase.GetAllRooms();

            //Debugging Line.
            Console.Write("LOGGED IN USER IS: " + loggedInUserName.name);
           
        }

      /*  public void CheckRooms()
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
        }*/

        private async void UpdateRoomsList()
        {
            //roomsCollectionView.ItemsSource = App.MyDatabase.GetActiveRoom(Home.GetLoggedInUser().name);
            roomsCollectionView.ItemsSource = await firebase.GetAllRooms();
        }

        private void Reset_Clicked(object sender, EventArgs e)
        {
            p2.SelectedItem = null;
            //CheckRooms();
            
        }

        

        private async void AdmitDefeat_Clicked(object sender, EventArgs e)
        {
            //First:: Get Selected Room (The One You Clicked At).
            Room getPlayerRoom = await firebase.GetPlayerRoom(LoginPage.currentLoggedInUser);

            //Second:: Get Selected User From That Room.
            string getSelectedPlayer = getPlayerRoom.p1;


            //Third:: Get Player Rec.
            User getWinningPlayer = await FirebaseHelper.GetUsrFromName(getPlayerRoom.p2);
            User getLoggedInPlayer = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);

            try
            {

                if(getPlayerRoom == null)
                {
                    await DisplayAlert("Error", "You cant Admit Defeat. This is not your room!", "OK!");
                    return;
                }
               
           // Check:: If The Player Who's Trying To Admit Defeat Is Not In The Room!
            if(getPlayerRoom.p1 != LoginPage.currentLoggedInUser) {
                    if(getPlayerRoom.p2 != LoginPage.currentLoggedInUser)
                    {
                        await DisplayAlert("Room Error (Not Exc)", $"You are not involved in this match! {getPlayerRoom.p1} , {getPlayerRoom.p2} , {LoginPage.currentLoggedInUser}", "OK!");
                        return;
                    }
               
            }

            //Fourth:: Decide Winner & Give Awards
            //TODO:: Winning player should be the second player in room!
            getWinningPlayer.duels += 1;
            getWinningPlayer.points += 3;

            //TODO:: Update Ranking Table. o3o

            //Fifth:: Apply Updates
            await firebase.UpdateUser(LoginPage.currentLoggedInUser, getWinningPlayer);

            //Fourth:: Display Alert!
            await DisplayAlert("You Lost!", "You just admit defeated! Duel Records Will be changed!", "OK");



        
          // Console.WriteLine("Second Player To Add Points To IS:" + getSelectedPlayer);
            }
            catch(NullReferenceException nu)
            {
                await DisplayAlert("Room Error", $"Error, Cant Com[lete The Operstion. Contact The Staff!", "OK!");
                return;
            }

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
            roomToCreate.isDone = false;
            //roomToCreate.winner = "";

            await firebase.CreateRoom(roomToCreate);
           
            //TODO Update Number of Duels for both players by 1.
            UpdateRoomsList();
            //CheckRooms();
            await DisplayAlert("Success!", "Room Added! Waiting for your opponent..", "OK!");


             }
        
       
    }
}