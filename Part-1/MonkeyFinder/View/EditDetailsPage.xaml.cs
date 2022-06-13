namespace MonkeyFinder;

public partial class EditDetailsPage : ContentPage
{
	public EditDetailsPage(EditMonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
