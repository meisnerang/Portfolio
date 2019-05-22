USE GuildCars
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'BodyStyleSelectAll')
	DROP PROCEDURE BodyStyleSelectAll
GO

CREATE PROCEDURE BodyStyleSelectAll AS
BEGIN

SELECT BodyStyleId, BodyStyleName
FROM BodyStyle

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ColorSelectAll')
	DROP PROCEDURE ColorSelectAll
GO

CREATE PROCEDURE ColorSelectAll AS
BEGIN

SELECT ColorId, ColorName
FROM Color

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'PurchaseTypeSelectAll')
	DROP PROCEDURE PurchaseTypeSelectAll
GO

CREATE PROCEDURE PurchaseTypeSelectAll AS
BEGIN

SELECT PurchaseTypeId, PurchaseTypeName
FROM PurchaseType

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'StatesSelectAll')
	DROP PROCEDURE StatesSelectAll
GO

CREATE PROCEDURE StatesSelectAll AS
BEGIN

SELECT StateId, StateName
FROM States

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialsSelectAll')
	DROP PROCEDURE SpecialsSelectAll
GO

CREATE PROCEDURE SpecialsSelectAll AS
BEGIN

SELECT SpecialId, Title, [Description]
FROM Special 

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialSelectByID')
	DROP PROCEDURE SpecialSelectByID
GO

CREATE PROCEDURE SpecialSelectByID (
@SpecialId INT
) AS

BEGIN

SELECT SpecialId, Title, [Description]
FROM Special
	WHERE Special.SpecialId LIKE @SpecialId
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialDelete')
      DROP PROCEDURE SpecialDelete
GO

CREATE PROCEDURE SpecialDelete (
	@SpecialId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Special WHERE SpecialId = @SpecialId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SpecialAdd')
	DROP PROCEDURE SpecialAdd
GO

CREATE PROCEDURE SpecialAdd (
@SpecialID INT OUTPUT,
@Title VARCHAR(100),
@Description VARCHAR(500)
) AS

BEGIN
	INSERT INTO Special (Title, [Description])
	VALUES(@Title, @Description);

	SET @SpecialID = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactSelectAll')
	DROP PROCEDURE ContactSelectAll
GO

CREATE PROCEDURE ContactSelectAll AS
BEGIN

SELECT ContactId, [Name], Email, Phone, [Message]
FROM Contact
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ContactAdd')
	DROP PROCEDURE ContactAdd
GO

CREATE PROCEDURE ContactAdd (
@ContactID INT OUTPUT,
@Name VARCHAR(35),
@Email VARCHAR(30),
@Phone VARCHAR(13),
@Message VARCHAR(500)
) AS

BEGIN
	INSERT INTO Contact ([Name], Email, Phone, [Message])
	VALUES(@Name, @Email, @Phone, @Message);

	SET @ContactID = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeAdd')
	DROP PROCEDURE MakeAdd
GO

CREATE PROCEDURE MakeAdd (
@MakeID INT OUTPUT,
@MakeName VARCHAR(35),
@UserId NVARCHAR(128)
) AS

BEGIN
	INSERT INTO Make (MakeName, UserId)
	VALUES(@MakeName, @UserId);

	SET @MakeID = SCOPE_IDENTITY();
END

GO 
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeSelectAll')
	DROP PROCEDURE MakeSelectAll
GO

CREATE PROCEDURE MakeSelectAll AS
BEGIN

SELECT MakeId, MakeName, UserId, DateAdded
FROM Make
ORDER BY MakeName
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'MakeItemSelectAll')
	DROP PROCEDURE MakeItemSelectAll
GO

CREATE PROCEDURE MakeItemSelectAll AS
BEGIN

SELECT MakeId, MakeName, AspNetUsers.Email, DateAdded
FROM Make
	INNER JOIN AspNetUsers
	ON AspNetUsers.Id = Make.UserId

	ORDER BY MakeName
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelAdd')
	DROP PROCEDURE ModelAdd
GO

CREATE PROCEDURE ModelAdd (
@ModelID INT OUTPUT,
@ModelName VARCHAR(35),
@MakeID INT,
@UserId NVARCHAR(128)
) AS

BEGIN
	INSERT INTO Model (ModelName, MakeId, UserId)
	VALUES(@ModelName, @MakeID, @UserId);

	SET @ModelID = SCOPE_IDENTITY();
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelSelectAll')
	DROP PROCEDURE ModelSelectAll
GO

CREATE PROCEDURE ModelSelectAll AS
BEGIN

SELECT ModelId, ModelName, Make.MakeName , Model.UserId, Model.DateAdded
FROM Model
	INNER JOIN Make
	ON Make.MakeId = Model.MakeId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelSelectByMake')
	DROP PROCEDURE ModelSelectByMake
GO

CREATE PROCEDURE ModelSelectByMake (
@MakeId INT
) AS
BEGIN

SELECT ModelId, ModelName, MakeId
FROM Model
	
	WHERE MakeId = @MakeId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ModelItemSelectAll')
	DROP PROCEDURE ModelItemSelectAll
GO

