CREATE TABLE [dbo].[EmployeeAssignSchedulerNotification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [AssessmentId] INT NULL,
    [IsRead] BIT NOT NULL DEFAULT 0,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [FK_EmployeeAssignSchedulerNotificationEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeAssignSchedulerNotificationAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE
)
