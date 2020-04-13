CREATE TABLE [dbo].[AspNetUserMessages] (
    [From]     NVARCHAR (128) NOT NULL,
    [To]       NVARCHAR (128) NOT NULL,
    [Text]     NTEXT          NOT NULL,
    [SentDate] DATETIME       NOT NULL,
    [ReadDate] DATETIME       NULL,
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AspNetUserMessages_AspNetUsers_From] FOREIGN KEY ([From]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AspNetUserMessages_AspNetUsers_To] FOREIGN KEY ([To]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

