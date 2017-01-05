CREATE TABLE [dbo].[Orders]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Date] DATETIMEOFFSET NULL, 
    [Price] FLOAT NULL, 
	[Status] NVARCHAR(50) NULL,
    [StoreId] uniqueidentifier NULL, 
    CONSTRAINT [StoreForeignKey] FOREIGN KEY (StoreId) REFERENCES [dbo].[Stores](Id) 
)
