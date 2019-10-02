using System.Collections.Generic;
using CardDecks.Models.DataModels;
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
		// todo в гите сделать README
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
		public IActionResult CreateOrderedCardDeck(string name)
		{
			// todo асинками все сделать
			var cardDeck = _cardDecksService.CreateOrderedDeck(name);

			return Ok(cardDeck.AsViewModel());
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
			var deck = _cardDecksService.GetByName(name);

			if (deck is null)
			{
				return BadRequest(new
				{
					error = "Не удалось найти колоду с переданным названием"
				});
			}

			return Ok(deck);
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult DeleteCardDeck(string name)
		{
			_cardDecksService.Delete(name);

			return Ok();
		}
	}
}