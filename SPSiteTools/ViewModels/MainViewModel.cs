using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graph.Beta.Models;
using Microsoft.Graph.Beta.Models.ODataErrors;
using SPSiteTools.Services;

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
    private async Task CreateSpSite()
    {
      try
      {
        _ = await _graphService.CreateNewSitePage(SiteID, NewSiteDescription, NewSiteName, NewSiteTitle);

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

    public delegate void MyEventAction(string message);
    public event MyEventAction MyEvent;


    [RelayCommand]
    private async Task GetSites()
    {
      try
      {
        GraphResponse = await _graphService.GetSitePages(SearchTerm);
        RetrievedSites = GraphResponse.Value;
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

    public async void ShowOdataExceptionAlert(ODataError odataError)
    {
      string errorCode = odataError.Error.Code.ToString();
      string errorMessage = odataError.Error.Message.ToString();
      var toastMesssage = errorCode + " : " + errorMessage;
      if (App.Current?.MainPage is not null)
      {
        await App.Current.MainPage.DisplayAlert("OData Error", toastMesssage, "Ok");
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