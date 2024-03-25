namespace FootballScoreboard
{
	public class Scoreboard : IScoreboard
	{
		private readonly IDatabase _database;

		public Scoreboard(IDatabase database)
		{
			_database = database;
		}

		/// <inheritdoc />
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

		/// <inheritdoc />
		public void UpdateGame(string homeTeam, string awayTeam, int homeTeamScore, int awayTeamScore)
		{
			var game = _database.GetGame(homeTeam, awayTeam);
			game.HomeTeamScore = homeTeamScore;
			game.AwayTeamScore = awayTeamScore;
			
			_database.SetGame(game);
		}

		/// <inheritdoc />
		public void FinishGame(string homeTeam, string awayTeam)
		{
			_database.RemoveGame(homeTeam, awayTeam);
		}

		/// <inheritdoc />
		public Game[] GetOrderedGames()
		{
			var games = from game in _database.Games 
				orderby (game.HomeTeamScore + game.AwayTeamScore) descending, game.StartTime ascending select game;

			return games.ToArray();
		}
	}
}
