CREATE PROCEDURE CreateGenres
AS
	CREATE TABLE Genres
	(
		Id INT NOT NULL IDENTITY(1,1),
		Name NVARCHAR(50) NOT NULL,

		CONSTRAINT PK_Genres PRIMARY KEY CLUSTERED (Id ASC)
	)