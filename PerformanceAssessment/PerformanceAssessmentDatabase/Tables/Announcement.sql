CREATE TABLE [dbo].[Announcement]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Content] NVARCHAR(MAX) NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL
)
