namespace BowlingScoreCalculator.Api.Bowling;

public class GameResult
{
    public bool IsCompleted { get; set; }
    public IEnumerable<uint?> Scores { get; set; }

    public GameResult(IEnumerable<uint?> scores)
    {
        Scores = scores;
    }
}