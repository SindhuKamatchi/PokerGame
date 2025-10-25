namespace PokerGame.Api.Models.Dto
{
    public class HandDto
    {
        public int HandNo { get; set; }
        public List<CardDto> Cards { get; set; }
    }
}
