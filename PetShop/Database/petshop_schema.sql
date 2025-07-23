
CREATE DATABASE PetShopDB;
GO
USE PetShopDB;
GO
-- Tạo bảng Users
CREATE TABLE Users (
    UserID INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    Password TEXT NOT NULL,
    Role TEXT CHECK(Role IN ('Customer', 'Admin')) NOT NULL DEFAULT 'Customer',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    Price REAL NOT NULL,
    Stock INTEGER NOT NULL,
    ImagePath TEXT
);

-- Tạo bảng Services
CREATE TABLE Services (
    ServiceID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    Price REAL NOT NULL,
    Duration INTEGER NOT NULL 
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    OrderID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserID INTEGER NOT NULL,
    TotalAmount REAL NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tạo bảng OrderDetails
CREATE TABLE OrderDetails (
    OrderDetailID INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderID INTEGER NOT NULL,
    ProductID INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    UnitPrice REAL NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Tạo bảng Bookings
CREATE TABLE Bookings (
    BookingID INTEGER PRIMARY KEY AUTOINCREMENT,
    UserID INTEGER NOT NULL,
    ServiceID INTEGER NOT NULL,
    BookingTime DATETIME NOT NULL,
    Notes TEXT,
    Status TEXT CHECK(Status IN ('Pending', 'Approved', 'Completed', 'Cancelled')) DEFAULT 'Pending',
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ServiceID) REFERENCES Services(ServiceID)
);

INSERT INTO Users (FullName, Email, Password, Role) VALUES
('Admin', 'admin@example.com', 'hashed_admin_pw', 'Admin'),

-- 29 khách hàng
('Nguyen Van A', 'a1@example.com', 'hashed_pw_1', 'Customer'),
('Le Thi B', 'b2@example.com', 'hashed_pw_2', 'Customer'),
('Tran Van C', 'c3@example.com', 'hashed_pw_3', 'Customer'),
('Hoang Thi D', 'd4@example.com', 'hashed_pw_4', 'Customer'),
('Pham Van E', 'e5@example.com', 'hashed_pw_5', 'Customer'),
('Do Thi F', 'f6@example.com', 'hashed_pw_6', 'Customer'),
('Nguyen Van G', 'g7@example.com', 'hashed_pw_7', 'Customer'),
('Tran Thi H', 'h8@example.com', 'hashed_pw_8', 'Customer'),
('Le Van I', 'i9@example.com', 'hashed_pw_9', 'Customer'),
('Phan Thi J', 'j10@example.com', 'hashed_pw_10', 'Customer'),
('Bui Van K', 'k11@example.com', 'hashed_pw_11', 'Customer'),
('Ngo Thi L', 'l12@example.com', 'hashed_pw_12', 'Customer'),
('Dang Van M', 'm13@example.com', 'hashed_pw_13', 'Customer'),
('Nguyen Thi N', 'n14@example.com', 'hashed_pw_14', 'Customer'),
('Vo Van O', 'o15@example.com', 'hashed_pw_15', 'Customer'),
('Pham Thi P', 'p16@example.com', 'hashed_pw_16', 'Customer'),
('Truong Van Q', 'q17@example.com', 'hashed_pw_17', 'Customer'),
('Do Thi R', 'r18@example.com', 'hashed_pw_18', 'Customer'),
('Nguyen Van S', 's19@example.com', 'hashed_pw_19', 'Customer'),
('Le Thi T', 't20@example.com', 'hashed_pw_20', 'Customer'),
('Tran Van U', 'u21@example.com', 'hashed_pw_21', 'Customer'),
('Hoang Thi V', 'v22@example.com', 'hashed_pw_22', 'Customer'),
('Pham Van W', 'w23@example.com', 'hashed_pw_23', 'Customer'),
('Do Thi X', 'x24@example.com', 'hashed_pw_24', 'Customer'),
('Nguyen Van Y', 'y25@example.com', 'hashed_pw_25', 'Customer'),
('Tran Thi Z', 'z26@example.com', 'hashed_pw_26', 'Customer'),
('Le Van AA', 'aa27@example.com', 'hashed_pw_27', 'Customer'),
('Phan Thi BB', 'bb28@example.com', 'hashed_pw_28', 'Customer'),
('Bui Van CC', 'cc29@example.com', 'hashed_pw_29', 'Customer');

INSERT INTO Products (Name, Description, Price, Stock, ImagePath) VALUES
('Thức ăn cho chó Pedigree 3kg', 'Dành cho chó trưởng thành', 250000, 100, 'images/pedigree.jpg'),
('Thức ăn cho mèo Me-O 1.2kg', 'Hương cá ngừ', 120000, 80, 'images/meo.jpg'),
('Thức ăn Royal Canin', 'Dành cho mèo con', 180000, 50, 'images/royalcanin.jpg'),
('Xương gặm vị bò', 'Cho chó lớn nhai khỏe', 40000, 200, 'images/bone.jpg'),
('Bát ăn inox chống trượt', 'Dành cho chó/mèo', 30000, 100, 'images/bowl.jpg'),
('Vòng cổ da', 'Cỡ trung, màu nâu', 60000, 40, 'images/collar.jpg'),
('Dây dắt thú cưng 1.5m', 'Dây dù chịu lực tốt', 75000, 60, 'images/leash.jpg'),
('Lồng vận chuyển thú nhỏ', 'Kích thước 40x30cm', 320000, 30, 'images/carrier.jpg'),
('Sữa tắm khử mùi', 'Tinh chất tự nhiên', 90000, 70, 'images/shampoo.jpg'),
('Cào móng mèo', 'Cột dọc đứng, dây thừng', 150000, 35, 'images/scratch.jpg'),
('Đồ chơi bóng phát sáng', 'Đèn LED bên trong', 45000, 90, 'images/lightball.jpg'),
('Găng tay chải lông', 'Tiện lợi khi tắm', 25000, 60, 'images/glove.jpg'),
('Xịt chống ve rận', 'Cho thú cưng', 70000, 40, 'images/spray.jpg'),
('Túi đựng phân chó', 'Gói 100 túi', 20000, 120, 'images/poopbag.jpg'),
('Bình nước mini', 'Mang đi chơi', 65000, 50, 'images/waterbottle.jpg'),
('Thức ăn hạt vị cá hồi', 'Cho mèo trưởng thành', 135000, 90, 'images/salmon.jpg'),
('Thức ăn cho hamster', 'Hỗn hợp ngũ cốc', 30000, 80, 'images/hamsterfood.jpg'),
('Chuồng chó inox', '60x90cm, 2 cửa', 600000, 20, 'images/kennel.jpg'),
('Đồ chơi lông vũ', 'Cho mèo đuổi bắt', 20000, 100, 'images/feather.jpg'),
('Chăn ấm thú cưng', 'Chất liệu lông mịn', 75000, 60, 'images/blanket.jpg'),
('Khay vệ sinh cho mèo', 'Có nắp đậy', 150000, 30, 'images/litterbox.jpg'),
('Đèn sưởi thú cưng', 'Giữ ấm mùa đông', 180000, 15, 'images/heater.jpg'),
('Bàn cào mèo mini', 'Kích thước 30cm', 85000, 25, 'images/scratchpad.jpg'),
('Máy cho ăn tự động', 'Hẹn giờ thông minh', 900000, 10, 'images/feeder.jpg'),
('Camera thú cưng', 'Theo dõi từ xa', 1100000, 5, 'images/camera.jpg'),
('Đồ chơi hình xương phát âm', 'Phát ra tiếng kêu', 40000, 80, 'images/toybone.jpg'),
('Balo đựng thú cưng', 'Cho mèo/chó nhỏ', 290000, 25, 'images/petbag.jpg'),
('Xương tẩy răng', 'Chó thích gặm', 35000, 120, 'images/chewbone.jpg'),
('Thức ăn cho vẹt', 'Ngũ cốc trộn hạt', 40000, 50, 'images/birdfood.jpg'),
('Thảm lót chuồng thú', 'Chống thấm', 100000, 70, 'images/pad.jpg');

INSERT INTO Services (Name, Description, Price, Duration) VALUES
('Tắm thú cưng cơ bản', 'Tắm nước ấm + sấy', 150000, 45),
('Tắm thú cưng VIP', 'Tắm nước ấm, xịt khử mùi, mát xa', 250000, 60),
('Cắt tỉa lông toàn thân', 'Gọn gàng, đẹp chuẩn spa', 200000, 60),
('Cắt móng chân', 'An toàn, không chảy máu', 50000, 15),
('Chải lông tạo kiểu', 'Phong cách Hàn Quốc', 120000, 45),
('Nhuộm lông nghệ thuật', 'Dùng màu thiên nhiên', 300000, 75),
('Xịt thơm toàn thân', 'Hương hoa nhẹ dịu', 40000, 5),
('Chăm sóc răng miệng', 'Làm sạch mảng bám', 180000, 30),
('Khám sức khỏe định kỳ', 'Tổng quát toàn thân', 250000, 40),
('Khám da liễu', 'Phát hiện nấm, ghẻ', 220000, 30),
('Tiêm phòng 5 bệnh', 'Chó/mèo', 450000, 30),
('Cạo lông vệ sinh', 'Vùng bụng, hậu môn', 70000, 15),
('Massage thư giãn', 'Giảm stress cho thú', 160000, 30),
('Gửi thú cưng qua đêm', 'Có camera theo dõi', 300000, 1440),
('Cạo vôi răng', 'Vệ sinh chuyên sâu', 210000, 35),
('Khám tai mũi họng', 'Bác sĩ thú y', 200000, 25),
('Khám mắt', 'Nhỏ mắt, kiểm tra giác mạc', 170000, 20),
('Gỡ rối lông', 'Dành cho chó lông dài', 95000, 30),
('Tắm khô cấp tốc', 'Không cần sấy', 100000, 20),
('Khám hậu môn tuyến', 'Cho chó mèo lớn', 130000, 25),
('Làm đẹp móng', 'Sơn móng an toàn', 140000, 40),
('Tư vấn dinh dưỡng', 'Miễn phí cho khách thân thiết', 0, 20),
('Tư vấn giống & nhân giống', 'Hướng dẫn chi tiết', 80000, 30),
('Vệ sinh tai', 'Nhẹ nhàng, không đau', 60000, 10),
('Tắm cho hamster', 'Không dùng nước', 40000, 10),
('Khám thú cảnh nhỏ', 'Thỏ, sóc, chim', 90000, 20),
('Điều trị ve rận', 'Phun thuốc + tắm', 190000, 45),
('Trị nấm da', 'Bôi thuốc + vệ sinh', 170000, 40),
('Điều trị tiêu chảy', 'Tư vấn dinh dưỡng', 150000, 30),
('Khám nội soi', 'Có video nội soi', 550000, 60);
