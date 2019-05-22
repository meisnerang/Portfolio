IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_NAME = 'DbReset')
	DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	
	DELETE FROM Special;
	DELETE FROM Sales;
	DELETE FROM States;
	DELETE FROM Vehicle;
	DELETE FROM Color;
	DELETE FROM BodyStyle;
	DELETE FROM Model;
	DELETE FROM Make;
	DELETE FROM AspNetUsers WHERE id IN ('00000000-0000-0000-0000-000000000000', '11111111-1111-1111-1111-111111111111');
	DELETE FROM Contact;
	DELETE FROM PurchaseType;
	

DBCC CHECKIDENT ('Vehicle', RESEED, 1)
DBCC CHECKIDENT ('Contact', RESEED, 1)
DBCC CHECKIDENT ('Sales', RESEED, 1)
DBCC CHECKIDENT ('Make', RESEED, 1)
DBCC CHECKIDENT ('Model', RESEED, 1)
DBCC CHECKIDENT ('Special', RESEED, 1)



SET IDENTITY_INSERT Color ON

INSERT INTO Color(ColorId, ColorName)
VALUES	(1,'Sunshine Orange'),
		(2,'Venetian Red Pearl'),
		(3, 'Ice Silver'),
		(4, 'Magnetite Grey'),
		(5,'Crystal Black Silica'),
		(6, 'Sepia Bronze'),
		(7, 'Crystal White Pearl'),
		(8, 'Dark Grey'),
		(9, 'Cinnamon Brown Pearl'),
		(10, 'Midnight Black'),
		(11, 'Cool Grey Khaki'),
		(12, 'Grey'),
		(13, 'Black'),
		(14, 'Ivory'),
		(15, 'Java Brown'),
		(16, 'Beige')

SET IDENTITY_INSERT Color OFF

SET IDENTITY_INSERT BodyStyle ON

INSERT INTO BodyStyle(BodyStyleId, BodyStyleName)
VALUES	(1,'Car'),
		(2,'SUV'),
		(3,'Truck'),
		(4,'Van')

SET IDENTITY_INSERT BodyStyle OFF

INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName)
	VALUES('00000000-0000-0000-0000-000000000000', 0, 0, 'tester1@gmail.com', 0, 0, 0, 'tester1@gmail.com', 'Antoinette', 'LaVelle');

	INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, FirstName, LastName)
	VALUES('11111111-1111-1111-1111-111111111111', 0, 0, 'tester2@yahoo.com', 0, 0, 0, 'tester2@yahoo.com', 'Georgia', 'Germaine');

SET IDENTITY_INSERT Make ON

INSERT INTO Make(MakeId, MakeName, UserId, DateAdded)
VALUES	(1,'Subaru', '00000000-0000-0000-0000-000000000000', CONVERT(datetime2, '10/29/2018', 101)),
		(2, 'Honda', '00000000-0000-0000-0000-000000000000', CONVERT(datetime2, '10/27/2018', 101)),
		(3, 'Ford', '00000000-0000-0000-0000-000000000000', CONVERT(datetime2, '10/28/2018', 101)),
		(4, 'Dodge', '00000000-0000-0000-0000-000000000000', CONVERT(datetime2, '10/28/2018', 101)) 

SET IDENTITY_INSERT Make OFF

SET IDENTITY_INSERT Model ON

INSERT INTO Model(ModelId, ModelName, MakeId, UserId, DateAdded)
VALUES	(1, 'Crosstrek', 1, '00000000-0000-0000-0000-000000000000', '10/01/2018'),
		(2, 'Outback', 1, '00000000-0000-0000-0000-000000000000', '10/19/2018'),
		(3, 'Forester', 1, '00000000-0000-0000-0000-000000000000', '10/01/2018'),
		(4, 'Ascent', 1, '00000000-0000-0000-0000-000000000000', '10/27/2018'),
		(5, 'Accord', 2, '00000000-0000-0000-0000-000000000000', '10/26/2018'),
		(6, 'Edge', 3, '00000000-0000-0000-0000-000000000000', '10/25/2018'),
		(7, 'Dakota', 4, '00000000-0000-0000-0000-000000000000', '10/24/2018')

SET IDENTITY_INSERT Model OFF

SET IDENTITY_INSERT Vehicle ON;

