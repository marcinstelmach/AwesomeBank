CREATE TABLE [Identity].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[PasswordHash] [nvarchar](1024) NOT NULL,
	[SecurityStamp] [nvarchar](1024) NOT NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT 0,
	[CreationDateTime] [datetimeoffset](7) NOT NULL,
	[BirthDayDate] [smalldatetime] NULL,
	[DocumentType] [int] NULL,
	[DocumentValue] [nvarchar](256) NULL
	CONSTRAINT [PK_Identity_Users_Id] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [UX_Identity_Users_NormalizedEmail] UNIQUE NONCLUSTERED ([Email])
);