CREATE TABLE [dbo].[ProgrammingLanguages]
(
	[ProgrammingLanguageId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Language] NVARCHAR(50) NOT NULL, 
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
)
