CREATE TABLE [dbo].[Announcement]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [Content] NVARCHAR(MAX) NULL,
    [TeamId] INT NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL
    CONSTRAINT [FK_TeamAnnouncement] FOREIGN KEY ([TeamId]) REFERENCES [Team]([Id])
)
