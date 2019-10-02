using System.Linq;
using CardDecks.Models.BusinessLogic.Shufflers;
using CardDecks.Models.DataModel;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CardDecksTests.Models.BusinessLogic.Shufflers
{
	public class ManualShufflerTests : BaseTest
	{
		private const string NAME = "name";

		private IShuffler _shuffler;

		protected override void RegisterServicesForTests(IServiceCollection services)
		{
			services.AddSingleton<IShuffler, ManualShuffler>();
		}

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_shuffler = Resolve<IShuffler>();
		}

		[Test]
		public void Shuffle_shuffles_cards()
		{
			var cards = Builder<Card>.CreateListOfSize(100)
				.All()
				.With(x => x.Suit = RandomGenerator.Enumeration<CardSuit>())
				.With(x => x.Value = RandomGenerator.Enumeration<CardValue>())
				.Build()
				.ToList();

			var deck = new CardDeck { Name = NAME, Cards = cards };

			var shuffledDeck = _shuffler.Shuffle(deck);

			shuffledDeck.Name.Should().Be(NAME);
			shuffledDeck.Cards.Should().BeEquivalentTo(cards);

			Assert.False(shuffledDeck.Cards.SequenceEqual(cards));
		}
	}
}