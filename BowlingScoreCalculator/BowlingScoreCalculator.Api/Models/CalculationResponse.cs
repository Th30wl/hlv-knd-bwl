namespace BowlingScoreCalculator.Api.Models;

/// <summary>
/// Game calculation response data
/// </summary>
public class CalculationResponse
{
    /// <summary>
    /// Calculated scores per each frame
    /// </summary>
    public IEnumerable<string>? FrameProgressScores { get; set; }

    /// <summary>
    /// Indicates if the game has completed
    /// </summary>
    public bool? GameCompleted { get; set; }

    /// <summary>
    /// Indicates if the game has valid data
    /// </summary>
    public bool GameValid { get; set; } = true;
}