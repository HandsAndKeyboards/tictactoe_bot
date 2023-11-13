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
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using TicTacToeBot.Attributes;
using TicTacToeBot.Models;

namespace TicTacToeBot.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
public class BotApiController : ControllerBase
{
    private readonly IMemoryCache _cache;

    public BotApiController(IMemoryCache cache)
    {
        _cache = cache;
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
    public virtual IActionResult MakeAMove([FromBody]BotTurnRequest body)
    {
        var figure = _cache.Get<Figure>("figure");
        //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
        // return StatusCode(200, default(BotTurnResponse));
        string? exampleJson = null;
        exampleJson = "{\n  \"game_field\" : \"ooo___oo_xoxo__xoo_o_oxx_oo_x______o_______x___oo_x_x_o_x_______oxo___o_x__o_o______x__o_____x__o_____o______x_____xoxoo___xo_____o__x_x__________x__x____o_xo__x__o___x_______o_x______xo______oxo_x_xx__xox___ox____x_oo__ox_x_x___o__________x______________o_____x____o___x___xo___x__x_xo__x_x___ox___x_______x____x_o_x__x_o__ox__o__x__ox_x_____x_oo_____x____o_ox\"\n}";
            
        var example = JsonConvert.DeserializeObject<BotTurnResponse>(exampleJson);            //TODO: Change the data returned
        return new ObjectResult(example);
    }
}