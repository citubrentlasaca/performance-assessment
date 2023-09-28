CREATE TABLE [dbo].[EmployerAssessmentChoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ChoiceValue] NVARCHAR(MAX) NOT NULL,
	[EmployerAssessmentItemId] INT NOT NULL,
    CONSTRAINT [FK_EmployerAssessmentChoiceEmployerAssessmentItem] FOREIGN KEY ([EmployerAssessmentItemId]) REFERENCES [EmployerAssessmentItem]([Id]) ON DELETE CASCADE
)
