CREATE PROCEDURE InsertGames
AS
	INSERT INTO Games (Name, Description, CreatedAt, GameListId, DownloadLink)
	VALUES 
	('Starcraft 2', 'Best', '01/18/2023', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'),  NULL),
	('Warcraft 3', 'Best', '12/16/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'),  NULL),
	('League of Legends',  'Best MOBA', '10/10/2015', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'),  NULL),
	('Dota 2', '....', '05/18/2012', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Played'), NULL),
	('Katana ZERO', 'Planned a long ago.', '04/18/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Planned'), 'https://www.google.com/search?q=katana+zero')
	;
