CREATE DATABASE CinemaManagement;
GO
USE CinemaManagement;



GO
CREATE TABLE MOVIE
(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(100) not null,
    Description NVARCHAR(500) not null,
    Genre nvarchar(100) not null,
    Director NVARCHAR(50) not null,
    ReleaseYear smalldatetime not null,
    Language NVARCHAR(20) not null,
    Country NVARCHAR(20) not null,
    Length INT not null,
    Trailer NVARCHAR(200) not null,
    StartDate SMALLDATETIME not null,
    Status nvarchar(50) not null,
    ImageSource varchar(100) not null,
);
GO