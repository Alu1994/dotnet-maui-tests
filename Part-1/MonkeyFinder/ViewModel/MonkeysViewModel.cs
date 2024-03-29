﻿using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

[QueryProperty("CreatedMonkey", "CreatedMonkey")]
[QueryProperty("EditedMonkey", "EditedMonkey")]
public partial class MonkeysViewModel : BaseViewModel
{
    IConnectivity _connectivity;
    IGeolocation _geolocation;
    MonkeyService _monkeyService;

    [ObservableProperty]
    bool isRefreshing;
    [ObservableProperty]
    Monkey createdMonkey;
    [ObservableProperty]
    Monkey editedMonkey;

    public ObservableCollection<Monkey> Monkeys { get; } = new();

    public MonkeysViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        _monkeyService = monkeyService;
        _connectivity = connectivity;
        _geolocation = geolocation;
    }

    [ICommand]
    async Task GetClosestMonkeyAsync()
    {
        if (IsBusy || Monkeys.Count is 0) return;

        try
        {
            var permitionStatus = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            var location = await _geolocation.GetLastKnownLocationAsync();
            if(location is null)
            {
                location = await _geolocation.GetLocationAsync(
                    new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
            }

            if (location is null) return;

            var first = Monkeys.OrderBy(m =>
                location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles))
                .FirstOrDefault();

            if (first is null) return;

            await Shell.Current.DisplayAlert("Closest Monkey",
                $"{first.Name} in {first.Location}", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get closest monkeys: {ex.Message}", "OK");
        }
    }

    [ICommand]
    async Task GoToDetailsAsync(Monkey monkey)
    {
        if (monkey is null) return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true,
            new Dictionary<string, object>
            {
                { "Monkey", monkey }
            });
    }

    [ICommand]
    async Task GetMonkeysAsync()
    {
        if (IsBusy) return;

        try
        {
            if(_connectivity.NetworkAccess is not NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Issue",
                    $"Check your internet and try again!", "OK");
                return;
            }

            IsBusy = true;
            var monkeys = await _monkeyService.GetMonkeysAsync();
            if (Monkeys.Count > 0) Monkeys.Clear();

            foreach (var monkey in monkeys) Monkeys.Add(monkey);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!",
                $"Unable to get monkeys: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [ICommand]
    async Task CreateMonkeyAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(CreateDetailsPage)}", true);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (createdMonkey is not null)
        {
            if (Monkeys.Any(x => x.Id == createdMonkey.Id))
            {
                createdMonkey = null;
                editedMonkey = null;
                base.OnPropertyChanged(e);
                return;
            }
            Monkeys.Add(new Monkey(createdMonkey));
        }
        if (editedMonkey is not null)
        {
            Monkeys.Remove(Monkeys.FirstOrDefault(x => x.Id == editedMonkey.Id));
            Monkeys.Add(new Monkey(editedMonkey));
        }
        CreatedMonkey = null;
        EditedMonkey = null;
        base.OnPropertyChanged(e);
    }
}
