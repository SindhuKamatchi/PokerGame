using PokerGame.Application.Interfaces;
using PokerGame.Domain;

namespace PokerGame.Infrastructure.Evaluators
{
    public class HandEvaluator : IHandEvaluator
    {
        public HandScore Evaluate(Hand hand)
        {
            var sortedCards = hand.Cards.OrderByDescending(c => c.Value).ToList();
            var values = sortedCards.Select(c => c.Value).ToList();
            var suits = sortedCards.Select(c => c.Suit).ToList();

            bool isFlush = IsFlush(suits);
            bool isStraight = IsStraight(values, out SuitValue straightHighCard);

            var grouped = GroupCardsByValue(sortedCards);
            var counts = grouped.Select(g => g.Count()).ToList();
            var groupedValues = grouped.Select(g => g.Key).ToList();

            if (IsRoyalFlush(isFlush, isStraight, values))
                return new HandScore(HandRank.RoyalFlush, SuitValue.Ace);

            if (isFlush && isStraight)
                return new HandScore(HandRank.StraightFlush, straightHighCard);

            if (counts[0] == 4)
                return new HandScore(HandRank.FourOfAKind, groupedValues[0]);

            if (counts[0] == 3 && counts[1] == 2)
                return new HandScore(HandRank.FullHouse, groupedValues[0]);

            if (isFlush)
                return new HandScore(HandRank.Flush, sortedCards.First().Value);

            if (isStraight)
                return new HandScore(HandRank.Straight, straightHighCard);

            if (counts[0] == 3)
                return new HandScore(HandRank.ThreeOfAKind, groupedValues[0]);

            if (counts.Count(c => c == 2) == 2)
            {
                var highestPair = groupedValues
                    .Where((_, i) => counts[i] == 2)
                    .Max();
                return new HandScore(HandRank.TwoPairs, highestPair);
            }

            if (counts[0] == 2)
                return new HandScore(HandRank.OnePair, groupedValues[0]);

            return new HandScore(HandRank.HighCard, sortedCards.First().Value);
        }

        private bool IsFlush(List<Suit> suits) =>
            suits.Distinct().Count() == 1;

        private bool IsStraight(List<SuitValue> values, out SuitValue highCard)
        {
            var sorted = values.OrderBy(v => (int)v).ToList();

            bool isNormalStraight = sorted.Zip(sorted.Skip(1), (a, b) => (int)b - (int)a).All(diff => diff == 1);
            bool isAceLowStraight = sorted.SequenceEqual(new List<SuitValue> {
                SuitValue.Two, SuitValue.Three, SuitValue.Four, SuitValue.Five, SuitValue.Ace
            });

            highCard = isAceLowStraight ? SuitValue.Five : sorted.Last();
            return isNormalStraight || isAceLowStraight;
        }

        private List<IGrouping<SuitValue, Card>> GroupCardsByValue(List<Card> cards) =>
            cards.GroupBy(c => c.Value)
                 .OrderByDescending(g => g.Count())
                 .ThenByDescending(g => g.Key)
                 .ToList();

        private bool IsRoyalFlush(bool isFlush, bool isStraight, List<SuitValue> values)
        {
            var royal = new List<SuitValue> {
                SuitValue.Ten, SuitValue.Jack, SuitValue.Queen, SuitValue.King, SuitValue.Ace
            };
            return isFlush && isStraight && royal.All(values.Contains);
        }
    }
}