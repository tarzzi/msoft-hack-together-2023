using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph.Beta.Models;
using SPSiteTools.Models;

namespace SPSiteTools.ViewModels
{
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

        [ObservableProperty]
        private Boolean loggedIn = false;

        [ObservableProperty]
        private string siteID = "";

        [ObservableProperty]
        private SitePageCollectionResponse sitePageCollectionResponse;

        [ObservableProperty]
        private User user = null;

        [ObservableProperty]
        private string newSiteTitle = "";

        [ObservableProperty]
        private string newSiteDescription = "";

        [ObservableProperty]
        private string newSiteName = "";

        [RelayCommand]
        private async Task LoadUserInformation()
        {
            var service = new GraphService();
            try
            {
                User = await service.GetMyDetailsAsync();
                UserName = User.DisplayName;
                UserGivenName = User.GivenName;
                UserSurname = User.Surname;
                UserPrincipalName = User.UserPrincipalName;
                HelloMessage = $"Hello {User.GivenName}!";
                LoggedIn = true;
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
            _ = await service.CreateNewSitePage(SiteID, NewSiteDescription, NewSiteName, NewSiteTitle);

        }

        [RelayCommand]
        private async Task GetSites()
        {
            var service = new GraphService();
            SitePageCollectionResponse = await service.GetSitePages(SiteID);

        }

    }

}