CREATE TABLE [dbo].[SelfAssessmentChoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ChoiceValue] NVARCHAR(MAX) NOT NULL,
	[SelfAssessmentItemId] INT NOT NULL,
    CONSTRAINT [FK_SelfAssessmentChoiceSelfAssessmentItem] FOREIGN KEY ([SelfAssessmentItemId]) REFERENCES [SelfAssessmentItem]([Id]) ON DELETE CASCADE
)