
namespace BowlingScoreCalculator.Api.Models;

/// <summary>
/// Game calculation request data
/// </summary>
public class CalculationRequest
{
    /// <summary>
    /// Array containing the number of pins downed with each roll
    /// </summary>
    public uint[] PinsDowned { get; set; }
}
