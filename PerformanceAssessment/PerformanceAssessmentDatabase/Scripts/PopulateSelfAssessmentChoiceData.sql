USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[SelfAssessmentChoice] ON

INSERT INTO [dbo].[SelfAssessmentChoice] ([Id], [ChoiceValue], [SelfAssessmentItemId])
VALUES
    (1, 'Exceeded expectations', 2),
    (2, 'Met expectations', 2),
    (3, 'Partially met expectations', 2),
    (4, 'Did not meet expectations', 2),

    (5, 'Very effective', 4),
    (6, 'Effective', 4),
    (7, 'Somewhat effective', 4),
    (8, 'Not effective', 4);

SET IDENTITY_INSERT [dbo].[SelfAssessmentChoice] OFF
