CREATE TABLE [dbo].[Link]
(
	[Node1Id] INT NOT NULL,
	[Node2Id] INT NOT NULL,
    CONSTRAINT [FK_Link_Node1Id] FOREIGN KEY ([Node1Id]) REFERENCES [dbo].[Node] ([Id]),
    CONSTRAINT [FK_Link_Node2Id] FOREIGN KEY ([Node2Id]) REFERENCES [dbo].[Node] ([Id])
)
