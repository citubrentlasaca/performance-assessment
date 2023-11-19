CREATE TABLE [dbo].[Assessment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [Title] NVARCHAR(MAX) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL,
    CONSTRAINT [FK_AssessmentEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)