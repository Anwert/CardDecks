namespace CardDecks.Models.Entities.Cards
{
	public struct Card
	{
		public CardValue Value { get; set; }

		public CardSuit Suit { get; set; }
	}
}