USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[EmployerAssessment] ON

INSERT INTO [dbo].[EmployerAssessment] ([Id], [Title], [Description])
VALUES

    (1, 'Annual Employer Performance Review', 'Conduct an annual performance review for employers.');

SET IDENTITY_INSERT [dbo].[EmployerAssessment] OFF
