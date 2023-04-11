using Kudomion.FirebaseManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Kudomion
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Leaderboard : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public Leaderboard()
        {
            InitializeComponent();

            RankAllUsers();
           // var rankedUsers = App.MyDatabase.ReadUsers().OrderByDescending(x => x.points);
           // userRankingsToLoad.ItemsSource =  rankedUsers;
           
            //var listToGetCount = rankedUsers.ToList();
          
         //   var getCount = Enumerable.Count(rankedUsers);

            //var getCountOfThisUser = listToGetCount.FindIndex(0, getCount, );


         /*   for (int i = 0; i < getCount; i++)
            {
             

                var newArr =  rankedUsers.ToArray();
             
                
            }*/
        }

        public async void RankAllUsers()
        {
            List<User> allUsers = await firebaseHelper.GetAllUsers();
            var rankedUsers = allUsers.OrderByDescending(p => p.points);
            userRankingsToLoad.ItemsSource = rankedUsers;
        }

    }
    }