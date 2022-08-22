namespace BowlingScoreCalculator.Api.Bowling;

public class Calculator : ICalculator
{
    private readonly ILogger<Calculator> logger;
    private const string tooMuchRolls = "Game has more rolls than allowed";
    private const string tooMuchPins = "Game has rolls with more pins than allowed";

    public Calculator(ILogger<Calculator> logger)
    {
        this.logger = logger;
        logger.LogDebug($"Created instance of {nameof(Calculator)}");
    }

    public GameResult Calculate(IEnumerable<uint> rolls)
    {
        List<uint?> scores = CalculateScores(rolls.ToArray());
        return new GameResult(scores) { IsCompleted = scores.Count == 10 && scores.Last() != null };
    }

    private static List<uint?> CalculateScores(uint[] rolls)
    {
        uint? runningTotal = 0;
        var scores = new List<uint?>();
        for (int i = 0; i < rolls.Length; i += 2)
        {
            if (scores.Count == 10)
            {
                ValidateRollCount(rolls, i);
                break;
            }
            uint? current = rolls[i];
            uint? next1 = i + 1 < rolls.Length ? rolls[i + 1] : null;
            uint? next2 = i + 2 < rolls.Length ? rolls[i + 2] : null;

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

    private static void ValidateNumberOfPins(uint? current, uint? score)
    {
        if (current < 10 && score > 10)
        {
            throw new InvalidGameException(tooMuchPins);
        }
    }

    private static void ValidateRollCount(uint[] rolls, int rollIndex)
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