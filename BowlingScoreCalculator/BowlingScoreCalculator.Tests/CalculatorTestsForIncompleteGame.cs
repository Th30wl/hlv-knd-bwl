using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTestsForIncompleteGame
{
    [Test]
    public void AllStrikes()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(10, 11).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x=> x != null), Is.EqualTo(270));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }

    [Test]
    public void AllSpares()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(5, 20).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x => x != null), Is.EqualTo(135));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }

    [Test]
    public void NoBonusFrames()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(3, 6).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(), Is.EqualTo(18));
            Assert.That(result.Scores.Count(), Is.EqualTo(3));
        });
    }

    [Test]
    public void NoBonusFramesUnfinishedFrame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(3, 7).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x => x != null), Is.EqualTo(18));
            Assert.That(result.Scores.Count(), Is.EqualTo(4));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }

    [Test]
    public void GutterBall()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = Enumerable.Repeat(0, 6).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(), Is.EqualTo(0));
            Assert.That(result.Scores.Count(), Is.EqualTo(3));
        });
    }

    [Test]
    public void MixedGameWithUnfinishedFrame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = new int[] { 2, 4, 5, 5, 6, 3, 10, 10, 8, 1, 5 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x => x != null), Is.EqualTo(87));
            Assert.That(result.Scores.Count(), Is.EqualTo(7));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }

    [Test]
    public void MixedGame()
    {
        var calc = new Calculator(new NullLogger<Calculator>());
        var rolls = new int[] { 2, 4, 5, 5, 6, 3, 10, 10, 8, 1, 5, 5, 10, 7, 2 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(), Is.EqualTo(135));
            Assert.That(result.Scores.Count(), Is.EqualTo(9));
        });
    }
}