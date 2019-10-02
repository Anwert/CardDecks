using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardDecks.Models.BusinessLogic.Repositories;
using CardDecks.Models.BusinessLogic.Shufflers;
using CardDecks.Models.DataModel;
using CardDecks.Models.Exceptions;

namespace CardDecks.Models.BusinessLogic.Services
{
	public class CardDecksService : ICardDecksService
	{
		private readonly ICardDecksRepository _cardDecksRepository;
		private readonly IShuffler _shuffler;

		public CardDecksService(ICardDecksRepository cardDecksRepository, IShuffler shuffler)
		{
			_cardDecksRepository = cardDecksRepository;
			_shuffler = shuffler;
		}

		public async Task<CardDeck> CreateOrderedDeckAsync(string name)
		{
			var existingDeck = await _cardDecksRepository.GetByNameAsync(name);
			if (!(existingDeck is null))
				throw new DeckAlreadyExistsException();

			var cards = (
				from suit in Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>()
				from value in Enum.GetValues(typeof(CardValue)).Cast<CardValue>()
				select new Card
				{
					Suit = suit,
					Value = value
				}).ToList();

			var deck = new CardDeck
			{
				Name = name,
				Cards = cards
			};

			await _cardDecksRepository.CreateAsync(deck);

			return deck;
		}

		public async Task<IEnumerable<string>> GetDeckNamesAsync() => await _cardDecksRepository.GetDeckNamesAsync();

		public async Task<CardDeck> GetByNameAsync(string name)
		{
			var deck = await _cardDecksRepository.GetByNameAsync(name);
			if (deck is null)
				throw new DeckNotFoundException();

			return deck;
		}

		public async Task DeleteAsync(string name) => await _cardDecksRepository.DeleteAsync(name);

		public async Task<CardDeck> ShuffleAsync(CardDeck deck)
		{
			var shuffledDeck = _shuffler.Shuffle(deck);

			await _cardDecksRepository.UpdateAsync(shuffledDeck);

			return shuffledDeck;
		}
	}
}