using PokerGame.Application.Interfaces;
using PokerGame.Domain;

namespace PokerGame.Infrastructure.Validators
{
    public class HandValidator :IHandValidator
    {
        public void Validate(Hand hand)
        {
            if (hand.Cards.Count != 5)
                throw new InvalidHandException("Hand must contain exactly 5 cards.");
            if (hand.Cards.DistinctBy(c => (c.Value, c.Suit)).Count() != 5)
                throw new InvalidHandException("Hand contains duplicate cards.");
        }

        public class InvalidHandException : Exception
        {
            public InvalidHandException(string message) : base(message) { }
        }
    }
}
