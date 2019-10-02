using System.Linq;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Shufflers
{
	public class SimpleShuffler : AbstractShuffler
	{
		public override CardDeck Shuffle(CardDeck deck)
		{
			deck.Cards = deck.Cards.OrderBy(_ => Rnd.Next()).ToList();

			return deck;
		}
	}
}