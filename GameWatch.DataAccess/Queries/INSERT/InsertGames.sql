INSERT INTO Games (Name, Genre, Description, GameListId)
VALUES 
('Starcraft 2', 'RTS', 'Best', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
('Warcraft 3', 'RTS', 'Best', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
('League of Legends', 'MOBA', 'Best MOBA', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Favorite')),
('Dota 2', 'MOBA', '....', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Played')),
('Katana ZERO', 'Action', 'Planned a long ago.', (SELECT Id FROM GameLists WHERE GameLists.Name = 'Planned'))
;