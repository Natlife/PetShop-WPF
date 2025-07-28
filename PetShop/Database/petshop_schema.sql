-- DROP DATABASE IF EXISTS PetShopDB;
USE master;
GO

ALTER DATABASE PetShopDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

DROP DATABASE IF EXISTS PetShopDB;
GO

CREATE DATABASE PetShopDB;
GO

USE PetShopDB;
GO

-- Bảng Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) CHECK(Role IN ('Customer', 'Admin')) NOT NULL DEFAULT 'Customer',
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price REAL NOT NULL,
    Stock INT NOT NULL,
    ImagePath NVARCHAR(255)
);

-- Bảng Services
CREATE TABLE Services (
    ServiceID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price REAL NOT NULL,
    Duration INT NOT NULL
);

-- Bảng Orders
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    TotalAmount REAL NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY IDENTITY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice REAL NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

-- Bảng Pets
CREATE TABLE Pets (
    PetID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Species NVARCHAR(50) NOT NULL,
    Breed NVARCHAR(100),
    Gender NVARCHAR(10) CHECK (Gender IN ('Male', 'Female')),
    BirthDate DATE,
    Color NVARCHAR(50),
    Weight FLOAT,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Bảng Bookings
CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY,
    UserID INT NOT NULL,
    ServiceID INT NOT NULL,
    PetID INT NOT NULL,
    BookingTime DATETIME NOT NULL,
    Notes NVARCHAR(MAX),
    Status NVARCHAR(50) CHECK(Status IN ('Pending', 'Approved', 'Completed', 'Cancelled')) DEFAULT 'Pending',
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID) ON DELETE CASCADE,
    FOREIGN KEY (PetID) REFERENCES Pets(PetID)
);

-- Bảng Invoices
CREATE TABLE Invoices (
    InvoiceId INT PRIMARY KEY IDENTITY,
    UserId INT NOT NULL,
    OrderId INT NOT NULL,
    InvoiceDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    Notes NVARCHAR(MAX),
    FOREIGN KEY (UserId) REFERENCES Users(UserID),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderID)
);


INSERT INTO Users (FullName, Email, Password, Role) VALUES
(N'Admin', N'admin@example.com', N'hashed_admin_pw', N'Admin'),


(N'Nguyen Van A', N'a1@example.com', N'hashed_pw_1', N'Customer'),
(N'Le Thi B', N'b2@example.com', N'hashed_pw_2', N'Customer'),
(N'Tran Van C', N'c3@example.com', N'hashed_pw_3', N'Customer'),
(N'Hoang Thi D', N'd4@example.com', N'hashed_pw_4', N'Customer'),
(N'Pham Van E', N'e5@example.com', N'hashed_pw_5', N'Customer'),
(N'Do Thi F', N'f6@example.com', N'hashed_pw_6', N'Customer'),
(N'Nguyen Van G', N'g7@example.com', N'hashed_pw_7', N'Customer'),
(N'Tran Thi H', N'h8@example.com', N'hashed_pw_8', N'Customer'),
(N'Le Van I', N'i9@example.com', N'hashed_pw_9', N'Customer'),
(N'Phan Thi J', N'j10@example.com', N'hashed_pw_10', N'Customer'),
(N'Bui Van K', N'k11@example.com', N'hashed_pw_11', N'Customer'),
(N'Ngo Thi L', N'l12@example.com', N'hashed_pw_12', N'Customer'),
(N'Dang Van M', N'm13@example.com', N'hashed_pw_13', N'Customer'),
(N'Nguyen Thi N', N'n14@example.com', N'hashed_pw_14', N'Customer'),
(N'Vo Van O', N'o15@example.com', N'hashed_pw_15', N'Customer'),
(N'Pham Thi P', N'p16@example.com', N'hashed_pw_16', N'Customer'),
(N'Truong Van Q', N'q17@example.com', N'hashed_pw_17', N'Customer'),
(N'Do Thi R', N'r18@example.com', N'hashed_pw_18', N'Customer'),
(N'Nguyen Van S', N's19@example.com', N'hashed_pw_19', N'Customer'),
(N'Le Thi T', N't20@example.com', N'hashed_pw_20', N'Customer'),
(N'Tran Van U', N'u21@example.com', N'hashed_pw_21', N'Customer'),
(N'Hoang Thi V', N'v22@example.com', N'hashed_pw_22', N'Customer'),
(N'Pham Van W', N'w23@example.com', N'hashed_pw_23', N'Customer'),
(N'Do Thi X', N'x24@example.com', N'hashed_pw_24', N'Customer'),
(N'Nguyen Van Y', N'y25@example.com', N'hashed_pw_25', N'Customer'),
(N'Tran Thi Z', N'z26@example.com', N'hashed_pw_26', N'Customer'),
(N'Le Van AA', N'aa27@example.com', N'hashed_pw_27', N'Customer'),
(N'Phan Thi BB', N'bb28@example.com', N'hashed_pw_28', N'Customer'),
(N'Bui Van CC', N'cc29@example.com', N'hashed_pw_29', N'Customer');

