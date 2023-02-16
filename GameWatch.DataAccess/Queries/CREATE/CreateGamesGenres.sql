CREATE PROCEDURE CreateGamesGenres
AS
	CREATE TABLE GamesGenres
	(
		GameId INT NOT NULL,
		GenreId INT NOT NULL
	)