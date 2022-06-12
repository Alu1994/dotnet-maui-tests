namespace MonkeyFinder;

public partial class CreateDetailsPage : ContentPage
{
	public CreateDetailsPage(CreateMonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
