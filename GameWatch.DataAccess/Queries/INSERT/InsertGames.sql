CREATE PROCEDURE InsertGames
AS
	INSERT INTO Games (Name, Genre, Description, CreatedAt, GameListId)
	VALUES 
	('Starcraft 2', 'RTS', 'Best', '01/18/2023', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
	('Warcraft 3', 'RTS', 'Best', '12/16/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
	('League of Legends', 'MOBA',  'Best MOBA', '10/10/2015', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
	('Dota 2', 'MOBA', '....', '05/18/2012', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Played')),
	('Katana ZERO', 'Action', 'Planned a long ago.', '04/18/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Planned'))
	;
