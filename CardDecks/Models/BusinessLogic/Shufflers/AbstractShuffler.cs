using System;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Shufflers
{
	public abstract class AbstractShuffler : IShuffler
	{
		protected readonly Random Rnd = new Random();

		public abstract CardDeck Shuffle(CardDeck deck);
	}
}