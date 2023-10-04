﻿CREATE TABLE [dbo].[Employee]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] INT NOT NULL,
    [TeamId] INT NOT NULL,
    [Status] NVARCHAR(MAX) NOT NULL,
    [DateTimeJoined] DATETIME NOT NULL,
    CONSTRAINT [FK_EmployeeUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeTeam] FOREIGN KEY ([TeamId]) REFERENCES [Team]([Id]) ON DELETE CASCADE
)