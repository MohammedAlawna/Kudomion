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
        }

        private void InviteToDuelClicked(object sender, EventArgs e)
        {
            //Change Button Color.

            DisplayAlert("Success!", "Invitation Sent To Your Opponent Notifications!", "OK!");
        }

        private void SendChatRequest(object sender, EventArgs e)
        {
            DisplayAlert("Success!", "Chat Invitation Has Been Sent!", "OK!");
        }
    }
}