using System.Collections.Generic;
using System.Threading.Tasks;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Repositories
{
	public interface ICardDecksRepository
	{
		Task CreateAsync(CardDeck deck);
		Task<IEnumerable<string>> GetDeckNamesAsync();
		Task<CardDeck> GetByNameAsync(string name);
		Task DeleteAsync(string name);
		Task UpdateAsync(CardDeck deck);
	}
}