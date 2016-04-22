CREATE TABLE [dbo].[IbanAccountNumber] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [AccountNumber] NVARCHAR (20) NOT NULL,
    [IsValid]       BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

