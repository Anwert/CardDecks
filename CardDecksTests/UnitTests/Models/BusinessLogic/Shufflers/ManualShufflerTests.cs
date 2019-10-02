using CardDecks.Models.BusinessLogic.Shufflers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CardDecksTests.UnitTests.Models.BusinessLogic.Shufflers
{
	[TestFixture]
	[Parallelizable(ParallelScope.Self)]
	public class ManualShufflerTests : ShufflerTests
	{
		protected override void RegisterServicesForTests(IServiceCollection services)
		{
			services.AddSingleton<IShuffler, ManualShuffler>();
		}
	}
}