using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient _httpClient;
    IReadOnlyCollection<Monkey> _monkeys;
    const string URL = "https://d169-2804-431-cff3-d4db-b42c-a2a8-c1d4-fcb.ngrok.io/monkeys";

    public MonkeyService()
    {
        _httpClient = new HttpClient();
        _monkeys = new List<Monkey>();
    }

    public async Task<IReadOnlyCollection<Monkey>> GetMonkeysAsync()
    {
        var response = await _httpClient.GetAsync(URL);
        if (response.IsSuccessStatusCode)
            _monkeys = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        return _monkeys;
    }

    public async Task<(bool, Monkey)> CreateMonkeyAsync(Monkey monkey)
    {
        if (monkey is null) return (false, default);
        var response = await _httpClient.PostAsJsonAsync(URL, monkey);
        if (response.IsSuccessStatusCode is false) return (false, default);
        var monkeyResult = await response.Content.ReadFromJsonAsync<Monkey>();
        return (response.IsSuccessStatusCode, monkeyResult);
    }

    public async Task<(bool, Monkey)> EditMonkeyAsync(Monkey monkey)
    {
        if (monkey is null) return (false, default);
        var response = await _httpClient.PutAsJsonAsync(URL, monkey);
        if (response.IsSuccessStatusCode is false) return (false, default);
        var monkeyResult = await response.Content.ReadFromJsonAsync<Monkey>();
        return (response.IsSuccessStatusCode, monkeyResult);
    }
}
