CREATE TABLE [Identity].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	CONSTRAINT [PK_Identity_Roles_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE UNIQUE NONCLUSTERED INDEX [UX_Identity_Roles_NormalizedName] ON [Identity].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL);