using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph.Beta.Models;
using SPSiteTools.Services;
using System.Diagnostics;

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
        private Boolean displayLogin = true;

        [ObservableProperty]
        private string siteID = "";

        [ObservableProperty]
        private string searchTerm = "";

        [ObservableProperty]
        private IEnumerable<Site> retrievedSites;

        [ObservableProperty]
        private User user = null;

        [ObservableProperty]
        private string newSiteTitle = "";

        [ObservableProperty]
        private string newSiteDescription = "";

        [ObservableProperty]
        private string newSiteName = "";

        [ObservableProperty]
        private SiteCollectionResponse graphResponse;

        // Create service instance from GraphService.cs and use that for each of the methods below
        
        private GraphService _graphService;

        public MainViewModel()
        {
            _graphService = GraphService.Instance;
        }
        public void LogOut()
        {
            try
            {
                _graphService.Dispose();
                DisplayLogin = true;
                LoggedIn = false;
                // Other logout logic here
            }
            catch (Exception ex)
            {

                Debug.WriteLine("failed here");
                Debug.WriteLine(ex);
                throw;
            }
        }

        [RelayCommand]
        private async Task LoadUserInformation()
        {
            try
            {
                User = await _graphService.GetMyDetailsAsync();
                UserName = User.DisplayName;
                UserGivenName = User.GivenName;
                UserSurname = User.Surname;
                UserPrincipalName = User.UserPrincipalName;
                HelloMessage = $"Hello {User.GivenName}!";
                LoggedIn = true;
                DisplayLogin = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user details: {ex}");
            }

        }
        [RelayCommand]
        private async Task CreateSpSite()
        {
            _ = await _graphService.CreateNewSitePage(SiteID, NewSiteDescription, NewSiteName, NewSiteTitle);

        }

        [RelayCommand]
        private async Task GetSites()
        {
            GraphResponse = await _graphService.GetSitePages(SearchTerm);
            RetrievedSites = GraphResponse.Value;
        }

    }

}