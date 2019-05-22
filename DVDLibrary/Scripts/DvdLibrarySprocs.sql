USE DvdLibrary
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdListAll')
	DROP PROCEDURE DvdListAll
GO

CREATE PROCEDURE DvdListAll AS
BEGIN
	SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
	FROM Dvd
		INNER JOIN ReleaseYear
		ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
		INNER JOIN Director
		ON Dvd.DirectorId = Director.DirectorId
		INNER JOIN Rating 
		ON Dvd.RatingId = Rating.RatingId
END
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdGetByID')
	DROP PROCEDURE DvdGetByID
GO

CREATE PROCEDURE DvdGetByID (
@DvdId INT
) AS

BEGIN
	SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
	FROM Dvd
		INNER JOIN ReleaseYear
		ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
		INNER JOIN Director
		ON Dvd.DirectorId = Director.DirectorId
		INNER JOIN Rating 
		ON Dvd.RatingId = Rating.RatingId
	WHERE Dvd.DvdId LIKE @DvdId

END
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdGetByReleaseYear')
      DROP PROCEDURE DvdGetByReleaseYear
GO

CREATE PROCEDURE DvdGetByReleaseYear (
	@ReleaseYearName INT
)
AS

BEGIN
SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
FROM Dvd
	INNER JOIN ReleaseYear
	ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
	INNER JOIN Director
	ON Dvd.DirectorId = Director.DirectorId
	INNER JOIN Rating
	ON Dvd.RatingId = Rating.RatingId
WHERE ReleaseYear.ReleaseYearName LIKE @ReleaseYearName
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdGetByDirector')
      DROP PROCEDURE DvdGetByDirector
GO
	  
CREATE PROCEDURE DvdGetByDirector (
	@DirectorName VARCHAR(30)
)
AS

BEGIN
SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
FROM Dvd
	INNER JOIN ReleaseYear
	ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
	INNER JOIN Director
	ON Dvd.DirectorId = Director.DirectorId
	INNER JOIN Rating
	ON Dvd.RatingId = Rating.RatingId
WHERE Director.DirectorName LIKE '%' + @DirectorName + '%'
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdGetByRating')
      DROP PROCEDURE DvdGetByRating
GO
	  
CREATE PROCEDURE DvdGetByRating (
	@RatingType VARCHAR(10)
)
AS

BEGIN
SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
FROM Dvd
	INNER JOIN ReleaseYear
	ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
	INNER JOIN Director
	ON Dvd.DirectorId = Director.DirectorId
	INNER JOIN Rating
	ON Dvd.RatingId = Rating.RatingId
WHERE Rating.RatingType LIKE @RatingType
END

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdCreate')
	DROP PROCEDURE DvdCreate
GO

CREATE PROCEDURE DvdCreate (
@DvdId INT OUTPUT,
@Title VARCHAR(45),
@Notes VARCHAR(60),
@ReleaseYearName INT,
@DirectorName VARCHAR(30),
@RatingType VARCHAR(10)
)
AS

Declare @ReleaseYearId INT
Declare @DirectorId INT
Declare @RatingId INT

SELECT @ReleaseYearId = ReleaseYearId FROM ReleaseYear WHERE ReleaseYearName = @ReleaseYearName 
SELECT @DirectorId = DirectorId FROM Director WHERE DirectorName = @DirectorName
SELECT @RatingId = RatingId FROM Rating WHERE RatingType = @RatingType



BEGIN
	INSERT INTO Dvd(Title, Notes, ReleaseYearId, DirectorId, RatingId)
	VALUES(@Title, @Notes, @ReleaseYearId, @DirectorId, @RatingId)
	SET @DvdId = SCOPE_IDENTITY();
END
GO	

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdEdit')
	DROP PROCEDURE DvdEdit
GO

CREATE PROCEDURE DvdEdit (
	@DvdId INT,
	@Title VARCHAR(45), 
	@Notes VARCHAR(60), 
	@ReleaseYearName INT, 
	@DirectorName VARCHAR(30), 
	@RatingType VARCHAR(10)
)
AS

Declare @ReleaseYearId INT
Declare @DirectorId INT
Declare @RatingId INT

SELECT @ReleaseYearId = ReleaseYearId FROM ReleaseYear WHERE ReleaseYearName = @ReleaseYearName 
SELECT @DirectorId = DirectorId FROM Director WHERE DirectorName = @DirectorName
SELECT @RatingId = RatingId FROM Rating WHERE RatingType = @RatingType

BEGIN
	UPDATE Dvd 
	SET Title = @Title,
		Notes = @Notes,
		ReleaseYearId = @ReleaseYearId,
		DirectorId = @DirectorId,
		RatingId = @RatingId
	WHERE DvdId = @DvdId
	
END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
	DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete(
	@DvdId INT
	) AS

BEGIN
	
	DELETE FROM Dvd WHERE DvdId = @DvdId

END
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdGetByTitle')
	DROP PROCEDURE DvdGetByTitle
GO

CREATE PROCEDURE DvdGetByTitle (
@Title VARCHAR(45)
) AS

BEGIN
	SELECT DvdId, Title, Notes, ReleaseYearName, DirectorName, RatingType
	FROM Dvd
		INNER JOIN ReleaseYear
		ON Dvd.ReleaseYearId = ReleaseYear.ReleaseYearId
		INNER JOIN Director
		ON Dvd.DirectorId = Director.DirectorId
		INNER JOIN Rating 
		ON Dvd.RatingId = Rating.RatingId
	WHERE Dvd.Title LIKE '%' + @Title + '%'

END
GO