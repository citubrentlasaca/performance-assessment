USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[SelfAssessment] ON

INSERT INTO [dbo].[SelfAssessment] ([Id], [Title], [Description])
VALUES

    (1, 'Performance Assessment - Sales Team', 'Assessment for the Sales Department');

SET IDENTITY_INSERT [dbo].[SelfAssessment] OFF
