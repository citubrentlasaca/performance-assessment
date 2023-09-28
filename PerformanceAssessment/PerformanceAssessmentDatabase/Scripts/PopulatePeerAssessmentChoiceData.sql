USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[PeerAssessmentChoice] ON

INSERT INTO [dbo].[PeerAssessmentChoice] ([Id], [ChoiceValue], [PeerAssessmentItemId])
VALUES
    (1, 'Yes', 3),
    (2, 'No', 3),
    (5, '1', 4),
    (6, '2', 4),
    (7, '3', 4),
    (8, '4', 4),
    (9, '5', 4);

SET IDENTITY_INSERT [dbo].[PeerAssessmentChoice] OFF
