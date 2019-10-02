namespace CardDecks.Models.Exceptions
{
	public class DeckNotFoundException : DeckException
	{
		public override string Message => "Не удалось найти колоду по переданному названию";
	}
}