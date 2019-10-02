using System.Collections.Generic;
using System.Linq;
using CardDecks.Models.Entities.Cards;

namespace CardDecks.Models.Repositories
{
	public class CardDecksRepository
	{
		public void Create(CardDeck deck) => DataBase.CardDecks.Add(deck);

		public IEnumerable<string> GetDeckNames() => DataBase.CardDecks.Select(x => x.Name);

		public CardDeck GetByName(string name) => DataBase.CardDecks.SingleOrDefault(x => x.Name == name);

		public void Delete(string name) => DataBase.CardDecks.RemoveAll(x => x.Name == name);

		public void Update(CardDeck deck)
		{
			var index = DataBase.CardDecks.FindIndex(x => x.Name == deck.Name);

			DataBase.CardDecks[index] = deck;
		}
	}
}