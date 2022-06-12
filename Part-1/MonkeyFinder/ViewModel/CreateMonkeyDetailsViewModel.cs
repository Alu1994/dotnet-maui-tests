namespace MonkeyFinder.ViewModel;

public partial class CreateMonkeyDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Monkey monkey;

    public CreateMonkeyDetailsViewModel()
    {
        monkey = new Monkey 
        { 
            Name = "Little Monkey",
            Latitude = -23.5489,
            Longitude = -46.6388,
            Location = "São Paulo",
            Details = "Mora em sp e ta xônado na bia",
            Population = 1,
            Image = "https://s2.glbimg.com/jqFu8xPTB7cyl57MOtcJMw1qhrY=/0x0:503x483/984x0/smart/filters:strip_icc()/i.s3.glbimg.com/v1/AUTH_59edd422c0c84a879bd37670ae4f538a/internal_photos/bs/2022/H/F/qVVZjpRN2fwAIliJar3w/macaco-joi.jpeg"
        };
    }

    [ICommand]
    async Task CreateMonkeyAsync()
    {
        MauiProgram.MonkeysList.Add(monkey);
        await Shell.Current.GoToAsync("..", true);
    }
}
