using System.Collections.Generic;
using CardDecks.Models.DataModels;

namespace CardDecks.Models.Repositories
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

			set
			{
				lock (LockObj)
				{
					_cardDecks = value;
				}
			}
		}
	}
}