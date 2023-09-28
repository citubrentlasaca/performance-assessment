CREATE TABLE [dbo].[EmployerAssessmentItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [QuestionType] NVARCHAR(MAX) NOT NULL,
    [Weight] FLOAT NOT NULL,
    [Target] FLOAT NOT NULL, 
    [Required] BIT NOT NULL,
    [EmployerAssessmentId] INT NOT NULL,
    CONSTRAINT [FK_EmployerAssessmentItemEmployerAssessment] FOREIGN KEY ([EmployerAssessmentId]) REFERENCES [EmployerAssessment]([Id]) ON DELETE CASCADE
)
