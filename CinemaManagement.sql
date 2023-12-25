CREATE DATABASE CinemaManagement;
GO
USE CinemaManagement;

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
    ReleaseDate DATE,
    Language NVARCHAR(20),
    Country NVARCHAR(20),
    Length INT,
    Trailer NVARCHAR(200),
    StartDate SMALLDATETIME,
    EndDate SMALLDATETIME,
);

--bảng GENRES
GO
CREATE TABLE GENRES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(50)
);
GO

--bảng MOVIES_GENRES
GO
CREATE TABLE MOVIES_GENRES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Movie_id INT REFERENCES MOVIES(id),
    Genre_id INT REFERENCES GENRES(id)
)
GO
INSERT INTO MOVIES VALUES(N'Bố Già', 
                        N'Phim sẽ xoay quanh lối sống thường nhật của một xóm lao động nghèo, ở đó có bộ tứ anh em Giàu - Sang - Phú - Quý với Ba Sang sẽ là nhân vật chính, hay lo chuyện bao đồng nhưng vô cùng thương con cái. Câu chuyện phim tập trung về hai cha con Ba Sang (Trấn Thành) và Quắn (Tuấn Trần). Dù yêu thương nhau nhưng khoảng cách thế hệ đã đem đến những bất đồng lớn giữa hai cha con. Liệu cả hai có thể cho nhau cơ hội thấu hiểu đối phương, thu hẹp khoảng cách và tạo nên hạnh phúc từ sự khác biệt?',
                        N'Trấn Thành',
                        '2021/03/12',
                        'VN',
                        'VN',
                        128,
                        'https://www.youtube.com/watch?v=uVa1lTvmVhs',
                        '2023/01/01',
                        '2023/12/30');         
GO              
INSERT INTO GENRES VALUES (N'Gia Đình'), (N'Hài');
GO


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