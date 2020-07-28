using System.Collections.Generic;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Repositories
{
	/// <summary>
	/// Имитация базы данных
	/// </summary>
	public static class DataBase
	{
		public static List<CardDeck> CardDecks { get; } = new List<CardDeck>();
	}
}