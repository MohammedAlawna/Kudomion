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
    public partial class Posts : ContentPage
    {
        public Posts()
        {
            InitializeComponent();
           // author.Text = LoginPage.currentLoggedInUser;
            author.Text = Home.getLoggedInUser().name;
            App.MyDatabase.ReadAllRoomsToString();
            
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                postsCollectionView.ItemsSource = App.MyDatabase.ReadPosts();
            }
            catch
            {

            }
        }

        void UpdateSingleRecord(string userNameToGet)
        {
           Home instance = new Home();
           List<User> listOfUsers =  App.MyDatabase.ReadUsers();
           User selectedUser = listOfUsers.Find(u => u.name == userNameToGet);
           selectedUser.posts++;

            Home.updateUser();
            
        }
        
      
        private void createPost_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("NOT AUTHORIZED!", "Posting system is only allowed for admins and supervisors", "OK!");
            return;
            try
            {
                if (content.Text == null)
                {
                    DisplayAlert("Post Empty!", "Please Write Something To Post!", "OK!");
                    return;
                }
                Post postToCreate = new Post();

                //Get Current Logged In User as the autohr.
                postToCreate.author = LoginPage.currentLoggedInUser;


                //TODO Comments system will be implemented later on.
                // postToCreate.comments = null;


                postToCreate.content = content.Text;

                //TODO Goods System will be implemented later on.
                //postToCreate.goods = null;
                Console.WriteLine(Home.getLoggedInUser());
                App.MyDatabase.UpdateUser(Home.getLoggedInUser());
                App.MyDatabase.CreatePost(postToCreate);

                //  Console.WriteLine(Home.getLoggedInUser().posts);
                Home.updateUser();

                DisplayAlert("Success!", "Post Added Successfully!", "OK!");
            }
            catch 
            {

            }
           
        }

    }
}