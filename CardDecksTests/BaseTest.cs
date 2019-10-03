using System;
using CardDecks;
using FizzWare.NBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CardDecksTests
{
	public abstract class BaseTest
	{
		protected readonly RandomGenerator RandomGenerator = new RandomGenerator();
		private IServiceProvider _serviceProvider;

		[OneTimeSetUp]
		public void BaseSetUp()
		{
			var services = new ServiceCollection();

			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.test.json")
				.Build();

			var startup = new Startup(config);
			startup.ConfigureServices(services);

			RegisterServicesForTests(services);

			_serviceProvider = services.BuildServiceProvider();
		}

		protected T Resolve<T>() => (T)_serviceProvider.GetService(typeof(T));

		protected virtual void RegisterServicesForTests(IServiceCollection services)
		{
		}
	}
}