/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Code]
      ,[Name]
      ,[RegionImageUrl]
  FROM [nzwalksdb].[dbo].[Regions]

  Insert into Regions
  ([Id],[Code],[Name],[RegionImageUrl])
  values (CONVERT(uniqueidentifier,'C56A4180-65AA-42EC-A945-5FD21DEC0538'), 'AKL', 'Auckland', 'some-image-url.jpg');
