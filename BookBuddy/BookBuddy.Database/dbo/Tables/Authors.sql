﻿CREATE TABLE [dbo].[Authors]
(
	[AuthorId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
)
