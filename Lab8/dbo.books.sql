CREATE TABLE [dbo].[Books] (
    [Id]                INT          NOT NULL,
    [Title]             VARCHAR (50) NOT NULL,
    [Author]            VARCHAR (50) NOT NULL,
    [Year]              INT          NOT NULL,
    [Author_address]    VARCHAR (50) NOT NULL,
    [Publisher_address] VARCHAR (50) NOT NULL,
    [Price]             INT          NOT NULL,
    [Trader]            VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

