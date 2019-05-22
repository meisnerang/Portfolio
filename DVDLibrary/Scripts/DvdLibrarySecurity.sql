USE master
GO

CREATE LOGIN DvdLibraryApp WITH PASSWORD='testing123'
GO

USE DvdLibrary
GO

CREATE USER DvdLibraryApp FOR LOGIN DvdLibraryApp
GO

GRANT SELECT ON Director TO DvdLibraryApp
GRANT INSERT ON Director TO DvdLibraryApp
GRANT UPDATE ON Director TO DvdLibraryApp
GRANT DELETE ON Director TO DvdLibraryApp
GRANT SELECT ON Dvd TO DvdLibraryApp
GRANT INSERT ON Dvd TO DvdLibraryApp
GRANT UPDATE ON Dvd TO DvdLibraryApp
GRANT DELETE ON Dvd TO DvdLibraryApp
GRANT SELECT ON Rating TO DvdLibraryApp
GRANT INSERT ON Rating TO DvdLibraryApp
GRANT UPDATE ON Rating TO DvdLibraryApp
GRANT DELETE ON Rating TO DvdLibraryApp
GRANT SELECT ON ReleaseYear TO DvdLibraryApp
GRANT INSERT ON ReleaseYear TO DvdLibraryApp
GRANT UPDATE ON ReleaseYear TO DvdLibraryApp
GRANT DELETE ON ReleaseYear TO DvdLibraryApp

GRANT EXECUTE ON DvdCreate TO DvdLibraryApp
GRANT EXECUTE ON DvdDelete TO DvdLibraryApp
GRANT EXECUTE ON DvdEdit TO DvdLibraryApp
GRANT EXECUTE ON DvdGetByDirector TO DvdLibraryApp
GRANT EXECUTE ON DvdGetById TO DvdLibraryApp
GRANT EXECUTE ON DvdGetByRating TO DvdLibraryApp
GRANT EXECUTE ON DvdGetByReleaseYear TO DvdLibraryApp
GRANT EXECUTE ON DvdGetByTitle TO DvdLibraryApp
GRANT EXECUTE ON DvdListAll TO DvdLibraryApp
GO

