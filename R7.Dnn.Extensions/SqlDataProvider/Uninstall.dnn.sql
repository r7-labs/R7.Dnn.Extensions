-- NOTE: To manually execute this script you must 
-- replace {databaseOwner} and {objectQualifier} with real values. 
-- Defaults is "dbo." for database owner and "" for object qualifier 

IF EXISTS (select * from sys.procedures where name = N'{objectQualifier}r7_DnnExtensions_DropDefaultConstraint')
    DROP PROCEDURE {databaseOwner}[{objectQualifier}r7_DnnExtensions_DropDefaultConstraint]
GO