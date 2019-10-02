namespace CardDecks.Models.Exceptions
{
	public class DeckAlreadyExistsException : DeckException
	{
		public override string Message => "Колода с переданным названием уже существует";
	}
}