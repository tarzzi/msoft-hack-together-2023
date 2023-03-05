using Azure.Identity;
using Microsoft.Graph.Beta;
using Microsoft.Graph.Beta.Models;

namespace SPSiteTools.Models
{
    internal class GraphService
    {
        private readonly string[] _scopes = new[] { "User.Read","Sites.Read.All", "Sites.ReadWrite.All" };
        private const string TenantId = "";
        private const string ClientId = "";
        private GraphServiceClient _client;

        public GraphService()
        {
            Initialize();
        }

        private void Initialize()
        {
            // using windows
            if(OperatingSystem.IsWindows())
            {
                var options = new InteractiveBrowserCredentialOptions
                {
                    TenantId = TenantId,
                    ClientId = ClientId,
                    AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
                    RedirectUri = new Uri("https://localhost"),
                };

                try
                {
                    InteractiveBrowserCredential interactiveCredential = new(options);
                    _client = new GraphServiceClient(interactiveCredential, _scopes);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error initializing Graph", ex);
                }
            }
        }

        public async Task<User> GetMyDetailsAsync()
        {
            try
            {
                return await _client.Me.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user details: {ex}");
                return null;
            }
        }

        public async Task<SitePage> CreateNewSitePage(string siteID, string description, string name, string title)
        {
            try
            {
                // Create a new Sharepoint site using Graph api
                // Beta endpoint currently https://graph.microsoft.com/beta/sites/{siteId}/pages

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
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating a new site: {ex}");
                return null;
            }
        }

        public async Task<SitePageCollectionResponse> GetSitePages(string siteID)
        {
            try
            {
                return await _client.Sites[$"{siteID}"].Pages.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing pages: {ex}");
                return null;
            }
        }

        public async Task<SitePage> UpdateSitePage(string siteID, string pageID, string title)
        {
            try
            {
                var requestBody = new SitePage
                {
                    Title = title,
                };
                return await _client.Sites[$"{pageID}"].Pages[$"{siteID}"].PatchAsync(requestBody);
            }
            catch (Exception ex) {
                Console.WriteLine($"Error updating site: {ex}");
                return null;
            }
        }

        public async Task DeleteSitePage(string siteID, string pageID)
        {
            try
            {
                await _client.Sites[$"{siteID}"].Pages[$"{pageID}"].DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating site: {ex}");
            }
        }
    }
}
