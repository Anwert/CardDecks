using System.Collections.Generic;
using System.Threading.Tasks;
using CardDecks.Controllers;
using CardDecks.Models.BusinessLogic.Services;
using CardDecks.Models.Errors;
using CardDecks.Models.Exceptions;
using CardDecks.Models.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CardDecksTests.IntegrationTests.Controllers
{
	[TestFixture]
	public class CardDecksControllerTests : BaseTest
	{
		private CardDecksController _controller;
		private ICardDecksService _cardDecksService;

		protected override void RegisterServicesForTests(IServiceCollection services)
		{
			services.AddSingleton<CardDecksController>();
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_cardDecksService = Resolve<ICardDecksService>();
			_controller = Resolve<CardDecksController>();
		}

		[Test]
		public async Task CreateOrderedCardDeck_returns_200_not_empty_deck()
		{
			var name = await GetNotExistingDeckNameAsync();

			var result = (OkObjectResult) await _controller.CreateOrderedCardDeck(new CardName { Name = name });
			var resultValue = (CardDeck)result.Value;

			resultValue.Name.Should().Be(name);
			resultValue.Cards.Should().NotBeEmpty();
		}

		[Test]
		public async Task CreateOrderedCardDeck_returns_400_if_deck_with_passed_name_already_exists()
		{
			var name = await CreateDeckAsync();

			var result = (BadRequestObjectResult)await _controller.CreateOrderedCardDeck(new CardName { Name = name });
			var resultValue = (CardDeckError)result.Value;

			resultValue.Error.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public async Task GetCardDeckNames_returns_200_with_created_deck_name()
		{
			var name = await CreateDeckAsync();

			var result = (OkObjectResult)await _controller.GetCardDeckNames();
			var resultValue = (IEnumerable<string>)result.Value;

			resultValue.Should().Contain(name);
		}

		[Test]
		public async Task GetCardDeck_returns_200_with_created_deck()
		{
			var name = await CreateDeckAsync();

			var result = (OkObjectResult)await _controller.GetCardDeck(name);
			var resultValue = (CardDeck)result.Value;

			resultValue.Name.Should().Be(name);
			resultValue.Cards.Should().NotBeEmpty();
		}

		[Test]
		public async Task GetCardDeck_returns_400_if_deck_with_passed_name_does_not_exist()
		{
			var name = await GetNotExistingDeckNameAsync();

			var result = (BadRequestObjectResult)await _controller.GetCardDeck(name);
			var resultValue = (CardDeckError)result.Value;

			resultValue.Error.Should().NotBeNullOrWhiteSpace();
		}

		[Test]
		public async Task DeleteCardDeck_returns_200_and_deletes_deck()
		{
			var name = await CreateDeckAsync();

			var result = (OkResult)await _controller.DeleteCardDeck(new CardName { Name = name });

			result.StatusCode.Should().Be(StatusCodes.Status200OK);

			Assert.ThrowsAsync<DeckNotFoundException>(async () => await _cardDecksService.GetByNameAsync(name));
		}

		[Test]
		public async Task ShuffleCardDeck_returns_200_with_shuffled_deck()
		{
			// todo дописать тесты
			var name = await CreateDeckAsync();
			var beforeShuffling = await _cardDecksService.GetByNameAsync(name);

			var result = (OkObjectResult)await _controller.ShuffleCardDeck(new CardName { Name = name });
			var resultValue = (CardDeck) result.Value;

			var expected = await _cardDecksService.GetByNameAsync(name);

			//resultValue.Should().BeEquivalentTo(expected, o => o.WithStrictOrderingFor(x => x.Cards));

			//var qwe = resultValue.Cards.SequenceEqual(beforeShuffling.Cards);

			//Assert.False(resultValue.Cards.SequenceEqual(beforeShuffling.Cards));
		}

		private async Task<string> GetNotExistingDeckNameAsync()
		{
			var name = RandomGenerator.Phrase(10);
			await _cardDecksService.DeleteAsync(name);

			return name;
		}

		private async Task<string> CreateDeckAsync()
		{
			var name = await GetNotExistingDeckNameAsync();
			var deck = await _cardDecksService.CreateOrderedDeckAsync(name);

			return deck.Name;
		}
	}
}