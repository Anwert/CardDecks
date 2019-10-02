using CardDecks.Models.Entities.Cards;

namespace CardDecks.Models.Entities.Shufflers
{
	// todo это все таки не Entity наверное
	public interface IShuffler
	{
		CardDeck Shuffle(CardDeck deck);
	}
}