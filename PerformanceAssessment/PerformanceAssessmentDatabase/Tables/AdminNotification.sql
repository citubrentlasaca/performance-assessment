CREATE TABLE [dbo].[AdminNotification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [EmployeeName] NVARCHAR(MAX) NOT NULL,
    [AssessmentTitle] NVARCHAR(MAX) NOT NULL,
    [TeamName] NVARCHAR(MAX) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [FK_AdminNotificationEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE
)
