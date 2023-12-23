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


--tạo bảng product
go
create table Product
(
	ID int identity(1,1) primary key,
	Name nvarchar(100) not null,
	ImageSource varchar(200) not null,
	Quantity int not null,
	Price int not null,
	Type int not null,
)
go