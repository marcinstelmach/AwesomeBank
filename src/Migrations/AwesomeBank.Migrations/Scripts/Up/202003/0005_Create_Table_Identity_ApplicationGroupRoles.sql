CREATE TABLE [Identity].[ApplicationGroupRoles](
	[ApplicationGroupId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	CONSTRAINT [PK_Identity_ApplicationGroupRoles_ApplicationGroupId_RoleId] PRIMARY KEY CLUSTERED ([ApplicationGroupId] ASC, [RoleId] ASC),
	CONSTRAINT [FK_Identity_ApplicationGroupRoles_ApplicationGroups_ApplicationGroupId] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [Identity].[ApplicationGroups] ([Id]),
	CONSTRAINT [FK_Identity_ApplicationGroupRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id])
);