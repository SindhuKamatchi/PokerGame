using Microsoft.AspNetCore.Mvc;
using PokerGame.Api.Mappers;
using PokerGame.Api.Models.Dto;
using PokerGame.Api.Models.Response;
using PokerGame.Application.Interfaces;

namespace PokerGame.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokerController : ControllerBase
    {
        private readonly IPokerGameEngine _engine;
        private readonly ILogger<PokerController> _logger;

        public PokerController(IPokerGameEngine engine, ILogger<PokerController> logger)
        {
            _engine = engine;
            _logger = logger;
        }

        [HttpPost("score")]
        [ProducesResponseType(typeof(List<HandScoreResponse>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult ScoreHands([FromBody] List<HandDto> handDtos)
        {
            if (handDtos == null || handDtos.Count == 0)
            {
                _logger.LogWarning("ScoreHands called with no input.");
                return BadRequest(new ErrorResponse { Error = "No hands provided." });
            }

            try
            {
                var hands = handDtos.Select(HandMapper.ToDomain).ToList();
                var scores = _engine.ScoreHands(hands);

                var result = scores.Select(kvp => new HandScoreResponse
                {
                    HandNo = kvp.Key.HandNo,
                    Score = kvp.Value.Description
                }).ToList();

                _logger.LogInformation("Scored {Count} hands successfully.", hands.Count);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error scoring hands.");
                return BadRequest(new ErrorResponse { Error = ex.Message });
            }
        }

        [HttpPost("winner")]
        [ProducesResponseType(typeof(WinnerResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public IActionResult DetermineWinner([FromBody] List<HandDto> handDtos)
        {
            if (handDtos == null || handDtos.Count == 0)
            {
                _logger.LogWarning("DetermineWinner called with no input.");
                return BadRequest(new ErrorResponse { Error = "No hands provided." });
            }

            try
            {
                var hands = handDtos.Select(HandMapper.ToDomain).ToList();
                var winner = _engine.DetermineWinner(hands);
                var score = _engine.ScoreHands(hands)[winner];

                _logger.LogInformation("Winner determined: HandNo {HandNo} with {Score}.", winner.HandNo, score.Description);
                return Ok(new WinnerResponse
                {
                    Winner = winner.HandNo,
                    Score = score.Description
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error determining winner.");
                return BadRequest(new ErrorResponse { Error = ex.Message });
            }
        }
    }
}