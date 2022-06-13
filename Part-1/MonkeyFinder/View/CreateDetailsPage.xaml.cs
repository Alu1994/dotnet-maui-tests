namespace MonkeyFinder;

public partial class CreateDetailsPage : ContentPage
{
    private readonly CreateMonkeyDetailsViewModel _viewModel;

    public CreateDetailsPage(CreateMonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        _viewModel.Monkey = null;
        base.OnNavigatingFrom(args);
    }
}
