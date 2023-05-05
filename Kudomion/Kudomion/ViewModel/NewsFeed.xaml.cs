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
    public partial class NewsFeed : ContentPage
    {
        FirebaseHelper firebase = new FirebaseHelper(); 
        public NewsFeed()
        {
            InitializeComponent();
           // AddPostTrial();
            LoadNewsData();
           
        }

        async void LoadNewsData()
        {
            postsInNewsFeed.ItemsSource = await firebase.GetAllPosts();
            
        }

        async void AddPostTrial()
        {
            Post _postToAdd = new Post
            {
                content = "Welcome Duelists!",
                imageSource = "",
                YGOGoodReaction = 0
            };

            await firebase.AddPost(_postToAdd);
        }

        public void StandradReactClicked(object sender, EventArgs e)
        {
            
            var item = (Image)sender;
            var parent = (Grid)item.Parent;
            var child = (Label)parent.Children[3];

            int convertedString = Int16.Parse(child.Text);
            int incrementCounter = convertedString + 1;

            child.Text = incrementCounter.ToString();
            
        }
        
        
    }
}