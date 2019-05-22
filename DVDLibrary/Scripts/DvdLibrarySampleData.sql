USE DvdLibrary
GO

SET IDENTITY_INSERT Rating ON

INSERT INTO Rating(RatingId, RatingType)
VALUES	(1,'G'),
		(2, 'PG'),
		(3, 'PG-13'),
		(4, 'R'),
		(5, 'R-17')

SET IDENTITY_INSERT Rating OFF

SET IDENTITY_INSERT Director ON

INSERT INTO Director(DirectorId, DirectorName)
VALUES	(1, 'Lisa Cholodenko'),
		(2, 'Patty Jenkins'),
		(3, 'Kathryn Bigelow'),
		(4, 'Gurinder Chadha'),
		(5, 'Sofia Coppola')

SET IDENTITY_INSERT Director OFF

SET IDENTITY_INSERT ReleaseYear ON

INSERT INTO ReleaseYear(ReleaseYearId, ReleaseYearName)
VALUES	(1, 1998),
		(2, 2017),
		(3, 2008),
		(4, 2002),
		(5, 2003)

SET IDENTITY_INSERT ReleaseYear OFF

SET IDENTITY_INSERT Dvd ON

INSERT INTO Dvd(DvdId, Title, Notes, ReleaseYearId, DirectorId, RatingId)
VALUES	(1, 'High Art', 'Drama/Indie film', 1,1,4),
		(2, 'Wonder Woman', 'Super Hero film', 2,2,3),
		(3, 'Lost In Translation', NULL, 5,5,4),
		(4, 'Bend It Like Beckham', NULL, 4,4,3),
		(5, 'The Hurt Locker', NULL, 3,3,4)

SET IDENTITY_INSERT Dvd OFF
