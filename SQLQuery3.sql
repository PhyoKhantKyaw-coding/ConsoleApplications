USE [PhyoKhantKyaw_ATM]
GO

CREATE TABLE TBL_User (
    UserID UNIQUEIDENTIFIER PRIMARY KEY,
    UserName NVARCHAR(100) NOT NULL,
    UserPassword NVARCHAR(100) NOT NULL,
    Wallet DECIMAL(18,2) NOT NULL
);
INSERT INTO TBL_User (UserID, UserName, UserPassword, Wallet) VALUES
(NEWID(), 'Alice', 'pass123', 1000.00),
(NEWID(), 'Bob', 'secure456', 1500.00),
(NEWID(), 'Charlie', 'mypassword', 2000.00),
(NEWID(), 'David', 'qwerty123', 1200.00),
(NEWID(), 'Eva', 'admin321', 800.00);


