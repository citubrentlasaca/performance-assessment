CREATE TABLE [dbo].[User]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(MAX) NOT NULL, 
    [LastName] NVARCHAR(MAX) NOT NULL,
    [EmailAddress] NVARCHAR(MAX) COLLATE Latin1_General_CS_AS NOT NULL,
    [Password] NVARCHAR(MAX) COLLATE Latin1_General_CS_AS NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL
)