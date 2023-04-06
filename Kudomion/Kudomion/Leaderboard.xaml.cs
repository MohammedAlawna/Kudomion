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
        
        public Leaderboard()
        {
            InitializeComponent();
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
        }
    }