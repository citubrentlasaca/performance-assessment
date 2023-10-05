CREATE TABLE [dbo].[User]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(MAX) NOT NULL, 
    [LastName] NVARCHAR(MAX) NOT NULL,
    [EmailAddress] NVARCHAR(MAX) NOT NULL,
    [Password] NVARCHAR(MAX) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL
)