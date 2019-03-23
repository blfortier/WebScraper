CREATE TABLE [dbo].[Table] (
	[Id] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    [Symbol]        NCHAR(10) NOT NULL,
    [LastPrice]     FLOAT (53)   NOT NULL,
    [Change]        FLOAT (53)   NOT NULL,
    [ChangePercent] FLOAT (53)   NOT NULL,
    [Currency]      NCHAR (10)   NOT NULL,
    [MarketTime]          DATETIME     NOT NULL,
    [Volume]        INT          NOT NULL,
    [AvgVol]        FLOAT (53)   NOT NULL,
    [MarketCap]     INT        NOT NULL    
);

