using System.Net;
using TicTacToeBot.Config;
using TicTacToeBot.Models;

namespace TicTacToeBot.Services;

public class MediatorService
{
    private readonly BotConfig _config;
    private HttpClient _httpClient;

    public MediatorService(BotConfig config)
    {
        _config = config;
        _httpClient = new HttpClient { BaseAddress = new Uri(config.MediatorUrl) };
    }

    public async Task<char> RegistrationBot()
    {
        var request = new RegisterBotInSessionRequest
        { 
            BotId = _config.BotId, 
            BotUrl = _config.BotUrl, 
            Password = _config.Password
        }; 
        var response = await _httpClient.PostAsJsonAsync($"/sessions/{_config.SessionId}/registration", request);

        return response.StatusCode switch
        {
            HttpStatusCode.OK => (await response.Content.ReadFromJsonAsync<RegisterBotInSessionResponse>()).Figure,
            HttpStatusCode.NotFound => throw new Exception($"not found {await response.Content.ReadAsStringAsync()}"),
            HttpStatusCode.BadRequest => throw new Exception($"400 {await response.Content.ReadAsStringAsync()}"),
            _ => throw new Exception($"undefined exception {await response.Content.ReadAsStringAsync()}")
        };
    }
}