namespace SPSiteTools;

public partial class MainPage : ContentPage
{
    public MainPage()
	  {
		InitializeComponent();

    }

  private void SetSiteID_Handler(object sender, EventArgs e)
  {
    // find element with x:name = "entryID", change its text to "Hello World"
    var entryID = this.FindByName<Entry>("entryID");
    var entryListSitesID = this.FindByName<Entry>("entryListPagesSiteID");
    var entryUpdatePageSiteID = this.FindByName<Entry>("entryUpdatePageSiteID");
    var entryDeletePageSiteID = this.FindByName<Entry>("entryDeletePageID");
    // get the x:name of the button that was clicked
    var button = (Button)sender;
    entryID.Text = button.Text;
    entryListSitesID.Text = button.Text;
    entryUpdatePageSiteID.Text = button.Text;
    entryDeletePageSiteID.Text = button.Text;


    }

  private void SetPageID_Handler(object sender, EventArgs e)
  {
    var entryDeletePageID = this.FindByName<Entry>("entryDeletePageID");
    var entryUpdatePageID = this.FindByName<Entry>("entryUpdatePageID");

    var button = (Button)sender;
    entryDeletePageID.Text = button.Text;
    entryUpdatePageID.Text = button.Text;
  }
}

