CREATE TABLE [Identity].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[NormalizedName] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_Identity_Groups_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UX_Identity_Groups_NormalizedName] UNIQUE NONCLUSTERED ([NormalizedName] ASC)
);