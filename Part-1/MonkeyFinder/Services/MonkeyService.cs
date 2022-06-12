using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient _httpClient;
    IReadOnlyCollection<Monkey> _monkeys;
    const string URL = "https://17e3-2804-431-cff2-d841-6052-bd3c-a350-8a45.ngrok.io/monkeys";

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

    public async Task<bool> CreateMonkeyAsync(Monkey monkey)
    {
        if (monkey is null) return false;
        var response = await _httpClient.PostAsJsonAsync(URL, monkey);
        return response.IsSuccessStatusCode;
    }
}
