namespace SPPageTools;

/**
 * Button interaction logic
 * Author: @Tarzzi
 **/


public partial class MainPage : ContentPage
{
    public MainPage()
	  {
		InitializeComponent();
    }

  private void SetSiteIDHandler(object sender, EventArgs e)
  {
    // find element with x:name = "entryID", change its text to "Hello World"
    var entryCreatePageSiteID = this.FindByName<Entry>("entryCreatePageSiteID");
    var entryListSitePagesID = this.FindByName<Entry>("entryListPagesSiteID");
    var entryUpdatePageSiteID = this.FindByName<Entry>("entryUpdatePageSiteID");
    var entryDeletePageSiteID = this.FindByName<Entry>("entryDeletePageSiteID");
    // get the x:name of the button that was clicked
    var button = (Button)sender;
    entryCreatePageSiteID.Text = button.Text;
    entryListSitePagesID.Text = button.Text;
    entryUpdatePageSiteID.Text = button.Text;
    entryDeletePageSiteID.Text = button.Text;
    }

  private void SetPageIDHandler(object sender, EventArgs e)
  {
    var entryDeletePageID = this.FindByName<Entry>("entryDeletePageID");
    var entryUpdatePageID = this.FindByName<Entry>("entryUpdatePageID");

    var button = (Button)sender;
    entryDeletePageID.Text = button.Text;
    entryUpdatePageID.Text = button.Text;
  }
}

