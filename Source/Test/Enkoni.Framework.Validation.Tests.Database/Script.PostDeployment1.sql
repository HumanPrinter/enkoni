/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\PopulateRegularPhoneNumber.sql
:r .\PopulateMobilePhoneNumber.sql
:r .\PopulateServicePhoneNumber.sql
:r .\PopulateOtherPhoneNumber.sql
:r .\PopulateDefaultPhoneNumber.sql

:r .\PopulateBasicEmail.sql
:r .\PopulateExtendedEmail.sql
:r .\PopulateCompleteEmail.sql

:r .\PopulateIbanAccountNumber.sql