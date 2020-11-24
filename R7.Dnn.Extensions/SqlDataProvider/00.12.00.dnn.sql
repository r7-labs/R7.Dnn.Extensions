-- NOTE: To manually execute this script you must 
-- replace {databaseOwner} and {objectQualifier} with real values. 
-- Defaults is "dbo." for database owner and "" for object qualifier 

IF EXISTS (select * from sys.procedures where name = N'{objectQualifier}r7_DnnExtensions_DropDefaultConstraint')
    DROP PROCEDURE {databaseOwner}[{objectQualifier}r7_DnnExtensions_DropDefaultConstraint]
GO

CREATE PROCEDURE {databaseOwner}[{objectQualifier}r7_DnnExtensions_DropDefaultConstraint]
    @tableName nvarchar (255),
    @columnName nvarchar (255)
AS
BEGIN
    DECLARE @sql nvarchar (max)
    SELECT TOP (1) @sql = N'ALTER TABLE {databaseOwner}[{objectQualifier}'+ @tableName +'] DROP CONSTRAINT [' + DC.name + N']'
        FROM sys.default_constraints DC
        JOIN sys.columns C ON C.default_object_id = DC.object_id
        WHERE DC.parent_object_id = OBJECT_ID (@tableName) AND C.name = @columnName
    IF @@ROWCOUNT = 1 EXECUTE sp_executesql @sql
END
GO
