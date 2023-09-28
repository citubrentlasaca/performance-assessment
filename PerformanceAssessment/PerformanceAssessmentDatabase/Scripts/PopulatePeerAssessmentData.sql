USE [PerformanceAssessmentDatabase]
GO

SET IDENTITY_INSERT [dbo].[PeerAssessment] ON

INSERT INTO [dbo].[PeerAssessment] ([Id], [Title], [Description])
VALUES

    (1, 'Quarterly Peer Review', 'Review and assess your peers'' performance for the last quarter.');

SET IDENTITY_INSERT [dbo].[PeerAssessment] OFF
