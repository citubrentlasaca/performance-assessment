USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[EmployerAssessmentChoice] ON

INSERT INTO [dbo].[EmployerAssessmentChoice] ([Id], [ChoiceValue], [EmployerAssessmentItemId])
VALUES
    (1, 'Excellent', 1),
    (2, 'Very Good', 1),
    (3, 'Good', 1),
    (4, 'Fair', 1),
    (5, 'Poor', 1);

INSERT INTO [dbo].[EmployerAssessmentChoice] ([Id], [ChoiceValue], [EmployerAssessmentItemId])
VALUES
    (6, 'Met targets', 4),
    (7, 'Effective communication', 4),
    (8, 'Teamwork', 4),
    (9, 'Innovation', 4);

SET IDENTITY_INSERT [dbo].[EmployerAssessmentChoice] OFF
