CREATE TABLE [dbo].[Answer]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [AssessmentId] INT NOT NULL,
    [ItemId] INT NOT NULL,
    [AnswerText] NVARCHAR(MAX) NULL,
    [SelectedChoices] NVARCHAR(MAX) NULL,
    [CounterValue] FLOAT NULL,
    CONSTRAINT [FK_AnswerAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AnswerItem] FOREIGN KEY ([ItemId]) REFERENCES [Item]([Id]) ON DELETE CASCADE
)