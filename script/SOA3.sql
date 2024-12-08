USE [master]
GO
/****** Object:  Database [SOA3]    Script Date: 12/8/2024 8:30:40 PM ******/
CREATE DATABASE [SOA3]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SOA3', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA3.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SOA3_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SOA3_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SOA3] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SOA3].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SOA3] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SOA3] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SOA3] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SOA3] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SOA3] SET ARITHABORT OFF 
GO
ALTER DATABASE [SOA3] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SOA3] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SOA3] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SOA3] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SOA3] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SOA3] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SOA3] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SOA3] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SOA3] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SOA3] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SOA3] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SOA3] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SOA3] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SOA3] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SOA3] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SOA3] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SOA3] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SOA3] SET RECOVERY FULL 
GO
ALTER DATABASE [SOA3] SET  MULTI_USER 
GO
ALTER DATABASE [SOA3] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SOA3] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SOA3] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SOA3] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SOA3] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SOA3] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SOA3', N'ON'
GO
ALTER DATABASE [SOA3] SET QUERY_STORE = ON
GO
ALTER DATABASE [SOA3] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SOA3]
GO
/****** Object:  Table [dbo].[order_reports]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NULL,
	[totalRevenue] [decimal](10, 2) NULL,
	[totalCost] [decimal](10, 2) NULL,
	[totalProfit] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_reports]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_reports](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderReportId] [int] NULL,
	[productId] [int] NULL,
	[totalSold] [int] NULL,
	[revenue] [decimal](10, 2) NULL,
	[cost] [decimal](10, 2) NULL,
	[profit] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[order_reports] ON 

INSERT [dbo].[order_reports] ([id], [orderId], [totalRevenue], [totalCost], [totalProfit]) VALUES (4, 3, CAST(35.00 AS Decimal(10, 2)), CAST(35.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[order_reports] OFF
GO
SET IDENTITY_INSERT [dbo].[product_reports] ON 

INSERT [dbo].[product_reports] ([id], [orderReportId], [productId], [totalSold], [revenue], [cost], [profit]) VALUES (6, 4, 3, 2, CAST(10.00 AS Decimal(10, 2)), CAST(10.00 AS Decimal(10, 2)), CAST(0.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[product_reports] OFF
GO
ALTER TABLE [dbo].[order_reports] ADD  DEFAULT ((0)) FOR [totalRevenue]
GO
ALTER TABLE [dbo].[order_reports] ADD  DEFAULT ((0)) FOR [totalCost]
GO
ALTER TABLE [dbo].[order_reports] ADD  DEFAULT ((0)) FOR [totalProfit]
GO
ALTER TABLE [dbo].[product_reports] ADD  DEFAULT ((0)) FOR [profit]
GO
ALTER TABLE [dbo].[product_reports]  WITH CHECK ADD FOREIGN KEY([orderReportId])
REFERENCES [dbo].[order_reports] ([id])
ON DELETE CASCADE
GO
/****** Object:  StoredProcedure [dbo].[sp_add_order_report]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_order_report]
@orderId INT,
@id INT OUTPUT
AS
BEGIN
	INSERT INTO order_reports(orderId) VALUES (@orderId)
	
	SET @id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_add_product_report]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[sp_add_product_report]
@orderReportId INT,
@productId INT,
@totalSold INT,
@revenue DECIMAL(10,2),
@cost DECIMAL(10,2),
@id INT out
AS
BEGIN
	INSERT INTO product_reports(orderReportId, productId, totalSold, revenue, cost, profit)
	VALUES(@orderReportId, @productId, @totalSold, @revenue, @cost, @revenue - @cost)

	SET @id = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_order_report]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_order_report]
@id INT
AS
BEGIN
	DELETE FROM order_reports
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_delete_product_report]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_delete_product_report]
@id INT
AS
BEGIN
	DELETE FROM product_reports
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_order_report_by_id]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[sp_get_order_report_by_id]
@id INT
AS
BEGIN
	SELECT * FROM order_reports
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_order_reports]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_order_reports]
AS
BEGIN
	SELECT * FROM order_reports
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_product_report_by_id]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_product_report_by_id]
@id INT
AS
BEGIN
	SELECT * FROM product_reports
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_product_reports]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_get_product_reports]
AS
BEGIN
	SELECT * FROM product_reports
END
GO
/****** Object:  StoredProcedure [dbo].[sp_update_order_report]    Script Date: 12/8/2024 8:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROC [dbo].[sp_update_order_report]
@id INT,
@totalRevenue DECIMAL(10,2),
@totalCost DECIMAL(10,2)
AS
BEGIN
	UPDATE order_reports
	SET
	totalRevenue = @totalRevenue,
	totalCost = @totalCost,
	totalProfit = @totalRevenue - @totalCost
	WHERE id = @id
END
GO
USE [master]
GO
ALTER DATABASE [SOA3] SET  READ_WRITE 
GO
