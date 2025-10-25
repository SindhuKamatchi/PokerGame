using PokerGame.Domain;
using PokerGame.Infrastructure.Comparers;

namespace PokerGame.Tests.UnitTests
{
    public class HandComparerTests
    {
        private Hand CreateHand(int handNo, string valuesCsv)
        {
            var values = valuesCsv.Split(',').Select(v => Enum.Parse<SuitValue>(v, true));
            var cards = values.Select(v => new Card(v, Suit.Spade)).ToList();
            return new Hand(handNo, cards);
        }

        [Theory]
        [InlineData("Ten,Jack,Queen,King,Ace", HandRank.Straight, SuitValue.Ace,
                    "Two,Two,Five,Seven,Nine", HandRank.OnePair, SuitValue.Two, -1)]
        [InlineData("Two,Two,Five,Seven,Nine", HandRank.OnePair, SuitValue.Two,
                    "King,King,Five,Seven,Nine", HandRank.OnePair, SuitValue.King, 1)]
        [InlineData("Nine,Nine,Five,Seven,Two", HandRank.OnePair, SuitValue.Nine,
                    "Nine,Nine,Five,Seven,Two", HandRank.OnePair, SuitValue.Nine, 0)]
        public void Compare_ShouldReturnExpectedResult(
            string hand1Values, HandRank rank1, SuitValue highCard1,
            string hand2Values, HandRank rank2, SuitValue highCard2,
            int expected)
        {
            var hand1 = CreateHand(1, hand1Values);
            var hand2 = CreateHand(2, hand2Values);

            var scores = new Dictionary<int, HandScore>
            {
                [hand1.HandNo] = new HandScore(rank1, highCard1),
                [hand2.HandNo] = new HandScore(rank2, highCard2)
            };

            var handMap = new Dictionary<int, Hand>
            {
                [hand1.HandNo] = hand1,
                [hand2.HandNo] = hand2
            };

            var comparer = new HandComparer(scores, handMap);
            var result = comparer.Compare(hand1.HandNo, hand2.HandNo);

            Assert.Equal(expected, result);
        }
    }
}