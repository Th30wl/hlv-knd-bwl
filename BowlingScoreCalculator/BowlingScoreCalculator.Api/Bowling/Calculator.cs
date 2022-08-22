namespace BowlingScoreCalculator.Api.Bowling;

public class Calculator
{
    private readonly ILogger<Calculator> logger;

    public Calculator(ILogger<Calculator> logger)
    {
        this.logger = logger;
        logger.LogDebug($"Created instance of {nameof(Calculator)}");
    }

    public GameResult Calculate(IEnumerable<int> rolls)
    {
        List<int?> scores = CalculateScores(rolls.ToArray());
        return new GameResult(scores) { IsCompleted = scores.Count == 10 && scores.Last() != null };
    }

    private static List<int?> CalculateScores(int[] rolls)
    {
        int? runningTotal = 0;
        var scores = new List<int?>();
        for (int i = 0; i < rolls.Length; i += 2)
        {
            if (scores.Count == 10)
            {
                break;
            }
            int? current = rolls[i];
            int? next1 = i + 1 < rolls.Length ? rolls[i + 1] : null;
            int? next2 = i + 2 < rolls.Length ? rolls[i + 2] : null;

            var score = current + next1;

            if (score < 10) //no bonus pts
            {
                runningTotal += score;
                scores.Add(runningTotal);
                continue;
            }

            score += next2; //spare
            
            runningTotal += score;
            scores.Add(runningTotal);

            if (current == 10) //strike, frame ends with first roll
            {
                i--;
            }
        }

        return scores;
    }
}