using PokerGame.Domain;

namespace PokerGame.Application.Interfaces
{
    public interface IHandValidator
    {
        public void Validate(Hand hand);
    }
}
