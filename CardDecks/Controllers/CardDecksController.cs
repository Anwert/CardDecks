using System.Collections.Generic;
using System.Threading.Tasks;
using CardDecks.Models.BusinessLogic.Services;
using CardDecks.Models.Exceptions;
using CardDecks.Models.Extensions;
using CardDecks.Models.ViewModels;
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
		private readonly ICardDecksService _cardDecksService;

		public CardDecksController(ICardDecksService cardDecksService)
		{
			_cardDecksService = cardDecksService;
		}

		[HttpPost]
		[ProducesResponseType(typeof(CardDeck), StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateOrderedCardDeck([FromBody] CardName cardName)
		{
			try
			{
				var deck = await _cardDecksService.CreateOrderedDeckAsync(cardName.Name);

				return Ok(deck.AsViewModel());
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
		public async Task<IActionResult> GetCardDeckNames()
		{
			var names = await _cardDecksService.GetDeckNamesAsync();

			return Ok(names);
		}

		[HttpGet]
		[ProducesResponseType(typeof(CardDeck), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> GetCardDeck(string name)
		{
			try
			{
				var deck = await _cardDecksService.GetByNameAsync(name);

				return Ok(deck);
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> DeleteCardDeck([FromBody] CardName cardName)
		{
			await _cardDecksService.DeleteAsync(cardName.Name);

			return Ok();
		}

		[HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> ShuffleCardDeck([FromBody] CardName cardName)
		{
			try
			{
				var deck = await _cardDecksService.GetByNameAsync(cardName.Name);
				var shuffledDeck = await _cardDecksService.ShuffleAsync(deck);

				return Ok(shuffledDeck);
			}
			catch (DeckException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}
	}
}