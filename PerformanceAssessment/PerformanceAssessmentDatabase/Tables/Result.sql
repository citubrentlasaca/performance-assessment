CREATE TABLE [dbo].[Result]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [AssessmentId] INT NOT NULL,
    [EmployeeId] INT NOT NULL,
    [Score] FLOAT NOT NULL,
    [DateTimeDue] NVARCHAR(MAX) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    [DateTimeUpdated] DATETIME NOT NULL,
    CONSTRAINT [FK_ResultAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ResultEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE
)
