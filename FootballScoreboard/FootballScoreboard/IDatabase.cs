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
		/// Returns a game based on 
		/// </summary>
		/// <throws></throws>
		Game GetGame(string homeTeam, string awayTeam);
	
		void RemoveGame(string homeTeam, string awayTeam);
	}
}