INSERT INTO Vehicle(VehicleId, MakeId, ModelId, [Year], BodyStyleId , IsManualTransmission, ExteriorColorId, InteriorColorId, UserId, Mileage, SalePrice, MSRP, Vin, [Description], PhotoFile, IsFeatured, IsNew, IsSold, DateAdded)
VALUES	(1, 1, 1, 2019, 1, 1, 1, 1, '00000000-0000-0000-0000-000000000000', 0, 24980, 26980, '123456789AAAAAAAA', 'Sample data vehicle description', 'inventory-1.jpg', 0, 1, 1, '01/09/2019'),  
		(2, 1, 1, 2019, 1, 0, 2, 2, '00000000-0000-0000-0000-000000000000', 0, 25983, 27983,  '123456789AAAAAAAB', 'Sample data vehicle description', 'inventory-2.jpg', 1, 1, 0, '01/01/2019'),
		(3, 1, 1, 2019, 1, 0, 3, 2, '00000000-0000-0000-0000-000000000000', 0,  28048, 30048, '123456789AAAAAAAC', 'Sample data vehicle description', 'inventory-3.jpg', 1, 1, 0, '01/01/2019'),
		(4, 1, 2, 2019, 1, 0, 4, 2, '00000000-0000-0000-0000-000000000000', 0, 34535, 36535, '123456789AAAAAAAD', 'Sample data vehicle description', 'inventory-4.jpg', 0, 1, 1, '01/01/2019'),
		(5, 1, 2, 2019, 1, 0, 5, 3, '00000000-0000-0000-0000-000000000000', 0, 30930, 32930, '123456789AAAAAAAE', 'Sample data vehicle description', 'inventory-5.jpg', 1, 1, 0, '01/01/2019'),
		(6, 1, 2, 2019, 1, 0, 3, 2, '00000000-0000-0000-0000-000000000000', 0, 38896, 40896, '123456789AAAAAAAF', 'Sample data vehicle description', 'inventory-6.jpg', 0,  1, 0, '01/01/2019'),
		(7, 1, 3, 2019, 2, 1, 6, 2, '00000000-0000-0000-0000-000000000000', 0, 27056, 29056, '123456789AAAAAAAG', 'Sample data vehicle description', 'inventory-7.jpg', 0, 1, 0, '01/01/2019'),
		(8, 1, 3, 2019, 2, 0, 7, 1, '00000000-0000-0000-0000-000000000000', 0, 30948, 32948, '123456789AAAAAAAH', 'Sample data vehicle description', 'inventory-8.jpg', 0, 1, 0, '01/01/2019'),
		(9, 1, 3, 2019, 2, 0, 8, 1, '00000000-0000-0000-0000-000000000000', 0, 27806, 29806, '123456789AAAAAAAI', 'Sample data vehicle description', 'inventory-9.jpg', 0, 1, 1, '01/01/2019'),
		(10, 1, 4, 2019, 2, 0, 9, 4, '00000000-0000-0000-0000-000000000000', 0, 44798, 46798, '123456789AAAAAAAJ', 'Sample data vehicle description', 'inventory-10.jpg', 1, 1, 0, '01/01/2019'),
		(11, 2, 5, 2014, 1, 0, 10, 3, '00000000-0000-0000-0000-000000000000', 76500, 14000, 16000, '123456789AAAAAAAK', 'Sample data vehicle description', 'inventory-11.jpg', 0, 0, 0, '04/01/2018'),
		(12, 3, 6, 2010, 2, 0, 7, 5, '00000000-0000-0000-0000-000000000000', 115800, 6750, 8750, '123456789AAAAAAAL', 'Sample data vehicle description', 'inventory-12.jpg', 0, 0, 0, '04/01/2018'),
		(13, 2, 5, 2017, 1, 0, 10, 2,'00000000-0000-0000-0000-000000000000', 32945, 21500, 23500, '123456789AAAAAAAM', 'Sample data vehicle description', 'inventory-13.jpg', 0, 0, 0, '04/01/2018'),
		(14, 1, 1, 2018, 1, 0, 11, 2, '00000000-0000-0000-0000-000000000000', 14250, 23750, 25750, '123456789AAAAAAAN', 'Sample data vehicle description', 'inventory-14.jpg', 0, 0, 0, '04/01/2018'),
		(15, 4, 7, 2003, 3, 0, 9, 2, '00000000-0000-0000-0000-000000000000', 184795, 8500, 10500, '123456789AAAAAAAO', 'Sample data vehicle description', 'inventory-15.jpg', 0, 0, 1, '04/01/2018')

SET IDENTITY_INSERT Vehicle OFF;

SET IDENTITY_INSERT Contact ON

INSERT INTO Contact (ContactId, [Name], Email, Phone, [Message])
VALUES	(1, 'Stan Getz', 'sgetz@gmail.com', '502-312-1132', 'What are your hours?'),
		(2, 'Aurora Gutierrez', 'aguti@yahoo.com', '415.233.9078', 'Are you open on Sundays?'),
		(3, 'Charlie Parker', 'cparker@gmail.com', '312-456-9090', 'Is this car still on sale?'),
		(4, 'Jenna Lutz', 'jlutz@gmail.com', '319-400-9090', 'Is this car still on sale?'),
		(5, 'Bertha Ruoff', 'bruoff@gmail.com', '812-456-8888', 'Is this car still on sale?'),
		(6, 'Gertrude Stein', 'gstein@gmail.com', '212-456-7878', 'Is this car still on sale?')

