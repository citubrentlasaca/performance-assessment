CREATE TABLE [dbo].[EmployeeAnnouncementNotification]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [EmployeeId] INT NOT NULL,
    [AnnouncementId] INT NULL,
    [IsRead] BIT NOT NULL DEFAULT 0,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [FK_EmployeeAnnouncementNotificationEmployee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeAnnouncementNotificationAnnouncement] FOREIGN KEY ([AnnouncementId]) REFERENCES [Announcement]([Id]) ON DELETE CASCADE
)
