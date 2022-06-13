using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

[QueryProperty("Monkey", "Monkey")]
public partial class CreateMonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey monkey;
    readonly MonkeyService _monkeyService;

    public CreateMonkeyDetailsViewModel(MonkeyService monkeyService)
    {
        _monkeyService = monkeyService;
        monkey = new();
    }

    [ICommand]
    async Task CreateMonkeyAsync()
    {
        var (isSuccess, monkeyReturn) = await _monkeyService.CreateMonkeyAsync(monkey);
        if (isSuccess is false) return;

        await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
        {
            ["CreatedMonkey"] = new Monkey(monkeyReturn)
        });
        monkey = null;
        monkeyReturn = null;
    }

    void FuckYou()
    {
        monkey = null;
    }
}
