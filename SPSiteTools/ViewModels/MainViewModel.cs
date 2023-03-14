using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph.Beta.Models;
using Microsoft.Graph.Beta.Models.ODataErrors;
using SPPageTools.Services;
using System.Diagnostics;

/**
 * View model for the main page
 * Part of .NET & Graph microsoft-hack-together 2023
 * Author: @Tarzzi
 **/

namespace SPPageTools.ViewModels
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
    private string pageID = "";

    [ObservableProperty]
    private string searchTerm = "";

    [ObservableProperty]
    private IEnumerable<Site> retrievedSites;

    [ObservableProperty]
    private IEnumerable<SitePage> retrievedSitePages;

    [ObservableProperty]
    private User user = null;

    [ObservableProperty]
    private string newSiteTitle = "";

    [ObservableProperty]
    private string newSiteDescription = "";

    [ObservableProperty]
    private string newSiteName = "";

    [ObservableProperty]
    private string updatedSiteTitle = "";

    [ObservableProperty]
    private SiteCollectionResponse sitesResponse;

    [ObservableProperty]
    private SitePageCollectionResponse sitePagesResponse;

    // Create service instance from GraphService.cs and use that for each of the functions below
    private GraphService _graphService;

    public MainViewModel()
    {
      _graphService = GraphService.Instance;
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
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }

    [RelayCommand]
    private async Task CreateSpSitePage()
    {
      try
      {
        var response = await _graphService.CreateNewSitePage(SiteID, NewSiteDescription, NewSiteName, NewSiteTitle);
        SetPageResponse("Created page", response);
      }
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }

    [RelayCommand]
    private async Task UpdatePage()
    {
      try
      {
        var response = await _graphService.UpdateSitePage(SiteID, PageID, UpdatedSiteTitle);
        SetPageResponse("Updated page", response);
      }
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }


    [RelayCommand]
    private async Task DeletePage()
    {
      try
      {
        await _graphService.DeleteSitePage(SiteID, PageID);
        SetStringResponse("Deleted", "Page " + PageID + " has been deleted");
      }
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }


    [RelayCommand]
    private async Task GetSites()
    {
      try
      {
        SitesResponse = await _graphService.SearchSites(SearchTerm);

        // Replace with only the site ID
        foreach (var site in SitesResponse.Value)
        {
          var siteId = site.Id.Split(',')[1];
          site.Id = siteId;
          var webUrl = site.WebUrl.Split("sharepoint.com")[1];
          site.WebUrl = webUrl;
        }

        RetrievedSites = SitesResponse.Value;
      }
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }

    [RelayCommand]
    private async Task GetPages()
    {
      try
      {
        SitePagesResponse = await _graphService.ListPages(SiteID);

        RetrievedSitePages = SitePagesResponse.Value;
      }
      catch (ODataError odataError)
      {
        ShowOdataExceptionAlert(odataError);
      }
      catch (Exception ex)
      {
        ShowExceptionAlert(ex);
      }
    }

    public void SetPageResponse(string responseTitle, SitePage response)
    {
      string shownMessage = "ID: "+ response.Id + " - Title: " + response.Title + " - WebURL: " + response.WebUrl;
      ShowResponsePopUp(responseTitle, shownMessage);
    }

    public void SetStringResponse(string responseTitle, string response)
    {
      ShowResponsePopUp(responseTitle, response);
    }

    public async void ShowResponsePopUp(string responseTitle, string response)
    {
      if (App.Current?.MainPage is not null)
      {
        await App.Current.MainPage.DisplayAlert(responseTitle, response, "Ok");
      }
    }


    public async void ShowOdataExceptionAlert(ODataError odataError)
    {
      string errorCode = odataError.Error.Code.ToString();
      string errorMessage = odataError.Error.Message.ToString();
      var shownMessage = errorCode + " : " + errorMessage;
      if (App.Current?.MainPage is not null)
      {
        await App.Current.MainPage.DisplayAlert("OData Error", shownMessage, "Ok");
      }
    }

    public async void ShowExceptionAlert(Exception ex)
    {
      if (App.Current?.MainPage is not null)
      {
        await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
      }
    }

  }
}