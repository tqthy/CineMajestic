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

--insert dữ liệu cho bảng Product

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Coca','1.jpg',19,10000,2)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Pepsi','2.jpg',27,10000,2)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Gà khô','3.jpg',25,50000,1)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Bò khô','4.jpg',11,200000,1)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Sting','5.jpg',19,10000,2)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Hướng dương','6.jpg',8,30000,1)

insert into Product(Name,ImageSource,Quantity,Price,Type)
values(N'Bắp rang bơ','7.jpg',59,15000,1)

