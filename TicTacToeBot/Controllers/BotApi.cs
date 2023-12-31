/*
 * Game mediator
 *
 * Контракт сервиса и бота для игры в крестики нолики
 *
 * OpenAPI spec version: 1.0.0
 *
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicTacToeBot.Attributes;
using TicTacToeBot.Bots;
using TicTacToeBot.Models;

namespace TicTacToeBot.Controllers;

[ApiController]
public class BotApiController : ControllerBase
{
    // private readonly ITicTacToeBot _bot;
    private readonly ITicTacToeBot _bot;
    
    public BotApiController(ITicTacToeBot bot)
    {
        _bot = bot;
    }

    /// <summary>
    /// Запрос бота сделать ход
    /// </summary>
    /// <remarks>Бот получает текущее игровое поле и возвращает новое игровое поле (сделав новый ход)</remarks>
    /// <param name="body">Данные, которые получает бот участников хакатона</param>
    /// <response code="200">Бот успешно сходил</response>
    [HttpPost]
    [Route("/bot/turn")]
    [ValidateModelState]
    [SwaggerOperation("MakeAMove")]
    [SwaggerResponse(statusCode: 200, type: typeof(BotTurnResponse), description: "Бот успешно сходил")]
    public virtual IActionResult MakeAMove([FromBody] BotTurnRequest body) =>
        Ok(new BotTurnResponse { game_field = _bot.Turn(body.game_field) });
}