using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTests
{
    [Test]
    public void AllStrikesFinishedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(10, 12).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() => {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(300));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void AllSparesFinishedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(5, 21).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() => {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(150));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void NoBonusFinishedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(3, 20).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() => {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(60));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void GutterBallFinishedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(0, 20).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() => {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(0));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }
}