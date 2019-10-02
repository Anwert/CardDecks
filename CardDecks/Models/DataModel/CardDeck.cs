using System.Collections.Generic;

namespace CardDecks.Models.DataModel
{
	public class CardDeck
	{
		public string Name { get; set; }

		public List<Card> Cards { get; set; }
	}
}