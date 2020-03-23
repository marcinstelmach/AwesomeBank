CREATE TABLE [Identity].[ApplicationUserGroups](
	[UserId] [uniqueidentifier] NOT NULL,
	[ApplicationGroupId] [int] NOT NULL,
	CONSTRAINT [PK_Identity_ApplicationUserGroups_ApplicationGroupId_UserId] PRIMARY KEY CLUSTERED ([ApplicationGroupId] ASC, [UserId] ASC),
	CONSTRAINT [FK_Identity_ApplicationUserGroups_ApplicationGroups_ApplicationGroupId] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [Identity].[ApplicationGroups] ([Id]),
	CONSTRAINT [FK_Identity_ApplicationUserGroups_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Identity].[Users] ([Id])
);