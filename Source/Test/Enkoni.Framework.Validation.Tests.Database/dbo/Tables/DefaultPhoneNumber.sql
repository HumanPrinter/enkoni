CREATE TABLE [dbo].[DefaultPhoneNumber] (
    [Id]                        INT           IDENTITY (1, 1) NOT NULL,
    [PhoneNumber]               NVARCHAR (20) NOT NULL,
    [IsValid]                   BIT           NOT NULL,
    [ContainsCountryAccessCode] BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

