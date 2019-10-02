using CardDecks.Models.Entities.Shufflers;
using CardDecks.Models.Repositories;
using CardDecks.Models.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CardDecks.Models.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddCustomServices(this IServiceCollection services)
		{
			// todo интерфейсы
			services.AddSingleton<CardDecksService>();
			services.AddSingleton<CardDecksRepository>();
			services.AddSingleton<IShuffler, SimpleShuffler>();
		}
	}
}