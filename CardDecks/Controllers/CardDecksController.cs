using System.Collections.Generic;
using CardDecks.Models.Entities.Cards;
using CardDecks.Models.Entities.Shufflers;
using CardDecks.Models.Exceptions;
using CardDecks.Models.Extensions;
using CardDecks.Models.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardDecks.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	[Produces("application/json")]
	public class CardDecksController : Controller
	{
		// todo в гите сделать README, написать там про рид ми и про то что шафлинк выбриается вот там вот в настройках
		// todo ошибки типа колода с таким именем уже существует и название - не пустое
		// todo возможно имеет смысл все прямо рестом сделать, то есть вынести все в отдельные модели и пометить фром бади
		// todo интерфейсы

		private readonly CardDecksService _cardDecksService;

		public CardDecksController(CardDecksService cardDecksService)
		{
			_cardDecksService = cardDecksService;
		}

		[HttpPost]
		[ProducesResponseType(typeof(CardDeck), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult CreateOrderedCardDeck(string name)
		{
			// todo асинками все сделать
			try
			{
				var deck = _cardDecksService.CreateOrderedDeck(name);

				return Ok(deck.AsViewModel());
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
		public IActionResult GetCardDeckNames()
		{
			var names = _cardDecksService.GetDeckNames();

			return Ok(names);
		}

		/// <summary>
		/// Возвращает колоду по названию. Если колоды не существует возвращает 400
		/// </summary>
		[HttpGet]
		[ProducesResponseType(typeof(CardDeck), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult GetCardDeck(string name)
		{
			try
			{
				var deck = _cardDecksService.GetByName(name);

				return Ok(deck);
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult DeleteCardDeck(string name)
		{
			_cardDecksService.Delete(name);

			return Ok();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public IActionResult ShuffleCardDeck(string name)
		{
			try
			{
				var deck = _cardDecksService.GetByName(name);
				var shuffledDeck = _cardDecksService.Shuffle(deck);

				return Ok(shuffledDeck);
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}
	}
}