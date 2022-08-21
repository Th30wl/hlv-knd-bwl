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
        var scores = new List<int?>();
        var finished = true;
        for (int i = 0; i < rollsArr.Length; i += 2)
        {
            if (i + 1 >= rollsArr.Length)
            {
                if (scores.Count < 10)
                {
                    scores.Add(null);
                }
                break;
            }
            var score = rollsArr[i] + rollsArr[i + 1];

            if (score < 10) //no bonus pts
            {
                runningTotal += score;
                scores.Add(runningTotal);
                continue;
            }
            
            if (i + 2 >= rollsArr.Length)
            {
                if (scores.Count < 10)
                {
                    scores.Add(null);
                }
                break;
            }

            score += rollsArr[i + 2]; //spare

            runningTotal += score;
            scores.Add(runningTotal);

            if (rollsArr[i] == 10) //strike, frame ends with first roll
            {
                i--;
            }
        }
        if (scores.Count < 10 || scores.Last() == null)
        {
            finished = false;
        }
        return new GameResult(scores) { IsCompleted = finished };
    }
}