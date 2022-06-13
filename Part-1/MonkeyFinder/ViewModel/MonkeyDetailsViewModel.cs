namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    IMap _map;
    public MonkeyDetailsViewModel(IMap map)
    {
        _map = map;
    }

    [ObservableProperty]
    Monkey monkey;

    [ICommand]
    async Task OpenMapAsync()
    {
        try
        {
            await _map.OpenAsync(Monkey.Latitude, Monkey.Longitude,
                new MapLaunchOptions
                {
                    Name = Monkey.Name,
                    NavigationMode = NavigationMode.None
                });
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to open map: {ex.Message}", "OK");
        }
    }

    [ICommand]
    async Task EditMonkeyAsync()
    {
        await Shell.Current.GoToAsync("..", true);
        await Shell.Current.GoToAsync(nameof(EditDetailsPage), true, new Dictionary<string, object>
        {
            ["EditableMonkey"] = monkey
        });
    }
}
