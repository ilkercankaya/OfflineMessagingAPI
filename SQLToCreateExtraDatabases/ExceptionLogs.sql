CREATE TABLE [dbo].[ExceptionLogs] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [Message]    NTEXT    NULL,
    [Source]     NTEXT    NULL,
    [StackTrace] NTEXT    NULL,
    [Time]       DATETIME NOT NULL,
    CONSTRAINT [PK_ExceptionLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

