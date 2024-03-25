namespace FootballScoreboard
{
	public interface IScoreboard
	{
		/// <summary>
		/// Adds a new game between home team and away team with default scores of 0 - 0 to the scoreboard.
		/// </summary>
		void StartGame(string homeTeam, string awayTeam);

		/// <summary>
		/// Updates an existing game with a new pair of absolute scores.
		/// </summary>
		void UpdateGame(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore);

		/// <summary>
		/// Removes the game from the scoreboard.
		/// </summary>
		void FinishGame(string homeTeam, string awayTeam);

		/// <summary>
		/// Returns a summary of games in progress ordered by their total score. 
		/// The games with the same total score will be returned ordered by the most recently started match in the scoreboard.
		/// </summary>
		Game[] GetOrderedGames();
	}
}
