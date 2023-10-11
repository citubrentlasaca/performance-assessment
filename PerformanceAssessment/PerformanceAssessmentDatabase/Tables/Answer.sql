CREATE TABLE [dbo].[Answer]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [ItemId] INT NOT NULL,
    [AnswerText] NVARCHAR(MAX) NULL,
    [SelectedChoices] NVARCHAR(MAX) NULL,
    [CounterValue] FLOAT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DateTimeAnswered] DATETIME NOT NULL,
    CONSTRAINT [FK_AnswerEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnswerItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([Id]) ON DELETE CASCADE
)