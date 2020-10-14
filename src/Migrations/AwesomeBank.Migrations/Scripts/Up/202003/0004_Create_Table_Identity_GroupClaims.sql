CREATE TABLE [Identity].[GroupClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](64) NOT NULL,
	[ClaimValue] [nvarchar](64) NOT NULL,
	[GroupId] [int] NOT NULL,
	CONSTRAINT [PK_Identity_GroupClaims_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Identity_GroupClaims_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Identity].[Groups] ([Id]),
	INDEX [IX_Identity_GroupClaims_GroupId] NONCLUSTERED ([GroupId] ASC)
);