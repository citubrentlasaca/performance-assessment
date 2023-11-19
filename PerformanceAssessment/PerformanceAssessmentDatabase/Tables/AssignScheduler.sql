CREATE TABLE [dbo].[AssignScheduler]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [AssessmentId] INT NOT NULL,
    [EmployeeId] INT NOT NULL,
    [IsAnswered] BIT NOT NULL DEFAULT 0,
    [Occurrence] NVARCHAR(MAX) NULL,
    [DueDate] NVARCHAR(MAX) NOT NULL,
    [Time] NVARCHAR(MAX) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL,
    CONSTRAINT [FK_AssignSchedulerAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssignSchedulerEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE
)