using System.Collections.Generic;
using System.Threading.Tasks;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Services
{
	public interface ICardDecksService
	{
		Task<CardDeck> CreateOrderedDeckAsync(string name);

		Task<IEnumerable<string>> GetDeckNamesAsync();

		Task<CardDeck> GetByNameAsync(string name);

		Task DeleteAsync(string name);

		Task<CardDeck> ShuffleAsync(CardDeck deck);
	}
}