using PokerGame.Domain;

namespace PokerGame.Application.Interfaces
{
    public interface IHandEvaluator
    {
        HandScore Evaluate(Hand hand);
    }
}
