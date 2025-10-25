using Microsoft.Extensions.Logging;
using PokerGame.Application.Interfaces;
using PokerGame.Domain;
using PokerGame.Infrastructure.Comparers;
using System.Linq;

namespace PokerGame.Application
{
    public class PokerGameEngine :IPokerGameEngine
    {
        private readonly IHandEvaluator _evaluator;
        private readonly IHandValidator _validator;
        private readonly ILogger<PokerGameEngine> _logger;

        public PokerGameEngine(IHandEvaluator evaluator, IHandValidator validator, ILogger<PokerGameEngine> logger)
        {
            _evaluator = evaluator;
            _validator = validator;
            _logger = logger;
        }

        public Dictionary<int, HandScore> ScoreHands(List<Hand> hands)
        {
            if (hands == null || hands.Count == 0)
            {
                _logger.LogWarning("No hands provided for scoring.");
                throw new ArgumentException("At least one hand must be provided.");
            }

            foreach (var hand in hands)
            {
                try
                {
                    _validator.Validate(hand);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Validation failed for HandNo {hand.HandNo}");
                    throw new Exception($"Hand {hand.HandNo} is invalid: {ex.Message}", ex);
                }
            }

            return hands.ToDictionary(h => h.HandNo, h => _evaluator.Evaluate(h));
        }

        public Hand DetermineWinner(List<Hand> hands)
        {
            var handMap = hands.ToDictionary(h => h.HandNo);
            var scores = ScoreHands(hands);
            var sortedHandNos = scores.Keys
                .OrderBy(handNo => handNo, new HandComparer(scores, handMap))
                .ToList();


            var winningHandNo = sortedHandNos.First();
            var winningHand = handMap[winningHandNo];
            var winningScore = scores[winningHandNo];

            _logger.LogInformation(
                "Winning HandNo: {HandNo} | Rank: {Rank} | HighCard: {HighCard}",
                winningHand.HandNo,
                winningScore.Rank,
                winningScore.HighCard
            );

            return winningHand;
        }
    }
}