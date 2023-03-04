using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SPSiteTools.Models;

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
            Microsoft.Graph.Beta.Models.User user = null;
            try
            {
                user = await service.GetMyDetailsAsync();
                UserName = user.DisplayName;
                UserGivenName = user.GivenName;
                UserSurname = user.Surname;
                UserPrincipalName = user.UserPrincipalName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user details: {ex}");
            }

        }
        [RelayCommand]
        private async Task CreateSpSite()
        {
            var service = new GraphService();
            Microsoft.Graph.Beta.Models.SitePage newPage = await service.CreateNewSitePage("test","test","test","test");

        }

        

    }

}