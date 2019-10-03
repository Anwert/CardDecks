using System.Collections.Generic;
using CardDecks.Models.DataModel;

namespace CardDecks.Models.BusinessLogic.Repositories
{
	/// <summary>
	/// Имитация базы данных
	/// </summary>
	public static class DataBase
	{
		private static readonly object LockObj = new object();

		private static List<CardDeck> _cardDecks = new List<CardDeck>();

		public static List<CardDeck> CardDecks
		{
			get
			{
				lock (LockObj)
				{
					return _cardDecks;
				}
			}
		}
	}
}