INSERT INTO Products (Name, Description, Price, Stock, ImagePath) VALUES
(N'Thức ăn cho chó Pedigree 3kg', N'Dành cho chó trưởng thành', 250000, 100, N'images/pedigree.jpg'),
(N'Thức ăn cho mèo Me-O 1.2kg', N'Hương cá ngừ', 120000, 80, N'images/meo.jpg'),
(N'Thức ăn Royal Canin', N'Dành cho mèo con', 180000, 50, N'images/royalcanin.jpg'),
(N'Xương gặm vị bò', N'Cho chó lớn nhai khỏe', 40000, 200, N'images/bone.jpg'),
(N'Bát ăn inox chống trượt', N'Dành cho chó/mèo', 30000, 100, N'images/bowl.jpg'),
(N'Vòng cổ da', N'Cỡ trung, màu nâu', 60000, 40, N'images/collar.jpg'),
(N'Dây dắt thú cưng 1.5m', N'Dây dù chịu lực tốt', 75000, 60, N'images/leash.jpg'),
(N'Lồng vận chuyển thú nhỏ', N'Kích thước 40x30cm', 320000, 30, N'images/carrier.jpg'),
(N'Sữa tắm khử mùi', N'Tinh chất tự nhiên', 90000, 70, N'images/shampoo.jpg'),
(N'Cào móng mèo', N'Cột dọc đứng, dây thừng', 150000, 35, N'images/scratch.jpg'),
(N'Đồ chơi bóng phát sáng', N'Đèn LED bên trong', 45000, 90, N'images/lightball.jpg'),
(N'Găng tay chải lông', N'Tiện lợi khi tắm', 25000, 60, N'images/glove.jpg'),
(N'Xịt chống ve rận', N'Cho thú cưng', 70000, 40, N'images/spray.jpg'),
(N'Túi đựng phân chó', N'Gói 100 túi', 20000, 120, N'images/poopbag.jpg'),
(N'Bình nước mini', N'Mang đi chơi', 65000, 50, N'images/waterbottle.jpg'),
(N'Thức ăn hạt vị cá hồi', N'Cho mèo trưởng thành', 135000, 90, N'images/salmon.jpg'),
(N'Thức ăn cho hamster', N'Hỗn hợp ngũ cốc', 30000, 80, N'images/hamsterfood.jpg'),
(N'Chuồng chó inox', N'60x90cm, 2 cửa', 600000, 20, N'images/kennel.jpg'),
(N'Đồ chơi lông vũ', N'Cho mèo đuổi bắt', 20000, 100, N'images/feather.jpg'),
(N'Chăn ấm thú cưng', N'Chất liệu lông mịn', 75000, 60, N'images/blanket.jpg'),
(N'Khay vệ sinh cho mèo', N'Có nắp đậy', 150000, 30, N'images/litterbox.jpg'),
(N'Đèn sưởi thú cưng', N'Giữ ấm mùa đông', 180000, 15, N'images/heater.jpg'),
(N'Bàn cào mèo mini', N'Kích thước 30cm', 85000, 25, N'images/scratchpad.jpg'),
(N'Máy cho ăn tự động', N'Hẹn giờ thông minh', 900000, 10, N'images/feeder.jpg'),
(N'Camera thú cưng', N'Theo dõi từ xa', 1100000, 5, N'images/camera.jpg'),
(N'Đồ chơi hình xương phát âm', N'Phát ra tiếng kêu', 40000, 80, N'images/toybone.jpg'),
(N'Balo đựng thú cưng', N'Cho mèo/chó nhỏ', 290000, 25, N'images/petbag.jpg'),
(N'Xương tẩy răng', N'Chó thích gặm', 35000, 120, N'images/chewbone.jpg'),
(N'Thức ăn cho vẹt', N'Ngũ cốc trộn hạt', 40000, 50, N'images/birdfood.jpg'),
(N'Thảm lót chuồng thú', N'Chống thấm', 100000, 70, N'images/pad.jpg');

