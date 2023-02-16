CREATE PROCEDURE InsertGamesGenres
AS
	INSERT INTO GamesGenres (GameId, GenreId)
	VALUES
	((SELECT Id FROM Games WHERE Name = 'Starcraft 2'), (SELECT Id FROM Genres WHERE Name = 'Strategy')),
	((SELECT Id FROM Games WHERE Name = 'Warcraft 3'), (SELECT Id FROM Genres WHERE Name = 'Strategy'))
	;