using System.Collections.Generic;
using System.Linq;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Shufflers
{
	public class ManualShuffler : AbstractShuffler
	{
		public override CardDeck Shuffle(CardDeck deck)
		{
			var length = deck.Cards.Count;
			if (length < 2)
				return deck;

			var cards = deck.Cards;
			var cardsCount = cards.Count;
			var shufflesCount = Rnd.Next(10, 200);
			for (var i = 0; i < shufflesCount; i++)
			{
				var delimiter = Rnd.Next(1, cardsCount - 1);
				cards = SwapHalves(cards, delimiter);
			}

			deck.Cards = cards;
			return deck;
		}

		private List<Card> SwapHalves(IReadOnlyCollection<Card> cards, int delimiter)
		{
			var firstHalf = cards.Take(delimiter);
			var secondHalf = cards.TakeLast(cards.Count - delimiter);

			return secondHalf.Concat(firstHalf).ToList();
		}
	}
}