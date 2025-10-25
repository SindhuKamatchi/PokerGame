namespace PokerGame.Domain
{
    /// <summary>
    /// Immutable 5-card poker hand. Validates exactly 5 distinct cards at creation.
    /// </summary>
    public class Hand
    {
        public int HandNo { get; }
        public IReadOnlyList<Card> Cards { get; }

        public Hand(int handNo, IEnumerable<Card> cards)
        {
            HandNo = handNo;
            Cards = cards.ToList().AsReadOnly(); 
        }
    }
}
