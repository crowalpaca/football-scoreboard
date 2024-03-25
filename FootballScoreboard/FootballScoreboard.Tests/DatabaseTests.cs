namespace FootballScoreboard.Tests
{
	public class DatabaseTests
	{
		[Fact]
		public void SetGame_AddNewGame_GameAdded()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};

			database.SetGame(game);

			Assert.Contains(game, database.Games);
		}

		[Fact]
		public void SetGame_HomeTeamIsNull_ThrowsNullReferenceException()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = null,
				AwayTeam = "away",
			};

			var test = () => database.SetGame(game);

			Assert.Throws<NullReferenceException>(test);
		}

		[Fact]
		public void SetGame_AwayTeamIsNull_ThrowsNullReferenceException()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = null,
			};

			var test = () => database.SetGame(game);

			Assert.Throws<NullReferenceException>(test);
		}

		[Fact]
		public void SetGame_OverrideExistingGame_GameOverriden()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};
			var newGame = game; // Copying the game.
			newGame.HomeTeamScore = 1;
			newGame.AwayTeamScore = 2;

			database.SetGame(game);
			database.SetGame(newGame);

			Assert.Contains(newGame, database.Games);
		}

		[Fact]
		public void SetGame_AddMultipleGames_AllGamesAdded()
		{
			var database = new Database();
			var games = new List<Game>();


			for(var i = 0; i < 10; i += 1)
			{
				var game = new Game()
				{
					HomeTeam = Guid.NewGuid().ToString(),
					AwayTeam = Guid.NewGuid().ToString()
				};
				games.Add(game);
			}

			foreach(var game in games)
			{ 
				database.SetGame(game);
			}

			Assert.Equal(games.Count, database.Games.Count());
			foreach(var game in games)
			{ 
				Assert.Contains(game, database.Games);
			}
		}

		[Fact]
		public void GetGame_GetExistingGame_ReturnsValidGame()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};
			
			database.SetGame(game);
			var returnedGame = database.GetGame(game.HomeTeam, game.AwayTeam);

			Assert.Equal(game, returnedGame);
		}

		[Fact]
		public void GetGame_GetMissingGame_ThrowsKeyNotFoundException()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};

			Assert.Throws<KeyNotFoundException>(() => database.GetGame(game.HomeTeam, game.AwayTeam));
		}

		[Fact]
		public void RemoveGame_RemoveExistingGame_GameIsRemoved()
		{
			var database = new Database();
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};

			database.SetGame(game);
			database.RemoveGame(game.HomeTeam, game.AwayTeam);

			Assert.DoesNotContain(game, database.Games);
		}
	}
}