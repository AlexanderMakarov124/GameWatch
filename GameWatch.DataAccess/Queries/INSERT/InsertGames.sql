CREATE PROCEDURE InsertGames
AS
	INSERT INTO Games (Name, Genre, Description, CreatedAt, GameListId, StoreLink, DownloadLink)
	VALUES 
	('Starcraft 2', 'RTS', 'Best', '01/18/2023', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'), NULL, NULL),
	('Warcraft 3', 'RTS', 'Best', '12/16/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'), NULL, NULL),
	('League of Legends', 'MOBA',  'Best MOBA', '10/10/2015', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite'), 'https://www.leagueoflegends.com/', NULL),
	('Dota 2', 'MOBA', '....', '05/18/2012', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Played'), NULL, NULL),
	('Katana ZERO', 'Action', 'Planned a long ago.', '04/18/2022', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Planned'),'https://www.google.com/url?sa=t&rct=j&q=&esrc=s&source=web&cd=&ved=2ahUKEwjPwO_7z4f9AhXXRPEDHcl0CCAQFnoECBAQAQ&url=https%3A%2F%2Fstore.steampowered.com%2Fapp%2F460950%2FKatana_ZERO%2F&usg=AOvVaw32N_P6qWEE79lNpm7JBOcC', 'https://rutracker.org/forum/tracker.php?nm=Katana%20zero')
	;
