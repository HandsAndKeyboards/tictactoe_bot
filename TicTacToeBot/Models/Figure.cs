/*
 * Game mediator
 *
 * Контракт сервиса и бота для игры в крестики нолики
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TicTacToeBot.Models
{
  
          /// <summary>
          /// Gets or Sets Figure
          /// </summary>
          [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
          public enum Figure
          {
              /// <summary>
              /// Enum XEnum for x
              /// </summary>
              [EnumMember(Value = "x")]
              X = 0,
              /// <summary>
              /// Enum OEnum for o
              /// </summary>
              [EnumMember(Value = "o")]
              O = 1
          }
}
