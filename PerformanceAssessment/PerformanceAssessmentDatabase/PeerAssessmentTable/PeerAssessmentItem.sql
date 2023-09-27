CREATE TABLE [dbo].[PeerAssessmentItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [QuestionType] NVARCHAR(MAX) NOT NULL,
    [Weight] FLOAT NOT NULL,
    [Target] FLOAT NOT NULL, 
    [Required] BIT NOT NULL,
    [PeerAssessmentId] INT NOT NULL,
    CONSTRAINT [FK_PeerAssessmentItemPeerAssessment] FOREIGN KEY ([PeerAssessmentId]) REFERENCES [PeerAssessment]([Id]) ON DELETE CASCADE
)
