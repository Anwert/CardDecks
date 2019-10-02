using System;
using System.Collections.Generic;
using System.Linq;
using CardDecks.Models.DataModels;
using CardDecks.Models.Repositories;

namespace CardDecks.Models.Services
{
	public class CardDecksService
	{
		private readonly CardDecksRepository _cardDecksRepository;

		public CardDecksService(CardDecksRepository cardDecksRepository)
		{
			_cardDecksRepository = cardDecksRepository;
		}

		public CardDeck CreateOrderedDeck(string name)
		{
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
		public CardDeck GetByName(string name) => _cardDecksRepository.GetByName(name);

		public void Delete(string name) => _cardDecksRepository.Delete(name);
	}
}