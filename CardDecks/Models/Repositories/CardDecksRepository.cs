using System.Collections.Generic;
using System.Linq;
using CardDecks.Models.DataModels;

namespace CardDecks.Models.Repositories
{
	public class CardDecksRepository
	{
		public void Create(CardDeck deck) => DataBase.CardDecks.Add(deck);

		public IEnumerable<string> GetDeckNames() => DataBase.CardDecks.Select(x => x.Name);

		public CardDeck GetByName(string name) => DataBase.CardDecks.SingleOrDefault(x => x.Name == name);

		public void Delete(string name) => DataBase.CardDecks.RemoveAll(x => x.Name == name);
	}
}