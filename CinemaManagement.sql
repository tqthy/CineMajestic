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
    Role NVARCHAR(20),
    Account_id INT,
    CONSTRAINT FK_Account FOREIGN KEY (Account_id) REFERENCES ACCOUNTS(id)
);
GO
INSERT INTO ACCOUNTS VALUES('admin', 'Admin@123', 1);
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
GO
CREATE TABLE GENRES(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(50)
);
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


--xóa bảng employees
drop table EMPLOYEES

--xóa bảng accounts
drop table ACCOUNTS

--thêm bảng staff
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

--thêm bảng ACCOUNTS
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