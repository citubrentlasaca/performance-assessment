CREATE TABLE [dbo].[PeerAssessmentChoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ChoiceValue] NVARCHAR(MAX) NOT NULL,
	[PeerAssessmentItemId] INT NOT NULL,
    CONSTRAINT [FK_PeerAssessmentChoicePeerAssessmentItem] FOREIGN KEY ([PeerAssessmentItemId]) REFERENCES [PeerAssessmentItem]([Id]) ON DELETE CASCADE
)
