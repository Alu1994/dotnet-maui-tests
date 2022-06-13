using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

[QueryProperty("EditableMonkey", "EditableMonkey")]
public partial class EditMonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey editableMonkey;
    readonly MonkeyService _monkeyService;

    public EditMonkeyDetailsViewModel(MonkeyService monkeyService)
    {
        _monkeyService = monkeyService;
    }

    [ICommand]
    async Task EditMonkeyAsync()
    {
        var (isSuccess, monkeyReturn) = await _monkeyService.EditMonkeyAsync(editableMonkey);
        if (isSuccess is false) return;
        await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
        {
            ["EditedMonkey"] = editableMonkey
        });
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            ["Monkey"] = editableMonkey
        });
    }
}
