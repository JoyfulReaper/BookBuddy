﻿CREATE TABLE [dbo].[Publishers]
(
	[PublisherId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(50) NOT NULL,
    [Website] NVARCHAR(50) NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
)