INSERT INTO Services (Name, Description, Price, Duration) VALUES
(N'Tắm thú cưng cơ bản', N'Tắm nước ấm + sấy', 150000, 45),
(N'Tắm thú cưng VIP', N'Tắm nước ấm, xịt khử mùi, mát xa', 250000, 60),
(N'Cắt tỉa lông toàn thân', N'Gọn gàng, đẹp chuẩn spa', 200000, 60),
(N'Cắt móng chân', N'An toàn, không chảy máu', 50000, 15),
(N'Chải lông tạo kiểu', N'Phong cách Hàn Quốc', 120000, 45),
(N'Nhuộm lông nghệ thuật', N'Dùng màu thiên nhiên', 300000, 75),
(N'Xịt thơm toàn thân', N'Hương hoa nhẹ dịu', 40000, 5),
(N'Chăm sóc răng miệng', N'Làm sạch mảng bám', 180000, 30),
(N'Khám sức khỏe định kỳ', N'Tổng quát toàn thân', 250000, 40),
(N'Khám da liễu', N'Phát hiện nấm, ghẻ', 220000, 30),
(N'Tiêm phòng 5 bệnh', N'Chó/mèo', 450000, 30),
(N'Cạo lông vệ sinh', N'Vùng bụng, hậu môn', 70000, 15),
(N'Massage thư giãn', N'Giảm stress cho thú', 160000, 30),
(N'Gửi thú cưng qua đêm', N'Có camera theo dõi', 300000, 1440),
(N'Cạo vôi răng', N'Ve sinh chuyên sâu', 210000, 35),
(N'Khám tai mũi họng', N'Bác sĩ thú y', 200000, 25),
(N'Khám mắt', N'Nhỏ mắt, kiểm tra giác mạc', 170000, 20),
(N'Gỡ rối lông', N'Dành cho chó lông dài', 95000, 30),
(N'Tắm khô cấp tốc', N'Không cần sấy', 100000, 20),
(N'Khám hậu môn tuyến', N'Cho chó mèo lớn', 130000, 25),
(N'Làm đẹp móng', N'Sơn móng an toàn', 140000, 40),
(N'Tư vấn dinh dưỡng', N'Miễn phí cho khách thân thiết', 0, 20),
(N'Tư vấn giống & nhân giống', N'Hướng dẫn chi tiết', 80000, 30),
(N'Vệ sinh tai', N'Nhẹ nhàng, không đau', 60000, 10),
(N'Tắm cho hamster', N'Không dùng nước', 40000, 10),
(N'Khám thú cảnh nhỏ', N'Thỏ, sóc, chim', 90000, 20),
(N'Điều trị ve rận', N'Phun thuốc + tắm', 190000, 45),
(N'Trị nấm da', N'Bôi thuốc + vệ sinh', 170000, 40),
(N'Điều trị tiêu chảy', N'Tư vấn dinh dưỡng', 150000, 30),
(N'Khám nội soi', N'Có video nội soi', 550000, 60);