using BowlingScoreCalculator.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingScoreCalculator.Api.Controllers;

[ApiController]
[Route("/scores")]
public class ScoresController : ControllerBase
{
    private readonly ILogger<ScoresController> logger;

    public ScoresController(ILogger<ScoresController> logger)
    {
        this.logger = logger;
    }

    [HttpPost]
    public CalculationResponse Post(CalculationRequest request)
    {
        throw new NotImplementedException();
    }
}