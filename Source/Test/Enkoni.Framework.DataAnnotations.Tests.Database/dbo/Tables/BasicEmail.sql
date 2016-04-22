CREATE TABLE [dbo].[BasicEmail] (
    [Id]                     INT            IDENTITY (1, 1) NOT NULL,
    [MailAddress]            NVARCHAR (MAX) NOT NULL,
    [IsValid]                BIT            NOT NULL,
    [ContainsComment]        BIT            NOT NULL,
    [ContainsIPAddress]      BIT            NOT NULL,
    [ContainsTopLevelDomain] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

