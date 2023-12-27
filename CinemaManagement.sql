CREATE DATABASE CinemaManagementTest;
GO
USE CinemaManagementTest;

--bảng ACCOUNT
go
create table ACCOUNTS
(
	id INT PRIMARY KEY IDENTITY(1, 1),
    Username VARCHAR(40) not null unique,
    Password VARCHAR(40) not null,
	Staff_Id int not null,
	Constraint FK_StaffId foreign key(Staff_Id) references Staff(Id)
)


go



--bảng staff
go
create table Staff
(
	Id int primary key identity(1, 1),
	FullName nvarchar(100) not null,
	Birth smalldatetime not null,
	Gender nvarchar(20) not null,
	Email varchar(100) not null,
	PhoneNumber varchar(20) not null,
	Salary int not null,
	Role nvarchar(30) not null,
	NgayVaolam smalldatetime not null,
	ImageSource varchar(50) default 'Default.jpg'
)
go

--bảng voucher
CREATE TABLE VOUCHER
(   
    ID INT IDENTITY (1,1) PRIMARY KEY,
    NAME NVARCHAR(50) NOT NULL,
    CODE VARCHAR(10) NOT NULL UNIQUE,
    VOUCHERDETAIL NTEXT NOT NULL,
    TYPE varchar(10) NOT NULL,
    STARTDATE SMALLDATETIME NOT NULL,
    FINDATE SMALLDATETIME NOT NULL
)


--bảng custom
CREATE TABLE CUSTOMER
(
    Id int identity(1,1) primary key ,
    FullName nvarchar(50) not null,
    PhoneNumber varchar(10) not null unique,
    Email varchar(50) not null,
    Point int not null,
    Birth smalldatetime not null,
    RegDate smalldatetime not null,
    Gender nvarchar(20) not null,
)   


--bảng MOVIES
GO
CREATE TABLE MOVIES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(100),
    Description NVARCHAR(500),
    Director NVARCHAR(50),
    ReleaseYear INT,
    Language NVARCHAR(20),
    Country NVARCHAR(20),
    Length INT,
    Trailer NVARCHAR(200),
    StartDate SMALLDATETIME,
    EndDate SMALLDATETIME,
    Genre_id INT,
    CONSTRAINT FK_Genre FOREIGN KEY (Genre_id) REFERENCES GENRES(id)
);
GO
-- Genres
CREATE TABLE GENRES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(50)
);

--bảng GENRES           
INSERT INTO GENRES VALUES (N'Gia Đình'), (N'Hài');
GO
GO
ALTER TABLE MOVIES ADD Poster NVARCHAR(100);
GO
INSERT INTO GENRES VALUES(N'Tài liệu'), (N'Phiêu lưu'), (N'Kinh dị'), (N'Hành động'), (N'Tội phạm'), (N'Giả tưởng'), (N'Khoa học'), (N'Hoạt hình'), (N'Tình cảm'), (N'Phép thuật')



--bảng product
go
create table Product
(
	ID int identity(1,1) primary key,
	Name nvarchar(100) not null,
	ImageSource varchar(200) not null,
	Quantity int not null,
	PurchasePrice int not null,
	Price int null,
	Type int not null,
)
go

--trigger tự set giá sau mỗi lần update,insert: giá bán = giá nhập +giá nhập *20%
go
create trigger trg_setPrice_Product
on Product
for insert,update
as
begin
	declare @ID int
	declare @PurchasePrice int

	select @ID=ID, @PurchasePrice=PurchasePrice
	from inserted

	update Product
	set Price=@PurchasePrice+0.2*@PurchasePrice
	where ID=@ID
end
go
-- bang errors

CREATE TABLE ERRORS(
    id INT IDENTITY(1, 1) PRIMARY KEY,
    NAME NVARCHAR(100) NOT NULL,
    DESCRIPTION NVARCHAR(200) NOT NULL,
    DATEADDED SMALLDATETIME DEFAULT GETDATE(),
    STATUS NVARCHAR(50) DEFAULT N'Chờ tiếp nhận',
    ENDDATE SMALLDATETIME,
    COST MONEY,
    STAFF_id INT CONSTRAINT FK_ERR_STAFF FOREIGN KEY REFERENCES STAFF(Id),
    IMAGE NVARCHAR(100) NOT NULL
)
GO
