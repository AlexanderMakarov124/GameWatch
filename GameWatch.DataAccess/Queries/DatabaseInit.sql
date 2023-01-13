-- Drop tables.

/****** Object:  Table [dbo].[Games]    Script Date: 1/13/2023 12:52:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Games]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[Games]
END
GO
/****** Object:  Table [dbo].[GameLists]    Script Date: 1/13/2023 12:52:32 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GameLists]') AND type in (N'U'))
BEGIN
ALTER TABLE [dbo].[Games] DROP CONSTRAINT [FK_Games_GameLists]
DROP TABLE [dbo].[GameLists]
END
GO

-- Create tables.

EXECUTE CreateGameLists

EXECUTE CreateGames

-- Insert data.

EXECUTE InsertGameLists

EXECUTE InsertGames