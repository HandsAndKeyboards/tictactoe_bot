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
        _httpClient = new HttpClient { BaseAddress = new Uri(config.mediatorUrl) };
    }

    public async Task<Figure> RegistrationBot()
    {
        var request = new RegisterBotInSessionRequest
            { BotId = _config.botId, BotUrl = _config.botUrl, Password = _config.password };
        var response = await _httpClient.PostAsJsonAsync($"/sessions/{_config.SessionId}/registration", request);
        return response.StatusCode switch
        {
            HttpStatusCode.OK => (await response.Content.ReadFromJsonAsync<RegisterBotInSessionResponse>()).Figure,
            HttpStatusCode.NotFound => throw new Exception("not found"),
            HttpStatusCode.BadRequest => throw new Exception("400"),
            _ => throw new Exception("undefined exception")
        };
    }
}