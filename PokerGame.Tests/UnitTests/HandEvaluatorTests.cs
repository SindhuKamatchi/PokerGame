using PokerGame.Application.Interfaces;
using PokerGame.Domain;
using PokerGame.Infrastructure.Evaluators;

namespace PokerGame.Tests.UnitTests
{
    public class HandEvaluatorTests
    {
        private readonly IHandEvaluator _evaluator = new HandEvaluator();
        [Theory]
        [InlineData("Ten,Jack,Queen,King,Ace", "Heart,Heart,Heart,Heart,Heart", HandRank.RoyalFlush)]
        [InlineData("2,3,4,5,6", "Spade,Spade,Spade,Spade,Spade", HandRank.StraightFlush)]
        [InlineData("Ace,2,3,4,5", "Club,Heart,Diamond,Spade,Club", HandRank.Straight)]
        public void EvaluateHand_ShouldReturnCorrectRank(string valuesCsv, string suitsCsv, HandRank expectedRank)
        {
            var values = valuesCsv.Split(',');
            var suits = suitsCsv.Split(',');

            var cards = values.Select((v, i) =>
                new Card(Enum.Parse<SuitValue>(v, true), Enum.Parse<Suit>(suits[i], true))
            );

            var hand = new Hand(1, cards);
            var score = _evaluator.Evaluate(hand);

            Assert.Equal(expectedRank, score.Rank);
        }
    }
}
