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
            ResetRoomValues();
            LoadDuelistsData();
        }

        private async void ProcessRoomCreation()
        {
            await firebase.GetAllRooms();
        }
        private async void LoadDuelistsData()
        {
            //Load LoggedInUser Name.
            var loggedInUserName = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);
            p1.Text = loggedInUserName.name;
            p1.IsEnabled = false;

            //Load AllUsersIntoSelector.
            List<User> listOfUsers = await firebase.GetAllUsers();
            p2.ItemsSource = listOfUsers;

            //Load All Rooms In The Lobby.
            roomsCollectionView.ItemsSource = await firebase.GetAllRooms();           
        }

     
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


        private Room getPlayerRoom = null;
        public User firstPlayer = null;
        public User secondPlayer = null;
        public User getWinningPlayer = null;
        private async void AdmitDefeat_Clicked(object sender, EventArgs e)
        {
            try { 
                //First:: Get Selected Room (The One You Clicked At).
                getPlayerRoom = await firebase.GetPlayerRoom(LoginPage.currentLoggedInUser);
                firstPlayer = await FirebaseHelper.GetUsrFromName(getPlayerRoom.p1);
                secondPlayer = await FirebaseHelper.GetUsrFromName(getPlayerRoom.p2);

                Console.WriteLine($"First Player Is: {firstPlayer.name}" + $", Second Player Is: {secondPlayer.name}");
               
                //Second:: Get Selected User From That Room.
                string getSelectedPlayer = getPlayerRoom.p1;

                User getLoggedInPlayer = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);

              
                getWinningPlayer = null;
                if (firstPlayer.name != getPlayerRoom.p1 && firstPlayer.name != getPlayerRoom.p2)
                {
                    await DisplayAlert("Missing Duelist!", "You Are Not Involved In This Room!", "OK!");
                    return;
                }
                if (LoginPage.currentLoggedInUser == getPlayerRoom.p1)
                {
                    //Second Player => winner.
                    getWinningPlayer = secondPlayer;
                    getWinningPlayer.duels += 1;
                    getWinningPlayer.points += 3;
                    firstPlayer.duels += 1;
                    getPlayerRoom.isDone = true;
                    getPlayerRoom.winner = getWinningPlayer.name;

                    //Update User and Room Records.
                    await firebase.UpdateUser(getWinningPlayer.name, getWinningPlayer);
                    await firebase.UpdateUser(firstPlayer.name, firstPlayer);
                    await firebase.UpdateRoom(getPlayerRoom.p1, getPlayerRoom.p2, getPlayerRoom);

                    //Update User Profile In Home Page.
                    User currentUser = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);
                    Home.noDuels.Text = currentUser.duels.ToString();
                    Home.noPoints.Text = currentUser.points.ToString();

                    //Update Ranking
                    Leaderboard lb = new Leaderboard();
                    lb.RankAllUsers();

                    //Prompt Admit Defeat!
                    await DisplayAlert("You Lost!", $"You just admit defeated! Duel Records Will be changed! Room Winner: {getPlayerRoom.winner}, P1{getPlayerRoom.p1} P2{getPlayerRoom.p2} Winning Player{getWinningPlayer.name}", "OK");

                    //Update Rooms List.
                    UpdateRoomsList();

                    //Reseting Room Values:
                    ResetRoomValues();

                    return;
                }
                if (LoginPage.currentLoggedInUser != getPlayerRoom.p1)
                {
                    //First Player => winner.
                    getWinningPlayer = firstPlayer;
                    getWinningPlayer.duels += 1;
                    getWinningPlayer.posts += 3;
                    secondPlayer.duels += 1;
                    getPlayerRoom.isDone = true;

                    //Update User and Room Records.
                    await firebase.UpdateUser(getWinningPlayer.name, getWinningPlayer);
                    await firebase.UpdateUser(secondPlayer.name, secondPlayer);
                    await firebase.UpdateRoom(getPlayerRoom.p1, getPlayerRoom.p2, getPlayerRoom);

                    //Update User Profile In Home Page.
                    User currentUser = await FirebaseHelper.GetUsrFromName(LoginPage.currentLoggedInUser);
                    Home.noDuels.Text = currentUser.duels.ToString();
                    Home.noPoints.Text = currentUser.points.ToString();

                    //Update Ranking
                    Leaderboard lb = new Leaderboard();
                    lb.RankAllUsers();

                    //Prompt Admit Defeat.
                    await DisplayAlert("You Lost!", $"You just admit defeated! Duel Records Will be changed!" + getWinningPlayer.name, "OK");

                    //Update Rooms List.
                    UpdateRoomsList();

                    //Reseting Room Values:
                    ResetRoomValues();

                    return;
                }


               
            }
            catch(Exception er)
            {
                await DisplayAlert("Exception!", $"{er.HResult}, {er.Message}", "OK!");
            }
        }


        void ResetRoomValues()
        {
            getPlayerRoom = null;
            firstPlayer = null;
            secondPlayer = null;
            getWinningPlayer = null;
        }

       
        private async void CreateRoom_Clicked(object sender, EventArgs e)
        {
            if(p2 == null)
            {
                await DisplayAlert("Missing Player!", "Please Enter Your Opponent Name", "OK!");
                return;
            }
            bool isUserExist = await firebase.CheckIfUserExists();
            Console.WriteLine($"Is User Exist: {isUserExist}");
            if(isUserExist == true)
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