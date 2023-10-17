CREATE TABLE [dbo].[EmployeeNotification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [AssessmentId] INT NULL,
    [AnnouncementId] INT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [FK_EmployeeNotificationEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeNotificationAssessment] FOREIGN KEY ([AssessmentId]) REFERENCES [Assessment]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeNotificationAnnouncement] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement]([Id]) ON DELETE CASCADE
)
