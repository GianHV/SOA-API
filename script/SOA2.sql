USE [master]
GO
/****** Object:  Database [SOA2]    Script Date: 12/6/2024 10:54:28 AM ******/
CREATE DATABASE [SOA2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SOA2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SOA2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SOA2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SOA2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SOA2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SOA2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SOA2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SOA2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SOA2] SET ARITHABORT OFF 
GO
ALTER DATABASE [SOA2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SOA2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SOA2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SOA2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SOA2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SOA2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SOA2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SOA2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SOA2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SOA2] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SOA2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SOA2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SOA2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SOA2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SOA2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SOA2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SOA2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SOA2] SET RECOVERY FULL 
GO
ALTER DATABASE [SOA2] SET  MULTI_USER 
GO
ALTER DATABASE [SOA2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SOA2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SOA2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SOA2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SOA2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SOA2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SOA2', N'ON'
GO
ALTER DATABASE [SOA2] SET QUERY_STORE = ON
GO
ALTER DATABASE [SOA2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SOA2]
GO
/****** Object:  UserDefinedTableType [dbo].[OrderItemType]    Script Date: 12/6/2024 10:54:28 AM ******/
CREATE TYPE [dbo].[OrderItemType] AS TABLE(
	[ProductId] [int] NULL,
	[ProductName] [varchar](255) NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 2) NULL
)
GO
/****** Object:  Table [dbo].[order_items]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[productId] [int] NULL,
	[productName] [varchar](255) NULL,
	[quantity] [int] NULL,
	[unitPrice] [decimal](10, 2) NULL,
	[totalPrice] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customerName] [varchar](255) NOT NULL,
	[customerEmail] [varchar](255) NOT NULL,
	[totalAmount] [decimal](10, 2) NULL,
	[status] [varchar](50) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[order_items] ON 

INSERT [dbo].[order_items] ([id], [orderId], [productId], [productName], [quantity], [unitPrice], [totalPrice]) VALUES (1, 1, 3, N'ao 5 lo', 3, CAST(10.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [productName], [quantity], [unitPrice], [totalPrice]) VALUES (2, 1, 3, N'ao 5 lo', 3, CAST(10.00 AS Decimal(10, 2)), CAST(30.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [productName], [quantity], [unitPrice], [totalPrice]) VALUES (5, 3, 1, N'ao thun', 2, CAST(12.50 AS Decimal(10, 2)), CAST(25.00 AS Decimal(10, 2)))
INSERT [dbo].[order_items] ([id], [orderId], [productId], [productName], [quantity], [unitPrice], [totalPrice]) VALUES (6, 3, 3, N'ao hai lo', 2, CAST(5.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[order_items] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [customerName], [customerEmail], [totalAmount], [status], [createdAt], [updatedAt]) VALUES (1, N'congvien', N'congvien@gmail.com', CAST(47.50 AS Decimal(10, 2)), N'2', CAST(N'2024-12-05T20:35:18.597' AS DateTime), NULL)
INSERT [dbo].[orders] ([id], [customerName], [customerEmail], [totalAmount], [status], [createdAt], [updatedAt]) VALUES (3, N'string', N'string', CAST(35.00 AS Decimal(10, 2)), N'0', CAST(N'2024-12-06T10:49:19.723' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[order_items]  WITH CHECK ADD FOREIGN KEY([orderId])
REFERENCES [dbo].[orders] ([id])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[sp_add_order]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_order]
    @customerName VARCHAR(255),
    @customerEmail VARCHAR(255),
	@status VARCHAR(50),
    @Items OrderItemType READONLY, -- Table-valued parameter
	@id int output
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;


        INSERT INTO Orders (customerName, customerEmail, totalAmount, status)
        VALUES (@customerName, @customerEmail, 0, @status);

        -- Lấy ID của order vừa được thêm
        DECLARE @OrderId INT = SCOPE_IDENTITY();

        INSERT INTO order_items (orderId, productId, productName, quantity, unitPrice, totalPrice)
        SELECT @OrderId, ProductId, ProductName, Quantity, Price, Quantity * Price
        FROM @Items;

		DECLARE @TotalAmount DECIMAL(10,2)
		SELECT @TotalAmount = SUM(unitPrice*quantity)
								FROM order_items
								WHERE orderId = @OrderId

		UPDATE orders
		SET totalAmount = @TotalAmount
		WHERE id = @OrderId

		SET @id = @OrderId

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_add_order_item]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_add_order_item]
@orderId int,
@productId int,
@productName VARCHAR(255),
@quantity INT,
@unitPrice DECIMAL(10,2),
@id int output
AS
BEGIN
	BEGIN TRANSACTION;

    BEGIN TRY
	 IF EXISTS (
            SELECT 1 
            FROM order_items
            WHERE orderId = @orderId
			AND productId = @productId
			AND unitPrice = @unitPrice
        )
	 BEGIN
            UPDATE order_items
            SET quantity = quantity + @quantity,
                totalPrice = (quantity + @quantity) * @unitPrice
            WHERE orderId = @orderId AND productId = @productId;

            SELECT @id = id
            FROM order_items
            WHERE orderId = @orderId AND productId = @productId;
        END
        ELSE
        BEGIN
			INSERT INTO order_items(orderId, productId, productName, quantity, unitPrice, totalPrice)
			VALUES(@orderId, @productId, @productName, @quantity, @unitPrice, @unitPrice * @quantity)

			SET @id = SCOPE_IDENTITY();
		 END;

        COMMIT TRANSACTION;
	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_order]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_order]
@id INT
AS
BEGIN
	DELETE FROM orders
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_order_item]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_order_item]
@id INT
AS
BEGIN
	DELETE FROM order_items
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_order_by_id]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_order_by_id]
@id INT
AS
BEGIN
	SELECT * FROM orders
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_order_item_by_id]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_order_item_by_id]
@id INT
AS
BEGIN
	SELECT * FROM order_items
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_order_items]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_order_items]
AS
BEGIN
	SELECT * FROM order_items
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_orders]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_get_orders]
AS
BEGIN
	SELECT * FROM orders
END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_order_item]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_update_order_item]
	@id INT,
	@productId INT,
	@productName VARCHAR(255),
	@quantity INT,
	@unitPrice DECIMAL(10,2)
AS
BEGIN
	UPDATE order_items
	SET productId = CASE WHEN @productId IS NULL THEN productId 
						ELSE @productId END,
	productName = CASE WHEN @productName IS NULL THEN productName 
						ELSE @productName END,
	quantity = CASE WHEN @quantity IS NULL THEN quantity 
						ELSE @quantity END,
	unitPrice = CASE WHEN @unitPrice IS NULL THEN unitPrice 
						ELSE @unitPrice END,
	totalPrice = CASE WHEN @quantity IS NULL THEN quantity 
						ELSE @quantity END * CASE WHEN @unitPrice IS NULL THEN unitPrice 
						ELSE @unitPrice END
END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_status_order]    Script Date: 12/6/2024 10:54:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_update_status_order]
@id INT,
@status VARCHAR(50)
AS
BEGIN
	UPDATE orders
	SET status = CASE WHEN @status IS NULL THEN status
					ELSE @status END
	WHERE id = @id
END
GO
USE [master]
GO
ALTER DATABASE [SOA2] SET  READ_WRITE 
GO
