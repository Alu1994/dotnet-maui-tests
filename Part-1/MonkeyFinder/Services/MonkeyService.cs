using System.Net.Http.Json;

namespace MonkeyFinder.Services;

public class MonkeyService
{
    HttpClient httpClient;

    public MonkeyService()
    {
        httpClient = new HttpClient();
    }

    List<Monkey> monkeys = new();
    public async Task<List<Monkey>> GetMonkeysAsync()
    {
        if (monkeys?.Count > 0)
            return monkeys;

        var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            monkeys = await response.Content.ReadFromJsonAsync<List<Monkey>>();
        }

        return monkeys;
    }
}
