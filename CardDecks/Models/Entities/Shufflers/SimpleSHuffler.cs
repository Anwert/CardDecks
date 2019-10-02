using System;
using System.Linq;
using CardDecks.Models.Entities.Cards;

namespace CardDecks.Models.Entities.Shufflers
{
	public class SimpleShuffler : IShuffler
	{
		private readonly Random _rnd = new Random();

		public CardDeck Shuffle(CardDeck deck)
			=> new CardDeck
			{
				Name = new string(deck.Name),
				Cards = deck.Cards.OrderBy(_ => _rnd.Next()).ToList()
			};
	}
}