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
        var runningTotal = 0;
        var scores = new List<int?>();
        for (int i = 0; i < rolls.Length; i += 2)
        {
            if (i + 1 >= rolls.Length)
            {
                if (scores.Count < 10)
                {
                    scores.Add(null); //incomplete frame
                }
                break;
            }
            var score = rolls[i] + rolls[i + 1];

            if (score < 10) //no bonus pts
            {
                runningTotal += score;
                scores.Add(runningTotal);
                continue;
            }

            if (i + 2 >= rolls.Length)
            {
                if (scores.Count < 10) //incomplete after strike or spare
                {
                    scores.AddRange(rolls[i] == 10 ? new int?[] { null, null } : new int?[] { null });
                }
                break;
            }

            score += rolls[i + 2]; //spare

            runningTotal += score;
            scores.Add(runningTotal);

            if (rolls[i] == 10) //strike, frame ends with first roll
            {
                i--;
            }
        }

        return scores;
    }
}