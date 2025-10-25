using PokerGame.Api.Models.Dto;
using PokerGame.Domain;

namespace PokerGame.Api.Mappers
{
    public static class HandMapper
    {
        public static Hand ToDomain(HandDto dto)
        {
            var cards = dto.Cards.Select(c =>
                new Card(
                    Enum.Parse<SuitValue>(c.Value, ignoreCase: true),
                    Enum.Parse<Suit>(c.Suit, ignoreCase: true)
                )
            );

            return new Hand(dto.HandNo, cards);
        }
    }
}
