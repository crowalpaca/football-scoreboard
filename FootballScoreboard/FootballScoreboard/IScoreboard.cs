namespace FootballScoreboard
{
	public class Scoreboard
	{
		private readonly IDatabase _database;

		public Scoreboard(IDatabase database)
		{
			_database = database;
		}

		public void StartGame(string homeTeam, string awayTeam)
		{
			var game = new Game()
			{
				HomeTeam = homeTeam,
				AwayTeam = awayTeam,
				StartTime = DateTime.Now,
			};

			_database.SetGame(game);
		}

		public void UpdateGame(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)
		{
			var game = _database.GetGame(homeTeam, awayTeam);
			game.HomeTeamScore = homeTeamScore;
			game.AwayTeamScore = awayTeamScore;
			
			_database.SetGame(game);
		}

		public void FinishGame(string homeTeam, string awayTeam)
		{
			_database.RemoveGame(homeTeam, awayTeam);
		}

		/// <summary>
		/// Returns a summary of games in progress ordered by their total score. 
		/// The games with the same total score will be returned ordered by the most recently started match in the scoreboard.
		/// </summary>
		public Game[] GetOrderedGames()
		{
			var games = from game in _database.Games 
				orderby (game.HomeTeamScore + game.AwayTeamScore) descending, game.StartTime ascending select game;

			return games.ToArray();
		}
	}
}
