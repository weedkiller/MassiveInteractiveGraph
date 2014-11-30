CREATE TABLE [dbo].[Link] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    [Node1Id] INT NOT NULL,
    [Node2Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Link_Node1Id] FOREIGN KEY ([Node1Id]) REFERENCES [dbo].[Node] ([Id]),
    CONSTRAINT [FK_Link_Node2Id] FOREIGN KEY ([Node2Id]) REFERENCES [dbo].[Node] ([Id])
);

