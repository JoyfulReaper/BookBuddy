CREATE TABLE [dbo].[ProgrammingLanguages]
(
	[ProgrammingLanguageId] INT NOT NULL PRIMARY KEY, 
    [Language] NVARCHAR(50) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
)
