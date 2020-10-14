CREATE TABLE [Identity].[UserGroups](
	[UserId] [uniqueidentifier] NOT NULL,
	[GroupId] [int] NOT NULL,
	CONSTRAINT [PK_Identity_UserGroups_GroupId_UserId] PRIMARY KEY CLUSTERED ([GroupId] ASC, [UserId] ASC),
	CONSTRAINT [FK_Identity_UserGroups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Identity].[Groups] ([Id]),
	CONSTRAINT [FK_Identity_UserGroups_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id]),
	INDEX [IX_Identity_USerGroups_GroupId] NONCLUSTERED ([GroupId]),
	INDEX [IX_Identity_USerGroups_UserId] NONCLUSTERED ([UserId])
);