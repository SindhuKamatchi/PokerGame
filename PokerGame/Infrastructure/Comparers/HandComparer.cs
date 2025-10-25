using PokerGame.Domain;

namespace PokerGame.Infrastructure.Comparers
{
    public class HandComparer : IComparer<int>
    {
        private readonly Dictionary<int, HandScore> _scores;
        private readonly Dictionary<int, Hand> _handMap;

        public HandComparer(Dictionary<int, HandScore> scores, Dictionary<int, Hand> handMap)
        {
            _scores = scores;
            _handMap = handMap;
        }

        public int Compare(int x, int y)
        {
            var scoreX = _scores[x];
            var scoreY = _scores[y];

            int rankCompare = scoreY.Rank.CompareTo(scoreX.Rank);
            if (rankCompare != 0) return rankCompare;

            int highCardCompare = scoreY.HighCard.CompareTo(scoreX.HighCard);
            if (highCardCompare != 0) return highCardCompare;

            var kickersX = _handMap[x].Cards.Select(c => c.Value).OrderByDescending(v => v).ToList();
            var kickersY = _handMap[y].Cards.Select(c => c.Value).OrderByDescending(v => v).ToList();

            for (int i = 0; i < kickersX.Count; i++)
            {
                int cmp = kickersY[i].CompareTo(kickersX[i]);
                if (cmp != 0) return cmp;
            }

            return 0;
        }
    }
}