CREATE PROCEDURE ModelItemSelectAll AS
BEGIN

SELECT ModelId, ModelName, Make.MakeName , AspNetUsers.Email, Model.DateAdded
FROM Model
	INNER JOIN Make
	ON Make.MakeId = Model.MakeId
	INNER JOIN AspNetUsers
	ON AspNetUsers.Id = Model.UserId
	
	ORDER BY Make.MakeName, ModelName
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'FeaturedSelectAll')
	DROP PROCEDURE FeaturedSelectAll
GO

CREATE PROCEDURE FeaturedSelectAll AS
BEGIN

SELECT VehicleId, PhotoFile, [Year], MakeName, ModelName, SalePrice
FROM Vehicle
	INNER JOIN Make
	ON Vehicle.MakeId = Make.MakeId
	INNER JOIN Model
	ON Vehicle.ModelId = Model.ModelId
	WHERE IsFeatured = 1

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelectAll')
	DROP PROCEDURE VehicleSelectAll
GO

CREATE PROCEDURE VehicleSelectAll AS
BEGIN

SELECT VehicleId, PhotoFile, [Year], MakeName, ModelName, BodyStyleName, IsManualTransmission, C1.ColorName AS Exterior_Color, C2.ColorName AS Interior_Color, Mileage, Vin, [Description], SalePrice, MSRP, IsSold
FROM Vehicle
	INNER JOIN Make
	ON Vehicle.MakeId = Make.MakeId
	INNER JOIN Model
	ON Vehicle.ModelId = Model.ModelId
	INNER JOIN BodyStyle
	ON Vehicle.BodyStyleId = BodyStyle.BodyStyleID
	INNER JOIN Color AS C1
	ON Vehicle.ExteriorColorId = C1.ColorId
	INNER JOIN Color AS C2
	ON Vehicle.InteriorColorId = C2.ColorId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelectById')
	DROP PROCEDURE VehicleSelectById
GO

CREATE PROCEDURE VehicleSelectById (
@VehicleId INT
) AS
BEGIN

SELECT VehicleId, PhotoFile, [Year], MakeName, ModelName, BodyStyleName, IsManualTransmission, C1.ColorName AS Exterior_Color, C2.ColorName AS Interior_Color, Mileage, Vin, [Description], SalePrice, MSRP, IsSold
FROM Vehicle
	INNER JOIN Make
	ON Vehicle.MakeId = Make.MakeId
	INNER JOIN Model
	ON Vehicle.ModelId = Model.ModelId
	INNER JOIN BodyStyle
	ON Vehicle.BodyStyleId = BodyStyle.BodyStyleID
	INNER JOIN Color AS C1
	ON Vehicle.ExteriorColorId = C1.ColorId
	INNER JOIN Color AS C2
	ON Vehicle.InteriorColorId = C2.ColorId

	WHERE Vehicle.VehicleId LIKE @VehicleId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleSelectById_Vehicle')
	DROP PROCEDURE VehicleSelectById_Vehicle
GO

CREATE PROCEDURE VehicleSelectById_Vehicle (
@VehicleId INT
) AS
BEGIN

SELECT VehicleId, PhotoFile, [Year], MakeId, ModelId, BodyStyleId, IsManualTransmission, ExteriorColorId, InteriorColorId, Mileage, Vin, [Description], SalePrice, MSRP, IsSold, IsNew
FROM Vehicle
	
	WHERE VehicleId LIKE @VehicleId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SaleAdd')
	DROP PROCEDURE SaleAdd
GO

CREATE PROCEDURE SaleAdd (
@SalesId INT OUTPUT,
@UserId NVARCHAR(128),
@Name VARCHAR(50),
@Phone VARCHAR(15),
@Email VARCHAR(30),
@Street1 VARCHAR(30),
@Street2 VARCHAR(30),
@City VARCHAR(30),
@StateId CHAR(2),
@Zipcode INT,
@PurchaseTypeId INT,
@VehicleId INT,
@PurchasePrice MONEY
) AS

BEGIN
	INSERT INTO Sales (UserId, [Name], Phone, Email, Street1, Street2, City, StateId, Zipcode, PurchaseTypeId, VehicleId, PurchasePrice)
	VALUES(@UserId, @Name, @Phone, @Email, @Street1, @Street2, @City, @StateId, @Zipcode, @PurchaseTypeId, @VehicleId, @PurchasePrice);

	SET @SalesId = SCOPE_IDENTITY();
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'ChangeToSold')
      DROP PROCEDURE ChangeToSold
GO

CREATE PROCEDURE ChangeToSold (
	@VehicleId INT
) AS
BEGIN
	UPDATE Vehicle SET 
		IsSold = 1
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleAdd')
	DROP PROCEDURE VehicleAdd
GO

CREATE PROCEDURE VehicleAdd (
@VehicleId INT OUTPUT,
@MakeId INT,
@ModelId INT,
@Year INT,
@BodyStyleId INT,
@IsManualTransmission BIT,
@ExteriorColorId INT,
@InteriorColorId INT,
@UserId NVARCHAR(128),
@Mileage INT,
@SalePrice MONEY,
@MSRP MONEY,
@Vin VARCHAR(17),
@Description NVARCHAR(1500),
@PhotoFile VARCHAR(255),
@IsFeatured BIT,
@IsNew BIT,
@IsSold BIT
) AS

