using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTestsForCompleteGame
{
    private Calculator calc;

    [SetUp]
    public void SetUp()
    {
        calc = new Calculator(new NullLogger<Calculator>());
    }

    [Test]
    public void AllStrikes()
    {
        var rolls = Enumerable.Repeat<uint>(10, 12).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(300));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void AllSpares()
    {
        var rolls = Enumerable.Repeat<uint>(5, 21).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(150));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void NoBonusFrames()
    {
        var rolls = Enumerable.Repeat<uint>(3, 20).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(60));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void GutterBall()
    {
        var rolls = Enumerable.Repeat<uint>(0, 20).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(0));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void MixedGame()
    {
        var rolls = new uint[] { 2, 4, 5, 5, 6, 3, 10, 10, 8, 1, 5, 5, 10, 7, 2, 4, 5 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(144));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void MixedGameFinishedWithStrikes()
    {
        var rolls = new uint[] { 10, 9, 1, 0, 2, 3, 5, 6, 4, 2, 8, 6, 3, 3, 5, 5, 4, 10, 10, 10 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(124));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }

    [Test]
    public void MixedGameFinishedWithSparesAndStrike()
    {
        var rolls = new uint[] { 9, 1, 2, 4, 6, 4, 3, 3, 0, 10, 9, 1, 3, 6, 5, 5, 4, 3, 1, 9, 10 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.True);
            Assert.That(result.Scores.Last(), Is.EqualTo(119));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
        });
    }
}