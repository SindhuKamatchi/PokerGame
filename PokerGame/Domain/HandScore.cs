namespace PokerGame.Domain
{
    public class HandScore
    {
        public HandRank Rank { get; set; }
        public SuitValue HighCard { get; set; }

        public HandScore(HandRank rank, SuitValue highCard)
        {
            Rank = rank;
            HighCard = highCard;
        }

        public string Description => $"{FormatRank(Rank)} with High Card {HighCard}";

        private string FormatRank(HandRank rank)
        {
            return rank switch
            {
                HandRank.FullHouse => "Full House",
                HandRank.StraightFlush => "Straight Flush",
                HandRank.FourOfAKind => "Four of a Kind",
                HandRank.ThreeOfAKind => "Three of a Kind",
                HandRank.TwoPairs => "Two Pairs",
                HandRank.OnePair => "One Pair",
                HandRank.HighCard => "High Card",
                HandRank.RoyalFlush => "Royal Flush",
                HandRank.Flush => "Flush",
                HandRank.Straight => "Straight",
                _ => rank.ToString()
            };
        }
    }
}