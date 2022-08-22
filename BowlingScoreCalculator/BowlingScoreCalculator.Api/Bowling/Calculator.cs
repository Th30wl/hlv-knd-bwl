namespace BowlingScoreCalculator.Api.Bowling;

public class Calculator
{
    private readonly ILogger<Calculator> logger;
    private const string tooMuchRolls = "Game has more rolls than allowed";
    private const string tooMuchPins = "Game has rolls with more pins than allowed";

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
                ValidateRollCount(rolls, i);
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

            ValidateNumberOfPins(current, score);

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

    private static void ValidateNumberOfPins(int? current, int? score)
    {
        if (current < 10 && score > 10)
        {
            throw new InvalidGameException(tooMuchPins);
        }
    }

    private static void ValidateRollCount(int[] rolls, int rollIndex)
    {
        if (rollIndex + 2 < rolls.Length)
        {
            throw new InvalidGameException(tooMuchRolls);
        }
        if (rollIndex + 2 == rolls.Length && rolls[rollIndex] < 10)
        {
            throw new InvalidGameException(tooMuchRolls);
        }
        if (rollIndex + 1 == rolls.Length && rolls[rollIndex] + rolls[rollIndex - 1] < 10)
        {
            throw new InvalidGameException(tooMuchRolls);
        }
    }
}