namespace FootballScoreboard
{
	public interface IDatabase
	{
		IEnumerable<Game> Games { get; }

		/// <summary>
		/// Adds a game to the database of overrides it if such a game already exists.
		/// </summary>
		void SetGame(Game game);

		/// <summary>
		/// Returns the game between the teams provided if it exists.
		/// </summary>
		Game GetGame(string homeTeam, string awayTeam);

		/// <summary>
		/// Removes the game between the teams provided.
		/// </summary>
		void RemoveGame(string homeTeam, string awayTeam);
	}
}
