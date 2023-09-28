USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[PeerAssessmentItem] ON

INSERT INTO [dbo].[PeerAssessmentItem] ([Id], [Question], [QuestionType], [Weight], [Target], [Required], [PeerAssessmentId])
VALUES

    (1, 'How effectively did your peer contribute to the project?', 'Short answer', 0, 0, 1, 1),
    (2, 'Provide a detailed assessment of your peer''s teamwork skills.', 'Paragraph', 0, 0, 1, 1),
    (3, 'Did your peer meet deadlines consistently?', 'Checkboxes', 50, 50, 1, 1),
    (4, 'Rate your peer''s communication skills on a scale of 1 to 5.', 'Multiple choice', 50, 50, 1, 1);

SET IDENTITY_INSERT [dbo].[PeerAssessmentItem] OFF
