CREATE TABLE [dbo].[AspNetBlockedUsers] (
    [From] NVARCHAR (128) NOT NULL,
    [To]   NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AspNetBlockedUsers] PRIMARY KEY CLUSTERED ([From] ASC, [To] ASC),
    CONSTRAINT [FK_AspNetBlockedUsers_AspNetUsers_From] FOREIGN KEY ([From]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_AspNetBlockedUsers_AspNetUsers_To] FOREIGN KEY ([To]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

