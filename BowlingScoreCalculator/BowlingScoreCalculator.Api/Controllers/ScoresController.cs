using BowlingScoreCalculator.Api.Bowling;
using BowlingScoreCalculator.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingScoreCalculator.Api.Controllers;

/// <summary>
/// Bowling score calculator
/// </summary>
[ApiController]
[Route("/scores")]
public class ScoresController : ControllerBase
{
    private readonly ILogger<ScoresController> logger;
    private readonly ICalculator calculator;

    public ScoresController(ILogger<ScoresController> logger, ICalculator calculator)
    {
        logger.LogDebug($"Created instance of {nameof(ScoresController)}");
        this.logger = logger;
        this.calculator = calculator;
    }

    /// <summary>
    /// Calculates the score of a bowling game
    /// </summary>
    /// <param name="request">Information about the pins downed</param>
    /// <returns>Calculated scores and info if the game is completed and valid</returns>
    [HttpPost]
    public CalculationResponse Post(CalculationRequest request)
    {
        try
        {
            var result = calculator.Calculate(request.PinsDowned);
            return new CalculationResponse
            {
                FrameProgressScores = result.Scores.Select(x => x?.ToString() ?? "*"),
                GameCompleted = result.IsCompleted
            };
        }
        catch (Exception e)
        {
            logger.LogError("An error occured while calculating game score", e);
            return new CalculationResponse { GameValid = false };
        }
    }
}