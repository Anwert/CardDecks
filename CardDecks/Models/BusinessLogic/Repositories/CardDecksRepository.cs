using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Repositories
{
	/// <summary>
	/// Не смотря на то, что в данном случае репозиторий просто имитирует запросы к БД и необходимости в асинхронности нет,
	/// методы класса реализованы через Task'и, чтобы можно было в случае чего поменять только реализацию репозитория,
	/// не переписывая все вышестоящие уровни приложения
	/// </summary>
	public class CardDecksRepository : ICardDecksRepository
	{
		public Task CreateAsync(CardDeck deck)
		{
			DataBase.CardDecks.Add(deck);

			return Task.CompletedTask;
		}

		public Task<IEnumerable<string>> GetDeckNamesAsync()
		{
			var names = DataBase.CardDecks.Select(x => x.Name);

			return Task.FromResult(names);
		}

		public Task<CardDeck> GetByNameAsync(string name)
		{
			var deck = DataBase.CardDecks.SingleOrDefault(x => x.Name == name);

			return Task.FromResult(deck);
		}

		public Task DeleteAsync(string name)
		{
			DataBase.CardDecks.RemoveAll(x => x.Name == name);

			return Task.CompletedTask;
		}

		public Task UpdateAsync(CardDeck deck)
		{
			var index = DataBase.CardDecks.FindIndex(x => x.Name == deck.Name);

			DataBase.CardDecks[index] = deck;

			return Task.CompletedTask;
		}
	}
}