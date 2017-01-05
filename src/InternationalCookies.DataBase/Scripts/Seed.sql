
if not exists(select 1 from dbo.stores) 
BEGIN

INSERT INTO dbo.DatabaseServers(DatabaseServer, DatabaseName, Region) values ('internationalcookiesq2bpnesdylzbk.database.windows.net', 'internationalcookies-db-we', 'WE')
INSERT INTO dbo.DatabaseServers(DatabaseServer, DatabaseName, Region) values ('internationalcookiesq2bpneshskdj.database.windows.net', 'internationalcookies-db-bs', 'BS')
INSERT INTO dbo.DatabaseServers(DatabaseServer, DatabaseName, Region) values ('internationalcookiesq2bpnesdjkhf.database.windows.net', 'internationalcookies-db-cus', 'CUS')
INSERT INTO dbo.DatabaseServers(DatabaseServer, DatabaseName, Region) values ('internationalcookiesq2bppqoeyns.database.windows.net', 'internationalcookies-db-sea', 'SEA')


INSERT INTO dbo.cookies (ImageUrl, [Name], Price) values ('https://intcookie.azureedge.net/cdn/cookie-cc.jpg', 'Chololate Chip', 1.2)
INSERT INTO dbo.cookies (ImageUrl, [Name], Price) values ('https://intcookie.azureedge.net/cdn/cookie-bc.jpg', 'Butter Cookie', 1.0)
INSERT INTO dbo.cookies (ImageUrl, [Name], Price) values ('https://intcookie.azureedge.net/cdn/cookie-mc.jpg', 'Macaroons', 0.9)

INSERT INTO dbo.stores (Country, [Name], [DatabaseServerId], [Id]) values ('Netherlands', 'Holland Cookies', 1, '2b76901f-ca39-417d-bfc3-498d0bb56e20')
INSERT INTO dbo.stores (Country, [Name], [DatabaseServerId], [Id]) values ('United States', 'Excellent Cookies', 3, '73e31e10-0677-40c8-813f-6308da54a5ec')
INSERT INTO dbo.stores (Country, [Name], [DatabaseServerId], [Id]) values ('Brazil', 'Yummie Cookies', 2, '0d2d3700-2be0-4037-813c-1ae25dec1aa9')
INSERT INTO dbo.stores (Country, [Name], [DatabaseServerId], [Id]) values ('Singapore', 'Lovely Cookies', 4, '56b12bca-ffab-4b6b-890b-5b37a70bd0e8')


END
