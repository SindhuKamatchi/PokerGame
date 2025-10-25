using PokerGame.Domain;

namespace PokerGame.Application.Interfaces
{
    public interface IPokerGameEngine
    {
        Dictionary<int, HandScore> ScoreHands(List<Hand> hands);
        Hand DetermineWinner(List<Hand> hands);
    }
}