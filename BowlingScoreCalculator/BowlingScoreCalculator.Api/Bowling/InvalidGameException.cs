namespace BowlingScoreCalculator.Api.Bowling;

public class InvalidGameException : Exception
{
    public InvalidGameException(string? message) : base(message)
    {
    }
}
