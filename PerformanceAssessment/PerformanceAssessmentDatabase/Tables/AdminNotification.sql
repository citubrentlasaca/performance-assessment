CREATE TABLE [dbo].[AdminNotification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [AssessmentId] INT NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [FK_AdminNotificationEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdminNotificationAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE
)
