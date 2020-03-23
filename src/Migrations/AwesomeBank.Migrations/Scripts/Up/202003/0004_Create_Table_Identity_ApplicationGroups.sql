CREATE TABLE [Identity].[ApplicationGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	CONSTRAINT [PK_Identity_ApplicationGroups_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE UNIQUE NONCLUSTERED INDEX [UX_Identity_ApplicationGroups_NormalizedName] ON [Identity].[ApplicationGroups]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL);