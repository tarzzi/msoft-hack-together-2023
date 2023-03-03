using CommunityToolkit.Mvvm.Messaging;

namespace SPSiteTools;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
        WeakReferenceMessenger.Default.Register<CountChangedMessage>(this, (r, m) =>
        {
            CounterBtn.Text = m.Text;
            SemanticScreenReader.Announce(m.Text);
        });
    }

}

