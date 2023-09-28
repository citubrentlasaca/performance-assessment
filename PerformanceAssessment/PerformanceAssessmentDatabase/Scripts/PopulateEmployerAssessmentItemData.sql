USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[EmployerAssessmentItem] ON

INSERT INTO [dbo].[EmployerAssessmentItem] ([Id], [Question], [QuestionType], [Weight], [Target], [Required], [EmployerAssessmentId])
VALUES
    (1, 'Rate the employer''s overall performance this year.', 'Multiple choice', 50, 50, 1, 1),
    (2, 'Provide feedback on the employer''s leadership skills.', 'Paragraph', 0, 0, 1, 1),
    (3, 'Describe any significant achievements by the employer this year.', 'Short answer', 0, 0, 1, 1),
    (4, 'Rate the employer''s performance for the quarter.', 'Checkboxes', 50, 50, 1, 1);

SET IDENTITY_INSERT [dbo].[EmployerAssessmentItem] OFF
