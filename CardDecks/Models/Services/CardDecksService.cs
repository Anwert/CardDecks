using System;
using System.Collections.Generic;
using System.Linq;
using CardDecks.Models.Entities.Cards;
using CardDecks.Models.Entities.Shufflers;
using CardDecks.Models.Exceptions;
using CardDecks.Models.Repositories;

namespace CardDecks.Models.Services
{
	public class CardDecksService
	{
		private readonly CardDecksRepository _cardDecksRepository;

		private readonly IShuffler _shuffler;

		public CardDecksService(CardDecksRepository cardDecksRepository, IShuffler shuffler)
		{
			_cardDecksRepository = cardDecksRepository;
			_shuffler = shuffler;
		}

		/// <summary>
		/// Создает колоду с переданным названием. Если колода уже существует - возвращает null
		/// <exception cref=""></exception>
		/// </summary>
		public CardDeck CreateOrderedDeck(string name)
		{
			// todo может вынести куда нить в сервис валидации, поменять название переменной
			// todo экспешены в суммари пораскидать
			var d = _cardDecksRepository.GetByName(name);
			if (!(d is null))
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

			_cardDecksRepository.Create(deck);

			return deck;
		}

		public IEnumerable<string> GetDeckNames() => _cardDecksRepository.GetDeckNames();

		/// <summary>
		/// Возвращает колоду по названию. Если колоды не существует возвращает null
		/// </summary>
		public CardDeck GetByName(string name)
		{
			var deck = _cardDecksRepository.GetByName(name);
			if (deck is null)
				throw new DeckNotFoundException();

			return deck;
		}

		public void Delete(string name) => _cardDecksRepository.Delete(name);

		public CardDeck Shuffle(CardDeck deck)
		{
			var shuffledDeck = _shuffler.Shuffle(deck);

			_cardDecksRepository.Update(shuffledDeck);

			return shuffledDeck;
		}
	}
}