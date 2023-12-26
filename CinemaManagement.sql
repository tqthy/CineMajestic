--tạo database
go
create database CineMajestic
go

--sử dụng database
go
use CineMajestic
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
--tân từ cho bảng Staff: mỗi nhân viên có id riêng làm khóa chính để phân biệt, có họ tên,ngày sinh,giới tính,email,sđt,mức lương,chức vụ,ngày vào làm,ảnh thẻ(sẽ dùng ảnh mặc định khi nhân viên vừa vào làm)


-- bảng ACCOUNTS
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
--tân từ: mỗi tài khoản có 1 id riêng làm khóa chính,có tên tk riêng,mk,có khóa ngoại Staff_Id tham chiếu tới id của bảng nhân viên để phân biệt tài khoản này là của ai



--bảng voucher
go
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
go
--tân từ: mỗi voucher có 1 id riêng làm khóa chính để phần biệt, có tên,code,chi tiết voucher,loại (vip 1 2 3),ngày áp dụng và ngày kết thúc voucher



--tạo bảng customer
go
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
go
--tân từ: mỗi khách hàng có id riêng làm khóa chính để phân biệt, có tên,sđt,email,point,ngày sinh,ngày đăng ký,và giới tính



--bảng MOVIES
GO
CREATE TABLE MOVIE
(
    id INT PRIMARY KEY IDENTITY(1, 1),
    Title NVARCHAR(100) not null,
    Description NVARCHAR(500) not null,
    Genre nvarchar(100) not null,
    Director NVARCHAR(50) not null,
    ReleaseYear int not null,
    Language NVARCHAR(20) not null,
    Country NVARCHAR(20) not null,
    Length INT not null,
    Trailer NVARCHAR(200) not null,
    StartDate SMALLDATETIME not null,
    Status nvarchar(50) not null,
    ImageSource varchar(100) not null,
);
GO
--tân từ: mỗi bộ phim có 1 id riêng để phân biệt với các bộ phim khác,có tiêu đề,miêu tả về phim,thể loại phim,đạo diễn 
--năm ra mắt,ngôn ngữ trong phim,quốc gia sx,thời lượng phim(phút),link trailer,ngày ra mắt phim,trạng thái phim trong rạp(đang phát hành,ngừng ph),đường dẫn ảnh phim



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
--tân từ: mỗi product sẽ có id riêng làm khóa chính để phân biệt với các product khác,có tên,đường dẫn ảnh,số lượng hiện có,giá nhập vào,giá bán(sẽ được tự động set=1.2 giá nhập),loại product(thức ăn ,đồ uống)

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





--bảng phòng
create table Auditorium
(
	Id int identity(1,1) primary key,
	Name nvarchar(50) not null,
	NumberOfSeats int not null,
)
--tân từ: mỗi phòng sẽ có 1 id riêng làm khóa chính để phân biệt với các phòng khác,có tên,số chỗ ngồi trong phòng


--bảng chỗ ngồi
create table Seat
(
	Id int identity(1,1) primary key,
	Location varchar(3),
	Condition bit ,
	Auditorium_Id int not null,
	Constraint FK_Auditorium_Id_BySeat foreign key(Auditorium_Id) references Auditorium(Id)
)
--mỗi chỗ ngồi sẽ có 1 id riêng để phân biệt với các chỗ ngồi khác,có vị trí(A01,A02,B03,...),tình trạng(có người ngồi hay chưa)
--cho biết chỗ ngồi thuộc phòng nào


--bảng suất chiếu
create table ShowTime
(
	Id int identity(1,1) primary key,
	StartTime smalldatetime not null,--cái này bao gồm ngày chiếu,giờ chiếu luôn
	EndTime time,--cái này sẽ tự động tính từ giờ chiếu và lenght của movie(xài trong constructor c#)
	PerSeatTicketPrice int not null,
	Movie_Id int,--khóa ngoại tham chiếu tới id của bảng movie(lưu ý chỉ lấy movie đang phát hành)
	Auditorium_Id int not null,--khóa ngoại tham chiếu tới id của bảng phòng chiếu
    Constraint FK_Auditorium_Id_ByShowTime foreign key(Auditorium_Id) references Auditorium(Id),
	Constraint FK_Movie_Id_ByShowTime foreign key(Movie_Id) references Movie(Id),
)
--mỗi suất chiếu có 1 id để phân biệt với suất chiếu khác, có thời gian bắt đầu(ngày tháng năm,giờ phút giây),
--thời gian kết thúc(giờ phút giây được tính tự động), có giá để mua 1 chỗ ngồi 
--cho biết phim chiếu là gì
--cho biết phòng nào đang chiếu



--bảng hóa đơn
create table Bill
(
	Id int identity(1,1) primary key,
	Staff_Id int,
	Customer_Id int,
	ShowTime_Id int,
	BillDate smalldatetime not null,
	QuantityTicket int not null,
	Discount int,
	Note nvarchar(300),
	Total int not null,
    Constraint FK_StaffId_ByBill foreign key(Staff_Id) references Staff(Id),
    Constraint FK_ShowTime_IdByBill foreign key(ShowTime_Id) references ShowTime(Id),
	Constraint FK_Customer_IdByBill foreign key(Customer_Id) references Customer(Id),
)
--tân từ: mỗi hóa đơn có 1 id riêng làm khóa chính để phân biệt với các hóa đơn khác
--cho biết nhân viên nào tạo hóa đơn này
--cho biết khách hàng nào thanh toán hđ
--cho biết hóa đơn được mua để xem suất chiếu nào
--cho biết ngày tạo hóa đơn
--số lượng suất chiếu được mua(số vé)
--cho biết giảm giá bn 4
--ghi chú
--giá trị hóa đơn




--bảng vé
create table Ticket
(
	Id int identity(1,1) primary key,
	Seat_Id int not null,
	Bill_Id int not null,
	Constraint FK_Seat_Id_ByTicket foreign key(Seat_Id) references Seat(Id),
	Constraint FK_Bill_Id_ByTicket foreign key(Bill_Id) references Bill(Id),
)
--mỗi vé có id riêng làm khóa chính để phân biệt với vé khác
--cho biết vé thuộc hóa đơn nào(từ đó biết được ai mua,suất chiếu gì,phòng nào)
--cho biết chỗ ngồi của vé




--bảng chi tiết hóa đơn
create table BillDetail
(
	Id int identity(1,1) primary key,
    Bill_Id int not null,
	Product_Id int,
	Quantity int not null,
	UnitPrice int not null,
	Total as (Quantity * UnitPrice),
	Constraint FK_BillId_ByBillDeTail foreign key(Bill_Id) references Bill(Id),
    Constraint FK_ProductId_ByBillDeTail foreign key(Product_Id) references Product(ID)
)
--mỗi cthđ có 1 id riêng làm khóa chính để phân biệt với cthđ khác
--cho biết cthđ thuộc hóa đơn nào
--cho biết mua sản phẩm nào,số lượng bao nhiêu,đơn giá,tổng tiên
