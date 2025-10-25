using PokerGame.Domain;

namespace PokerGame.Infrastructure.Comparers
{
    public class HandComparer : IComparer<Hand>
    {
        private readonly Dictionary<Hand, HandScore> _scores;

        public HandComparer(Dictionary<Hand, HandScore> scores) => _scores = scores;

        public int Compare(Hand? x, Hand? y)
        {
            if (x == null || y == null) return 0;

            var scoreX = _scores[x];
            var scoreY = _scores[y];

            int rankCompare = scoreY.Rank.CompareTo(scoreX.Rank);
            if (rankCompare != 0) return rankCompare;

            int highCardCompare = scoreY.HighCard.CompareTo(scoreX.HighCard);
            if (highCardCompare != 0) return highCardCompare;

            var kickersX = x.Cards.Select(c => c.Value).OrderByDescending(v => v).ToList();
            var kickersY = y.Cards.Select(c => c.Value).OrderByDescending(v => v).ToList();

            for (int i = 0; i < kickersX.Count; i++)
            {
                int cmp = kickersY[i].CompareTo(kickersX[i]);
                if (cmp != 0) return cmp;
            }

            return 0;
        }
    }
}
