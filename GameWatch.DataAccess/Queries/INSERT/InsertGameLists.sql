CREATE PROCEDURE InsertGameLists
AS
	INSERT INTO GameLists (Name)
	VALUES
	('Favorite'),
	('Played'),
	('Planned')
	;