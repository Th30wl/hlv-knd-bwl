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
            var score = rollsArr[i] + rollsArr[i + 1];
            if (score < 10) //no bonus pts
            {
                runningTotal += score;
                scores.Add(runningTotal);
                continue;
            }

            score += rollsArr[i + 2]; //spare
            runningTotal += score;
            scores.Add(runningTotal);

            if (i + 2 >= rollsArr.Length - 1)
            {
                break;
            }
            if (rollsArr[i] == 10) //strike, frame ends with first roll
            {
                i--;
            }
        }
        return new GameResult(scores) { IsCompleted = finished };
    }
}