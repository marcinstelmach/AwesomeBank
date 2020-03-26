CREATE TABLE [Identity].[ApplicationGroupClaims](
	[ApplicationGroupId] [int] NOT NULL,
	[ClaimId] [int] NOT NULL,
	CONSTRAINT [PK_Identity_ApplicationGroupClaims_ApplicationGroupId_ClaimId] PRIMARY KEY CLUSTERED ([ApplicationGroupId] ASC, [ClaimId] ASC),
	CONSTRAINT [FK_Identity_ApplicationGroupClaims_ApplicationGroups_ApplicationGroupId] FOREIGN KEY ([ApplicationGroupId]) REFERENCES [Identity].[ApplicationGroups] ([Id]),
	CONSTRAINT [FK_Identity_ApplicationGroupClaims_Claims_ClaimId] FOREIGN KEY ([ClaimId]) REFERENCES [Identity].[Claims] ([Id])
);