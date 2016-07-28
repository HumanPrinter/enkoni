CREATE TABLE [dbo].[TestDummy] (
    [RecordId]     INT            IDENTITY (1, 1) NOT NULL,
    [TextValue]    NVARCHAR (100) NOT NULL,
    [NumericValue] INT            NOT NULL,
    [BooleanValue] BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([RecordId] ASC)
);

