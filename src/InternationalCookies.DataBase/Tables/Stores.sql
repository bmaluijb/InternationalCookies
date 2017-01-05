CREATE TABLE [dbo].[Stores]
(
	[Id] uniqueidentifier NOT NULL DEFAULT NEWID() PRIMARY KEY,
    [Name] VARCHAR(50) NULL, 
    [Country] VARCHAR(50) NULL,
	[DatabaseServerId] INT NULL,
	CONSTRAINT [DatabaseServerForeignKey] FOREIGN KEY (DatabaseServerId) REFERENCES [dbo].[DatabaseServers](Id) 
)
