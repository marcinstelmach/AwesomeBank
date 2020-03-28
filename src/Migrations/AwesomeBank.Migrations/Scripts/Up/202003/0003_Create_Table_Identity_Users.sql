﻿CREATE TABLE [Identity].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PasswordHash] [nvarchar](1024) NULL,
	[SecurityStamp] [nvarchar](36) NULL,
	CONSTRAINT [PK_Identity_Users_Id] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [FK_Identity_Roles_Id] FOREIGN KEY ([RoleId]) REFERENCES [Identity].[Roles] ([Id])
);

CREATE UNIQUE NONCLUSTERED INDEX [UX_Identity_Users_NormalizedEmail] ON [Identity].[Users]
(
	[Email]
);