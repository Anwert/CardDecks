namespace CardDecks.Models.Exceptions
{
	public class InvalidDeckNameException : DeckException
	{
		public override string Message => "Название колоды должно содержать хотя бы один символ";
	}
}