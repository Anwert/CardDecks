namespace CardDecks.Models.DataModels
{
	public struct Card
	{
		public CardValue Value { get; set; }

		public CardSuit Suit { get; set; }
	}
}