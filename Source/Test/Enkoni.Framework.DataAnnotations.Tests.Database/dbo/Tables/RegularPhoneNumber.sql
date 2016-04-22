CREATE TABLE [dbo].[RegularPhoneNumber] (
    [Id]                        INT           IDENTITY (1, 1) NOT NULL,
    [PhoneNumber]               NVARCHAR (25) NOT NULL,
    [IsValid]                   BIT           NOT NULL,
    [ContainsCountryAccessCode] BIT           NOT NULL,
    [ContainsCarrierPreselect]  BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

