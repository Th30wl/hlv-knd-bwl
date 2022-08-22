using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTestsForIncompleteGame
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
        var rolls = Enumerable.Repeat<uint>(10, 10).ToArray();
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x=> x != null), Is.EqualTo(240));
            Assert.That(result.Scores.Count(), Is.EqualTo(10));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }

    [Test]
    public void AllSpares()
    {
        var rolls = Enumerable.Repeat<uint>(5, 20).ToArray();
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
        var rolls = Enumerable.Repeat<uint>(3, 6).ToArray();
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
        var rolls = Enumerable.Repeat<uint>(3, 7).ToArray();
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
        var rolls = Enumerable.Repeat<uint>(0, 6).ToArray();
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
        var rolls = new uint[] { 2, 4, 5, 5, 6, 3, 10, 10, 8, 1, 5 };
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
        var rolls = new uint[] { 1, 1, 1, 1, 9, 1, 2, 8, 9, 1, 10, 10 };
        var result = calc.Calculate(rolls);
        Assert.Multiple(() =>
        {
            Assert.That(result.IsCompleted, Is.False);
            Assert.That(result.Scores.Last(x => x != null), Is.EqualTo(55));
            Assert.That(result.Scores.Count(), Is.EqualTo(7));
            Assert.That(result.Scores.Last(), Is.Null);
        });
    }
}