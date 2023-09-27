CREATE TABLE [dbo].[SelfAssessmentItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [QuestionType] NVARCHAR(MAX) NOT NULL,
    [Weight] FLOAT NOT NULL,
    [Target] FLOAT NOT NULL, 
    [Required] BIT NOT NULL,
    [SelfAssessmentId] INT NOT NULL,
    CONSTRAINT [FK_SelfAssessmentItemSelfAssessment] FOREIGN KEY ([SelfAssessmentId]) REFERENCES [SelfAssessment]([Id]) ON DELETE CASCADE
)