SET IDENTITY_INSERT Contact OFF


INSERT INTO States (StateId, StateName)
VALUES	('IN', 'Indiana'),
		('KY', 'Kentucky'),
		('TN', 'Tennessee'),
		('OH', 'Ohio')



SET IDENTITY_INSERT PurchaseType ON

INSERT INTO PurchaseType (PurchaseTypeId, PurchaseTypeName)
VALUES	(1, 'Bank Finance'),
		(2, 'Cash'),
		(3, 'Dealer Finance')

SET IDENTITY_INSERT PurchaseType OFF

SET IDENTITY_INSERT Sales ON

INSERT INTO Sales (SalesId, UserId, [Name], Phone, Email, Street1, Street2, City, StateId, Zipcode, PurchaseTypeId, VehicleId, PurchasePrice)
VALUES	(1, '00000000-0000-0000-0000-000000000000', 'Lois Lane', '123-444-8989', 'lane@gmail.com', '513 Main St', NULL, 'Shepherdsville', 'KY', 44444, 1, 1, 26000.00),
		(2, '00000000-0000-0000-0000-000000000000', 'Superman', '777-999-3434','spidey@comics.com', '200 Mulberry Ln', NULL, 'New Albany', 'IN', 47155, 2, 4, 35615.00),
		(3, '00000000-0000-0000-0000-000000000000', 'Wonderwoman', '999-666-6996', 'iamwoman@yahoo.com', '23 Oak Ave', NULL, 'Louisville', 'KY', 40777, 3, 9, 29500.00),
		(4, '11111111-1111-1111-1111-111111111111', 'SpiderMan', '124-909-7878', 'net3@sticky.org', '4567 Mudd Ln', NULL, 'Corydon', 'IN', 47112, 1, 15, 9000.00)

SET IDENTITY_INSERT Sales OFF

SET IDENTITY_INSERT Special ON
INSERT INTO Special (SpecialId, Title, [Description])
VALUES	(1, '10% Off All 2019 Models!', 'In sodales erat arcu, sed suscipit diam aliquet vel. Maecenas consequat est non tortor dictum suscipit. Fusce lectus orci, bibendum pharetra libero et, porta porta tortor. Nullam sodales, leo ut gravida fermentum, turpis mi dictum tortor, sit amet laoreet nisl ante eu nisi. Curabitur tristique metus id mi facilisis consequat. Quisque ultrices tellus metus, nec pretium lacus posuere sed. Cras laoreet velit a leo bibendum, a auctor ipsum molestie.'),
		(2, '$1200 Cash Back on ALL Vans!!', 'In sodales erat arcu, sed suscipit diam aliquet vel. Maecenas consequat est non tortor dictum suscipit. Fusce lectus orci, bibendum pharetra libero et, porta porta tortor. Nullam sodales, leo ut gravida fermentum, turpis mi dictum tortor, sit amet laoreet nisl ante eu nisi. Curabitur tristique metus id mi facilisis consequat. Quisque ultrices tellus metus, nec pretium lacus posuere sed. Cras laoreet velit a leo bibendum, a auctor ipsum molestie.'),
		(3, '$1500 Cash Back on ALL Trucks!!', 'In sodales erat arcu, sed suscipit diam aliquet vel. Maecenas consequat est non tortor dictum suscipit. Fusce lectus orci, bibendum pharetra libero et, porta porta tortor. Nullam sodales, leo ut gravida fermentum, turpis mi dictum tortor, sit amet laoreet nisl ante eu nisi. Curabitur tristique metus id mi facilisis consequat. Quisque ultrices tellus metus, nec pretium lacus posuere sed. Cras laoreet velit a leo bibendum, a auctor ipsum molestie.'),
		(4, '20% Off All 2018 Models!', 'In sodales erat arcu, sed suscipit diam aliquet vel. Maecenas consequat est non tortor dictum suscipit. Fusce lectus orci, bibendum pharetra libero et, porta porta tortor. Nullam sodales, leo ut gravida fermentum, turpis mi dictum tortor, sit amet laoreet nisl ante eu nisi. Curabitur tristique metus id mi facilisis consequat. Quisque ultrices tellus metus, nec pretium lacus posuere sed. Cras laoreet velit a leo bibendum, a auctor ipsum molestie.')

SET IDENTITY_INSERT Special OFF
	
END
	 