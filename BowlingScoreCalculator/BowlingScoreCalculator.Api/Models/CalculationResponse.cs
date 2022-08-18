namespace BowlingScoreCalculator.Api.Models;

public class CalculationResponse
{
    public IEnumerable<short> FrameProgressScores { get; set; }

    public CalculationResponse(IEnumerable<short> frameProgressScores)
    {
        FrameProgressScores = frameProgressScores;
    }

    public bool GameCompleted { get; set; }
}
