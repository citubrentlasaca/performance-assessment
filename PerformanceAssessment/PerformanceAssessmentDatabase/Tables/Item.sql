CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [QuestionType] NVARCHAR(MAX) NOT NULL,
    [Weight] FLOAT NULL, 
    [Required] BIT NOT NULL,
    [AssessmentId] INT NOT NULL,
    CONSTRAINT [FK_ItemAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE
)
