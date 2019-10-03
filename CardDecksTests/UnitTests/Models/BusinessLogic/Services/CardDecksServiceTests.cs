using System.Linq;
using System.Threading.Tasks;
using CardDecks.Models.BusinessLogic.Repositories;
using CardDecks.Models.BusinessLogic.Services;
using CardDecks.Models.BusinessLogic.Shufflers;
using CardDecks.Models.DataModel;
using CardDecks.Models.Exceptions;
using CardDecksTests.Extensions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;

namespace CardDecksTests.UnitTests.Models.BusinessLogic.Services
{
	[TestFixture]
	[Parallelizable(ParallelScope.Self)]
	public class CardDecksServiceTests : BaseTest
	{
		private ICardDecksService _service;
		private ICardDecksRepository _cardDecksRepository;
		private IShuffler _shuffler;

		protected override void RegisterServicesForTests(IServiceCollection services)
		{
			services.AddMock<ICardDecksRepository>();
			services.AddMock<IShuffler>();
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_service = Resolve<ICardDecksService>();
			_cardDecksRepository = Resolve<ICardDecksRepository>();
			_shuffler = Resolve<IShuffler>();
		}

		[OneTimeTearDown]
		public void OneTimeTearDown()
		{
			_cardDecksRepository.ClearReceivedCalls();
			_shuffler.ClearReceivedCalls();
		}

		[Test]
		public void CreateOrderedDeckAsync_throws_if_name_is_empty()
		{
			Assert.ThrowsAsync<InvalidDeckNameException>(async () => await _service.CreateOrderedDeckAsync(string.Empty));
		}

		[Test]
		public void CreateOrderedDeckAsync_throws_if_deck_with_passed_name_exists()
		{
			var name = RandomGenerator.Phrase(10);
			var deck = new CardDeck();

			_cardDecksRepository.GetByNameAsync(name).Returns(deck);

			Assert.ThrowsAsync<DeckAlreadyExistsException>(async () => await _service.CreateOrderedDeckAsync(name));
		}
		
		[Test]
		public async Task CreateOrderedDeckAsync_creates_new_deck()
		{
			var name = RandomGenerator.Phrase(10);
			var deck = await _service.CreateOrderedDeckAsync(name);

			_cardDecksRepository.Received().CreateAsync(Arg.Is<CardDeck>(x => x.Cards.Count > 0));

			deck.Name.Should().Be(name);
			deck.Cards.Should().HaveCountGreaterThan(0);

			Assert.That(deck.Cards, Is.Ordered.By("Suit").Then.By("Value"));
		}

		[Test]
		public async Task GetDeckNamesAsync_returns_result_from_repository()
		{
			var deckNames = Enumerable.Range(0, 100).Select(x => RandomGenerator.Phrase(10)).ToList();

			_cardDecksRepository.GetDeckNamesAsync().Returns(deckNames);

			var result = await _service.GetDeckNamesAsync();

			result.Should().BeEquivalentTo(deckNames);
		}

		[Test]
		public async Task GetByNameAsync_returns_result_from_repository()
		{
			var name = RandomGenerator.Phrase(10);
			var deck = new CardDeck();

			_cardDecksRepository.GetByNameAsync(name).Returns(deck);

			var result = await _service.GetByNameAsync(name);

			result.Should().BeSameAs(deck);
		}

		[Test]
		public void GetByNameAsync_throws_if_repository_returns_null()
		{
			var name = RandomGenerator.Phrase(10);

			_cardDecksRepository.GetByNameAsync(name).Returns((CardDeck)null);

			Assert.ThrowsAsync<DeckNotFoundException>(async () => await _service.GetByNameAsync(name));
		}

		[Test]
		public async Task DeleteAsync_calls_repository_method()
		{
			await _service.DeleteAsync("qwe");

			_cardDecksRepository.Received().DeleteAsync("qwe");
		}

		[Test]
		public async Task ShuffleAsync_shuffles_and_updates_deck()
		{
			var deck = new CardDeck();
			var shuffledDeck = new CardDeck();

			_shuffler.Shuffle(deck).Returns(shuffledDeck);

			var result = await _service.ShuffleAsync(deck);

			_cardDecksRepository.Received().UpdateAsync(shuffledDeck);

			result.Should().BeSameAs(shuffledDeck);
		}
	}
}