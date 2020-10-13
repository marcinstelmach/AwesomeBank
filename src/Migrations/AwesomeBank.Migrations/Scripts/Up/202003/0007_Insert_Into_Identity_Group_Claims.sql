DECLARE @adminsGroupId [int];
DECLARE @clientsGroupId [int];
DECLARE @bankOfficeWorkersGroup [int];
SET @adminsGroupId = (SELECT [Id] FROM [Identity].[Groups] WHERE [NormalizedName] = 'ADMINS');
SET @clientsGroupId = (SELECT [Id] FROM [Identity].[Groups] WHERE [NormalizedName] = 'CLIENTS');
SET @bankOfficeWorkersGroup = (SELECT [Id] FROM [Identity].[Groups] WHERE [NormalizedName] = 'BANK_OFFICE_WORKERS');

INSERT INTO [Identity].[GroupClaims] ([ClaimType], [ClaimValue], [GroupId])
VALUES
	('permission', 'account.get', @adminsGroupId),
	('permission', 'account.manage', @adminsGroupId),
	('permission', 'account.get', @bankOfficeWorkersGroup),
	('permission', 'account.manage', @bankOfficeWorkersGroup),
	('permission', 'account.get', @clientsGroupId);
