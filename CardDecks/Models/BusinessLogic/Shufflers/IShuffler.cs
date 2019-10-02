using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Shufflers
{
	public interface IShuffler
	{
		CardDeck Shuffle(CardDeck deck);
	}
}