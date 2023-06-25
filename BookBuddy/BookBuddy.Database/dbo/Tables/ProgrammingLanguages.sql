CREATE TABLE [dbo].[ProgrammingLanguages]
(
	[ProgrammingLanguageId] INT NOT NULL PRIMARY KEY, 
    [ProgrammingLanguage] NVARCHAR(50) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
)
