CREATE TABLE [dbo].[Cookies]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NULL, 
    [ImageUrl] NVARCHAR(150) NULL, 
    [Price] FLOAT NULL
)
