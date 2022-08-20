namespace BowlingScoreCalculator.Api.Bowling;

public class GameResult
{
    public bool IsCompleted { get; set; }
    public IEnumerable<int> Scores { get; set; }

    public GameResult(IEnumerable<int> scores)
    {
        Scores = scores;
    }
}