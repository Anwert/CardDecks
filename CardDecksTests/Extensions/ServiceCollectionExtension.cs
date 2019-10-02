using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CardDecksTests.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddMock<T>(this IServiceCollection services) where T : class
		{
			services.AddSingleton(Substitute.For<T>());
		}
	}
}