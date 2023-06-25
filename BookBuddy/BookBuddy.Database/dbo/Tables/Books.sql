CREATE TABLE [dbo].[Books]
(
	[BookId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(200) NOT NULL, 
    [AuthorId] INT NULL, 
    [PublisherId] INT NULL, 
    [BookFormatId] INT NULL, 
    [ProgrammingLanguageId] INT NULL, 
    [ISBN] NVARCHAR(20) NULL, 
    [PublicationYear] INT NOT NULL, 
    [Genre] NVARCHAR(100) NULL, 
    [Website] NVARCHAR(100),
    [Notes] NVARCHAR(2000),
    [DateCreated] DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    [DateDeleted] DATETIME2 NULL, 
    CONSTRAINT [FK_Books_Authors] FOREIGN KEY ([AuthorId]) REFERENCES [Authors]([AuthorId]), 
    CONSTRAINT [FK_Books_Publishers] FOREIGN KEY ([PublisherId]) REFERENCES [Publishers]([PublisherId]), 
    CONSTRAINT [FK_Books_Formats] FOREIGN KEY ([BookFormatId]) REFERENCES [BookFormats]([BookFormatId]),
    CONSTRAINT [FK_Books_ProgrammingLanguageId] FOREIGN KEY ([ProgrammingLanguageId]) REFERENCES [ProgrammingLanguages]([ProgrammingLanguageId])
)
