namespace PokerGame.Domain
{
    public enum Suit { Heart, Diamond, Club, Spade }
    public enum SuitValue
    {
        Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9,
        Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14
    }
    public class Card(SuitValue value, Suit suit)
    {
        public SuitValue Value { get; } = value;
        public Suit Suit { get; } = suit;
    }

}
