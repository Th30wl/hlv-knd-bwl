using BowlingScoreCalculator.Api.Bowling;
using Microsoft.Extensions.Logging.Abstractions;

namespace BowlingScoreCalculator.Tests;

public class CalculatorTestsForInvalidGame
{
    private Calculator calc;
    private const string tooMuchRolls = "Game has more rolls than allowed";
    private const string tooMuchPins = "Game has rolls with more pins than allowed";

    [SetUp]
    public void SetUp()
    {
        calc = new Calculator(new NullLogger<Calculator>());
    }

    [Test]
    public void TooMuchRollsAllStrikes()
    {
        var rolls = Enumerable.Repeat(10, 13).ToArray();
        var ex = Assert.Throws<InvalidGameException>(() =>
        {
            calc.Calculate(rolls);
        });
        Assert.That(ex.Message, Is.EqualTo(tooMuchRolls));
    }

    [Test]
    public void TooMuchRollsAllSpares()
    {
        var rolls = Enumerable.Repeat(5, 22).ToArray();
        var ex = Assert.Throws<InvalidGameException>(() =>
        {
            calc.Calculate(rolls);
        });
        Assert.That(ex.Message, Is.EqualTo(tooMuchRolls));
    }

    [Test]
    public void TooMuchRollsNoBonus()
    {
        var rolls = Enumerable.Repeat(3, 21).ToArray();
        var ex = Assert.Throws<InvalidGameException>(() =>
        {
            calc.Calculate(rolls);
        });
        Assert.That(ex.Message, Is.EqualTo(tooMuchRolls));
    }

    [Test]
    public void TooMuchPinsInFrame()
    {
        var rolls = new int[] { 2, 4, 5, 8, 6, 3, 10, 10, 8, 1, 5, 5, 10, 7, 2, 4, 5 };
        var ex = Assert.Throws<InvalidGameException>(() =>
        {
            calc.Calculate(rolls);
        });
        Assert.That(ex.Message, Is.EqualTo(tooMuchPins));
    }
}