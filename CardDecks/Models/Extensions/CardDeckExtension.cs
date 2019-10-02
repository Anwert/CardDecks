using System.Linq;

namespace CardDecks.Models.Extensions
{
	public static class CardDeckExtension
	{
		public static ViewModels.CardDeck AsViewModel(this DataModels.CardDeck dataModelCardDeck)
			=> new ViewModels.CardDeck
			{
				Name = dataModelCardDeck.Name,
				Cards = dataModelCardDeck.Cards.Select(x => new ViewModels.Card
				{
					Value = x.Value.ToString(),
					Suit = x.Suit.ToString()
				}).ToList()
			};
	}
}