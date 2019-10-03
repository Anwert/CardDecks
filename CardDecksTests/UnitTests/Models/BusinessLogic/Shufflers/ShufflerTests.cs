using System.Linq;
using CardDecks.Models.BusinessLogic.Shufflers;
using CardDecks.Models.DataModel;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;

namespace CardDecksTests.UnitTests.Models.BusinessLogic.Shufflers
{
	public abstract class ShufflerTests : BaseTest
	{
		private IShuffler _shuffler;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_shuffler = Resolve<IShuffler>();
		}

		[Test]
		public void Shuffle_shuffles_cards()
		{
			var name = RandomGenerator.Phrase(10);

			var cards = Builder<Card>.CreateListOfSize(100)
				.All()
					.With(x => x.Suit = RandomGenerator.Enumeration<CardSuit>())
					.With(x => x.Value = RandomGenerator.Enumeration<CardValue>())
				.Build()
				.ToList();

			var deck = new CardDeck { Name = name, Cards = cards };

			var shuffledDeck = _shuffler.Shuffle(deck);

			shuffledDeck.Name.Should().Be(name);
			shuffledDeck.Cards.Should().BeEquivalentTo(cards);

			Assert.False(shuffledDeck.Cards.SequenceEqual(cards));
		}
	}
}