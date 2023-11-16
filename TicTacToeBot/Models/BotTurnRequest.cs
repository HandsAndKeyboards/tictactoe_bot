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
using System.Text;
using Newtonsoft.Json;

namespace TicTacToeBot.Models;

/// <summary>
/// 
/// </summary>
[DataContract]
public class BotTurnRequest : IEquatable<BotTurnRequest>
{ 
    /// <summary>
    /// Gets or Sets GameField
    /// </summary>

    [DataMember(Name="game_field")]
    public string game_field { get; set; }

    /// <summary>
    /// Returns the string presentation of the object
    /// </summary>
    /// <returns>String presentation of the object</returns>
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("class BotTurnRequest {\n");
        sb.Append("  GameField: ").Append(game_field).Append('\n');
        sb.Append("}\n");
        return sb.ToString();
    }

    /// <summary>
    /// Returns the JSON string presentation of the object
    /// </summary>
    /// <returns>JSON string presentation of the object</returns>
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }

    /// <summary>
    /// Returns true if objects are equal
    /// </summary>
    /// <param name="obj">Object to be compared</param>
    /// <returns>Boolean</returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((BotTurnRequest)obj);
    }

    /// <summary>
    /// Returns true if BotTurnRequest instances are equal
    /// </summary>
    /// <param name="other">Instance of BotTurnRequest to be compared</param>
    /// <returns>Boolean</returns>
    public bool Equals(BotTurnRequest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return 
            game_field == other.game_field ||
            game_field.Equals(other.game_field);
    }

    /// <summary>
    /// Gets the hash code
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            var hashCode = 41;
            // Suitable nullity checks etc, of course :)
            hashCode = hashCode * 59 + game_field.GetHashCode();
            return hashCode;
        }
    }

    #region Operators
#pragma warning disable 1591

    public static bool operator ==(BotTurnRequest left, BotTurnRequest right) => Equals(left, right);

    public static bool operator !=(BotTurnRequest left, BotTurnRequest right) => !Equals(left, right);

#pragma warning restore 1591
    #endregion Operators
}