using System;
using CardDecks.Models.BusinessLogic.Repositories;
using CardDecks.Models.BusinessLogic.Services;
using CardDecks.Models.BusinessLogic.Shufflers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CardDecks.Models.Extensions
{
	public static class ServiceCollectionExtension
	{
		public static void AddCustomServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddSingleton<ICardDecksService, CardDecksService>();
			services.AddSingleton<ICardDecksRepository, CardDecksRepository>();

			switch (config.GetValue<string>("ShufflingAlgorithm"))
			{
				case "Simple":
					services.AddSingleton<IShuffler, SimpleShuffler>();
					break;

				case "Manual":
					services.AddSingleton<IShuffler, ManualShuffler>();
					break;

				default:
					throw new Exception(@"В настройках приложения должна быть указана переменная ""ShufflingAlgorithm"" со значением ""Manual"" или ""Simple""");
			}
		}
	}
}