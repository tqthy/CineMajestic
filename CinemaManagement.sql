CREATE DATABASE CinemaManagement;
GO
USE CinemaManagement;
GO
CREATE TABLE ACCOUNTS(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Username VARCHAR(40),
    Password VARCHAR(40),
    Account_Type INT
);
GO
CREATE TABLE EMPLOYEES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    FullName NVARCHAR(50),
    DateOfBirth DATE,
    Gender NVARCHAR(20),
    PhoneNumber VARCHAR(20),
    Email VARCHAR(50),
    Salary MONEY,
    Account_id INT,
    CONSTRAINT FK_Account FOREIGN KEY (Account_id) REFERENCES ACCOUNTS(id)
);