using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTests
{
    [Test]
    public void AllStrikesFinishedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = new int[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() => {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(300));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }
}