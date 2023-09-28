USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[SelfAssessmentItem] ON

INSERT INTO [dbo].[SelfAssessmentItem] ([Id], [Question], [QuestionType], [Weight], [Target], [Required], [SelfAssessmentId])
VALUES

    (1, 'Please provide a detailed paragraph describing your assessment of your sales performance.', 'Paragraph', 0, 0, 1, 1),
    (2, 'How well did you meet your sales targets this quarter?', 'Multiple choice', 50, 50, 1, 1),
    (3, 'Provide examples of successful sales strategies you used.', 'Short answer', 0, 0, 1, 1),
    (4, 'How effective were your recent marketing campaigns?', 'Checkboxes', 50, 50, 1, 1);

SET IDENTITY_INSERT [dbo].[SelfAssessmentItem] OFF
