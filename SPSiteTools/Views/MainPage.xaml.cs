namespace SPSiteTools;

public partial class MainPage : ContentPage
{
    public MainPage()
	  {
		InitializeComponent();

    }

  private void Button_Clicked(object sender, EventArgs e)
  {
    // find element with x:name = "entryID", change its text to "Hello World"
    var entryID = this.FindByName<Entry>("entryID");
    // get the x:name of the button that was clicked
    var button = (Button)sender;
    entryID.Text = button.Text;

    }
}

