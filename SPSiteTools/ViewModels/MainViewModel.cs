using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPSiteTools.ViewModels
{
    public sealed record CountChangedMessage(string Text);
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private string userName = "";

        [ObservableProperty]
        private string userGivenName = "";

        [ObservableProperty]
        private string userSurname = "";

        [ObservableProperty]
        private string userPrincipalName = "";

        [ObservableProperty]
        private string helloMessage = "Hello you!";

        [RelayCommand]
        private async Task LoadUserInformation()
        {
            var service = new GraphService();
            Microsoft.Graph.Models.User user = await service.GetMyDetailsAsync();

            UserName = user.DisplayName;
            UserGivenName = user.GivenName;
            UserSurname = user.Surname;
            UserPrincipalName = user.UserPrincipalName;
        }


    }

}