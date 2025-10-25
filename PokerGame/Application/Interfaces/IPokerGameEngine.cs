using PokerGame.Domain;

namespace PokerGame.Application.Interfaces
{
    public interface IPokerGameEngine
    {
        Dictionary<Hand, HandScore> ScoreHands(List<Hand> hands);
        Hand DetermineWinner(List<Hand> hands);
    }
}
