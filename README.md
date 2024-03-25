# Football Scoreboard

An implementation of the Live Football World Cup Score Board as a C# library. It has no external dependencies.

## Requirements

Visual Studio 2022 or any alternative code editor with .NET 7 intsalled.


## API

The API consists of two interfaces:

`IScoreboard` is the main interface between the library and the user. It provides necessary functionality to track, sort and update the game scores.
The interface is completely modular and can be easily integrated into any system.

```cs
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
```

`IDatabase` is a storage interface for more convenient storage and representation of data. The default implementation is an in-memory `Dictionary` collection.

```cs
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
```

## Data Storage

Internally, game data is stored within `Game` struct. It has all the necessary data about the game, plus the time it was added to the scoreboard, which is necessary for ordering.

```cs
public struct Game
{
	public string HomeTeam;
	public string AwayTeam;
	public int HomeTeamScore;
	public int AwayTeamScore;
	public DateTime StartTime;
}
```

The games are stored as key-value pairs where the key is `HomeTeam + AwayTeam`. This approach was chosen against a unique numeric id because it greatly simplifies the implementation and adds an in-built protection from duplicates. Potential later features that would require an id, such as game history, can be implemented in a way that consumes data from the scoreboard and generates an id for long-term storage.
