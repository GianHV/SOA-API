USE [master]
GO
/****** Object:  Database [SOA1]    Script Date: 12/8/2024 8:26:07 PM ******/
CREATE DATABASE [SOA1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SOA1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SOA1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SOA1] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SOA1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SOA1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SOA1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SOA1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SOA1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SOA1] SET ARITHABORT OFF 
GO
ALTER DATABASE [SOA1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SOA1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SOA1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SOA1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SOA1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SOA1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SOA1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SOA1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SOA1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SOA1] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SOA1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SOA1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SOA1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SOA1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SOA1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SOA1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SOA1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SOA1] SET RECOVERY FULL 
GO
ALTER DATABASE [SOA1] SET  MULTI_USER 
GO
ALTER DATABASE [SOA1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SOA1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SOA1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SOA1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SOA1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SOA1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SOA1', N'ON'
GO
ALTER DATABASE [SOA1] SET QUERY_STORE = ON
GO
ALTER DATABASE [SOA1] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SOA1]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[description] [text] NULL,
	[price] [decimal](10, 2) NULL,
	[quantity] [int] NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [name], [description], [price], [quantity], [createdAt], [updatedAt]) VALUES (1, N'ao thun', N'ffdfd', CAST(12.50 AS Decimal(10, 2)), 1, CAST(N'2024-12-05T10:21:50.303' AS DateTime), CAST(N'2024-12-06T10:49:19.647' AS DateTime))
INSERT [dbo].[products] ([id], [name], [description], [price], [quantity], [createdAt], [updatedAt]) VALUES (3, N'ao hai lo', N'hoi nach', CAST(5.00 AS Decimal(10, 2)), 7, CAST(N'2024-12-05T10:39:20.630' AS DateTime), CAST(N'2024-12-06T10:49:19.667' AS DateTime))
SET IDENTITY_INSERT [dbo].[products] OFF
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_product]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_product]
@name VARCHAR(255),
@description TEXT,
@price DECIMAL(10,2),
@quantity INT,
@id int output
AS
BEGIN
	INSERT INTO products(name, description, price, quantity) VALUES(@name, @description, @price, @quantity)
	SET @id = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_product]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_delete_product]
@id INT
AS
BEGIN
	DELETE FROM products
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_edit_product]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_edit_product]
@id INT,
@name VARCHAR(255),
@description TEXT,
@price DECIMAL(10,2),
@quantity INT
AS
BEGIN
	DECLARE @new_quantity INT;

    -- Lấy quantity hiện tại của sản phẩm
    SELECT @new_quantity = quantity
    FROM products
    WHERE id = @id;

    -- Tính toán giá trị mới của quantity
    IF @quantity IS NOT NULL
    BEGIN
        SET @new_quantity = @new_quantity + @quantity;
    END

    -- Kiểm tra nếu giá trị mới của quantity là âm
    IF @new_quantity < 0
    BEGIN
        RAISERROR ('Số lượng sản phẩm hiện tại không đủ', 16, 1);
        RETURN;
    END

	UPDATE products
	SET name = CASE WHEN @name IS NULL
		THEN name ELSE @name end,
	description = CASE WHEN @description IS NULL
		THEN description ELSE @description end,
	price = CASE WHEN @price IS NULL
			THEN price
			ELSE @price end,
	quantity = @new_quantity,
	updatedAt = GETDATE()
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_product_by_id]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_get_product_by_id]
@id INT
AS
BEGIN
	SELECT * FROM products
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_products]    Script Date: 12/8/2024 8:26:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[sp_get_products]
AS
BEGIN
	SELECT * FROM products
END
GO
USE [master]
GO
ALTER DATABASE [SOA1] SET  READ_WRITE 
GO
