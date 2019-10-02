using System.Linq;
using CardDecks.Models.Entities.Cards;

namespace CardDecks.Models.Extensions
{
	public static class CardDeckExtension
	{
		public static ViewModels.CardDeck AsViewModel(this CardDeck dataModelCardDeck)
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