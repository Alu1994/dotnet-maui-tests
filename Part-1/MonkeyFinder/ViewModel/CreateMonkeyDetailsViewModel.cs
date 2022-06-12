using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class CreateMonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey monkey;
    readonly MonkeyService _monkeyService;

    public CreateMonkeyDetailsViewModel(MonkeyService monkeyService)
    {
        _monkeyService = monkeyService;
        monkey = new Monkey();
    }

    [ICommand]
    async Task CreateMonkeyAsync()
    {
        var isSuccess = await _monkeyService.CreateMonkeyAsync(monkey);
        if (isSuccess is false) return;
        MauiProgram.MonkeysList.Add(monkey);
        await Shell.Current.GoToAsync("..", true);
    }
}