BEGIN
	INSERT INTO Vehicle (MakeId, ModelId, [Year], BodyStyleId, IsManualTransmission, ExteriorColorId, InteriorColorId, UserId, Mileage, SalePrice, MSRP, Vin, [Description], PhotoFile, IsFeatured, Isnew, IsSold)
	VALUES(@MakeId, @ModelId, @Year, @BodyStyleId, @IsManualTransmission, @ExteriorColorId, @InteriorColorId, @UserId, @Mileage, @SalePrice, @MSRP, @Vin, @Description, @PhotoFile, @IsFeatured, @IsNew, @IsSold);

	SET @VehicleId = SCOPE_IDENTITY();
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleEdit')
      DROP PROCEDURE VehicleEdit
GO

CREATE PROCEDURE VehicleEdit (
	@VehicleId int,
	@MakeId INT,
	@ModelId INT,
	@Year INT,
	@BodyStyleId INT,
	@IsManualTransmission BIT,
	@ExteriorColorId INT,
	@InteriorColorId INT,
	@UserId NVARCHAR(128),
	@Mileage INT,
	@SalePrice MONEY,
	@MSRP MONEY,
	@Vin VARCHAR(17),
	@Description NVARCHAR(1500),
	@PhotoFile VARCHAR(255),
	@IsFeatured BIT,
	@IsNew BIT,
	@IsSold BIT
) AS
BEGIN
	UPDATE Vehicle SET 
		MakeId = @MakeId, 
		ModelId = @ModelId, 
		[Year] = @Year, 
		BodyStyleId = @BodyStyleId, 
		IsManualTransmission = @IsManualTransmission, 
		ExteriorColorId = @ExteriorColorId, 
		InteriorColorId = @InteriorColorId, 
		UserId = @UserId,
		Mileage = @Mileage, 
		SalePrice = @SalePrice,
		MSRP = @MSRP,
		Vin = @Vin,
		[Description] = @Description,
		PhotoFile = @PhotoFile,
		IsFeatured = @IsFeatured,
		IsNew = @IsNew,
		IsSold = @IsSold
	WHERE VehicleId = @VehicleId
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InventoryReportUsed')
	DROP PROCEDURE InventoryReportUsed
GO

CREATE PROCEDURE InventoryReportUsed AS
BEGIN

Select [Year], m.MakeName AS Make, mo.ModelName AS Model, Count(v.MakeId) AS [Count], SUM(v.MSRP) AS StockValue
FROM Vehicle v
	Inner Join Make m ON v.MakeId = m.MakeId
	Inner Join Model mo ON v.ModelId = mo.ModelId
Where v.IsNew = 0
Group By [Year], v.MakeId, m.MakeName, mo.ModelName

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'InventoryReportNew')
	DROP PROCEDURE InventoryReportNew
GO

CREATE PROCEDURE InventoryReportNew AS
BEGIN

Select [Year], m.MakeName AS Make, mo.ModelName AS Model, Count(v.MakeId) AS [Count], SUM(v.MSRP) AS StockValue
FROM Vehicle v
	Inner Join Make m ON v.MakeId = m.MakeId
	Inner Join Model mo ON v.ModelId = mo.ModelId
Where v.IsNew = 1
Group By [Year], v.MakeId, m.MakeName, mo.ModelName

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'RolesSelectAll')
	DROP PROCEDURE RolesSelectAll
GO

CREATE PROCEDURE RolesSelectAll AS
BEGIN

SELECT Id, [Name]
FROM AspNetRoles

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SelectUsersAndRoles')
	DROP PROCEDURE SelectUsersAndRoles
GO

CREATE PROCEDURE SelectUsersAndRoles AS
BEGIN

Select u.LastName, u.FirstName, u.Email, r.Name AS Role
From AspNetUsers u
	Inner Join AspNetUserRoles ON
	u.Id = AspNetUserRoles.UserId
	Inner Join AspNetRoles r ON
	r.Id = AspNetUserRoles.RoleId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleCount')
	DROP PROCEDURE VehicleCount
GO

CREATE PROCEDURE VehicleCount AS
BEGIN

Select COUNT(*) From Vehicle

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'VehicleDelete')
      DROP PROCEDURE VehicleDelete
GO

CREATE PROCEDURE VehicleDelete (
	@VehicleId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Vehicle WHERE VehicleId = @VehicleId;

	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'SaleDelete')
      DROP PROCEDURE SaleDelete
GO

CREATE PROCEDURE SaleDelete (
	@VehicleId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM Sales Where VehicleId = @VehicleId;
	
	COMMIT TRANSACTION
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'GetMaxVehicleId')
	DROP PROCEDURE GetMaxVehicleId
GO

CREATE PROCEDURE GetMaxVehicleId AS
BEGIN

Select MAX(VehicleId) From Vehicle

END
GO
