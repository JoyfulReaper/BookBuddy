﻿CREATE TABLE [dbo].[Publishers]
(
	[PublisherId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Name] NVARCHAR(50),
    [Website] NVARCHAR(50) NULL
)
