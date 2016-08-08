/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO TestDummy (TextValue, NumericValue, BooleanValue)
VALUES
('TestDummy 1', 11, 1),
('TestDummy 2', 12, 1),
('TestDummy 3', 13, 0),
('TestDummy 4', 14, 0)