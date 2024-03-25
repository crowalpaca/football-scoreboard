namespace FootballScoreboard
{
	public class Database : IDatabase
	{
		public IEnumerable<Game> Games => _games.Values;
		private Dictionary<string, Game> _games = new Dictionary<string, Game>();

		/// <inheritdoc />
		public void SetGame(Game game)
		{
			var gameId = GetGameId(game.HomeTeam, game.AwayTeam);

			if (_games.ContainsKey(gameId))
			{
				_games[gameId] = game;
			}
			else
			{ 
				_games.Add(gameId, game);
			}
		}

		/// <inheritdoc />
		public Game GetGame(string homeTeam, string awayTeam)
		{
			var gameId = GetGameId(homeTeam, awayTeam);

			return _games[gameId];
		}

		/// <inheritdoc />
		public void RemoveGame(string homeTeam, string awayTeam)
		{
			_games.Remove(GetGameId(homeTeam, awayTeam));
		}

		private string GetGameId(string homeTeam, string awayTeam)
		{
			if (homeTeam == null)
			{
				throw new NullReferenceException(nameof(homeTeam));
			}
			if (awayTeam == null)
			{
				throw new NullReferenceException(nameof(awayTeam));
			}

			return homeTeam + awayTeam;
		}
	}
}
