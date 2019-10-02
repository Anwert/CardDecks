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

			switch (config.GetValue<string>(Consts.SHUFFLING_ALGORITHM_SETTING_NAME))
			{
				case Consts.SHUFFLING_ALGORITHM_SIMPLE:
					services.AddSingleton<IShuffler, SimpleShuffler>();
					break;

				case Consts.SHUFFLING_ALGORITHM_MANUAL:
					services.AddSingleton<IShuffler, ManualShuffler>();
					break;

				default:
					throw new Exception($@"В настройках приложения должна быть указана переменная ""{Consts.SHUFFLING_ALGORITHM_SETTING_NAME}"" со значением ""{Consts.SHUFFLING_ALGORITHM_SIMPLE}"" или ""{Consts.SHUFFLING_ALGORITHM_MANUAL}""");
			}
		}
	}
}