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
        var rollsArr = rolls.ToArray();
        var runningTotal = 0;
        var scores = new List<int>();
        var finished = true;
        for (int i = 0; i < rollsArr.Length; i += 2)
        {
            if (rollsArr[i] == 10) //strike
            {
                runningTotal += rollsArr[i] + rollsArr[i + 1] + rollsArr[i + 2];
                scores.Add(runningTotal);
                if (i + 2 >= rollsArr.Length - 1)
                {
                    break;
                }
                i--;
                continue;
            }
        }
        return new GameResult(scores) { IsCompleted = finished };
    }
}