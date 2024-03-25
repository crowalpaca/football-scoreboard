namespace FootballScoreboard.Tests
{
	public class ScoreboardTests
	{
		[Fact]
		public void StartGame_StartNewGame_GameAddedToDatabase()
		{
			var databaseMock = new Mock<IDatabase>();
			var scoreboard = new Scoreboard(databaseMock.Object);

			scoreboard.StartGame("home", "away");

			databaseMock.Verify(m => m.SetGame(It.IsAny<Game>()), Times.Once());
		}

		[Fact]
		public void StartGame_StartTwoGames_FirstGameHasEarlierStartTime()
		{
			var startedGames = new List<Game>();
			var databaseMock = new Mock<IDatabase>();
			databaseMock.Setup(m => m.SetGame(It.IsAny<Game>()))
				.Callback<Game>(startedGames.Add);
			var scoreboard = new Scoreboard(databaseMock.Object);

			scoreboard.StartGame("team1", "team2");
			scoreboard.StartGame("team3", "team4");

			Assert.True(startedGames[0].StartTime < startedGames[1].StartTime);
		}

		[Fact]
		public void FinishGame_FinishGame_GameRemovedFromDatabase()
		{
			var databaseMock = new Mock<IDatabase>();
			var scoreboard = new Scoreboard(databaseMock.Object);

			scoreboard.FinishGame("home", "away");

			databaseMock.Verify(m => m.RemoveGame(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
		}

		[Fact]
		public void UpdateGame_UpdateExistingGame_ScoreProperlyUpdated()
		{
			var game = new Game()
			{
				HomeTeam = "home",
				AwayTeam = "away",
			};
			var databaseMock = new Mock<IDatabase>();
			databaseMock.Setup(m => m.GetGame(It.IsAny<string>(), It.IsAny<string>())).Returns(game);
			var scoreboard = new Scoreboard(databaseMock.Object);
			var homeTeamScore = 10;
			var awayTeamScore = 5;

			scoreboard.StartGame("home", "away");
			scoreboard.UpdateGame("home", "away", homeTeamScore, awayTeamScore);
			
			databaseMock.Verify(
				m => m.SetGame(
					It.Is<Game>(
						arg => arg.HomeTeamScore == homeTeamScore && arg.AwayTeamScore == awayTeamScore
					)
				)
			);
		}

		[Fact]
		public void GetOrderedGames_GetOrderedGames_OrderedCorrectly()
		{
			// These games are pre-sorted by score and date.
			var games =new []{ 
				new Game()
				{
					HomeTeam = "team1", AwayTeam = "team2",
					HomeTeamScore = 5, AwayTeamScore = 3,
					StartTime = DateTime.Now + TimeSpan.FromSeconds(1),
				},
				new Game()
				{
					HomeTeam = "team3", AwayTeam = "team4",
					HomeTeamScore = 2, AwayTeamScore = 3,
					StartTime = DateTime.Now + TimeSpan.FromSeconds(2),
				},
				new Game()
				{
					HomeTeam = "team4", AwayTeam = "team5",
					HomeTeamScore = 2, AwayTeamScore = 3,
					StartTime = DateTime.Now + TimeSpan.FromSeconds(3),
				},
				new Game()
				{
					HomeTeam = "team6", AwayTeam = "team7",
					HomeTeamScore = 0, AwayTeamScore = 1,
					StartTime = DateTime.Now + TimeSpan.FromSeconds(4),
				}
			};
			var unorderedGames = games.Reverse();

			var databaseMock = new Mock<IDatabase>();
			databaseMock.Setup(m => m.Games).Returns(unorderedGames);
			var scoreboard = new Scoreboard(databaseMock.Object);

			var orderedGames = scoreboard.GetOrderedGames();

			Assert.Equal(games, orderedGames);
		}
	}
}