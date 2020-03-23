CREATE TABLE [Identity].[Claims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](128) NULL,
	[ClaimValue] [nvarchar](128) NULL,
	CONSTRAINT [PK_Identity_Claims_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);