CREATE PROCEDURE CreateGames
AS
	CREATE TABLE Games
	(
		Id INT NOT NULL IDENTITY (1,1),
		Name NVARCHAR(75) NOT NULL,
		Description NVARCHAR(MAX) NULL,
		CreatedAt DATE DEFAULT GETDATE() NOT NULL,
		GameListId INT NOT NULL,
		StoreLink NVARCHAR(MAX) NULL,
		DownloadLink NVARCHAR(MAX) NULL,
		CoverUrl NVARCHAR(MAX) NULL,
		Summary NVARCHAR(MAX) NULL,
		ReleaseDate DATE NULL,

		CONSTRAINT PK_Games PRIMARY KEY CLUSTERED (Id ASC),
		CONSTRAINT FK_Games_GameLists FOREIGN KEY (GameListId) REFERENCES GameLists (Id),
		CONSTRAINT UK_Games_Name UNIQUE(Name)
	)