CREATE TABLE [dbo].[DatabaseServers]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [DatabaseServer] VARCHAR(300) NULL, 
    [DatabaseName] VARCHAR(50) NULL, 
    [Region] VARCHAR(50) NULL

)
