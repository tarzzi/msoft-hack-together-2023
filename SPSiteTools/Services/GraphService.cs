using Azure.Identity;
using Microsoft.Graph;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models;
using Microsoft.Kiota.Abstractions;

namespace SPSiteTools.Services
{
  internal class GraphService
  {
    private readonly string[] _scopes = new[] { "User.Read", "Sites.Read.All", "Sites.ReadWrite.All" };
    private const string TenantId = "b8acf3fb-c93b-4f7f-8b47-39d4c91068a6";
    private const string ClientId = "24af24f0-fe8b-4a17-a623-19f24cca3de8";
    private GraphServiceClient _client;
    private static GraphService _instance;
    public GraphService()
    {
      Initialize();
    }

    public static GraphService Instance
    {
      get
      {
        if (_instance == null)
        {
          _instance = new GraphService();
        }
        return _instance;
      }
    }
    public void Dispose()
    {
      _instance = null;
    }

    private void Initialize()
    {
      // using windows
      if (OperatingSystem.IsWindows())
      {
        var options = new InteractiveBrowserCredentialOptions
        {
          TenantId = TenantId,
          ClientId = ClientId,
          AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
          RedirectUri = new Uri("https://localhost"),
        };

        InteractiveBrowserCredential interactiveCredential = new(options);
        _client = new GraphServiceClient(interactiveCredential, _scopes);

      }
    }

    public async Task<User> GetMyDetailsAsync()
    {
      return await _client.Me.GetAsync();
    }

    public async Task<SitePage> CreateNewSitePage(string siteID, string description, string name, string title)
    {
      var requestBody = new SitePage
      {
        Description = description,
        Name = name,
        Title = title,
        PageLayout = PageLayoutType.Article,
        PromotionKind = PagePromotionType.Page,
      };
      return await _client.Sites["{site-id}"].Pages.PostAsync(requestBody);
    }

    public async Task<SiteCollectionResponse> GetSitePages(string searchTerm)
    {

      // Get a list of sites using Graph api
      // Beta endpoint currently https://graph.microsoft.com/beta/sites/{siteId}/pages

      return await _client.Sites.GetAsync((requestConfiguration) =>
          {
            requestConfiguration.QueryParameters.Search = searchTerm;
          });

    }

    public async Task<SitePage> UpdateSitePage(string siteID, string pageID, string title)
    {
      var requestBody = new SitePage
      {
        Title = title,
      };
      return await _client.Sites[$"{pageID}"].Pages[$"{siteID}"].PatchAsync(requestBody);

    }

    public async Task DeleteSitePage(string siteID, string pageID)
    {
      await _client.Sites[$"{siteID}"].Pages[$"{pageID}"].DeleteAsync();
    }
  }
}
