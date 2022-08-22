namespace BowlingScoreCalculator.Api.Bowling;

public interface ICalculator
{
    GameResult Calculate(IEnumerable<uint> rolls);
}