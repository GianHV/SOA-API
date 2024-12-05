
CREATE DATABASE [SOA1]
USE [SOA1]
GO
/****** Object:  Table [dbo].[products]    Script Date: 12/5/2024 10:54:54 AM ******/
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
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT (getdate()) FOR [created_at]
GO
/****** Object:  StoredProcedure [dbo].[sp_add_product]    Script Date: 12/5/2024 10:54:54 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_delete_product]    Script Date: 12/5/2024 10:54:54 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_edit_product]    Script Date: 12/5/2024 10:54:54 AM ******/
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
	UPDATE products
	SET name = CASE WHEN @name IS NULL
		THEN name ELSE @name end,
	description = CASE WHEN @description IS NULL
		THEN description ELSE @description end,
	price = CASE WHEN @price IS NULL
		THEN price ELSE @price end,
	quantity = CASE WHEN @quantity IS NULL
		THEN quantity ELSE @quantity end,
	updated_at = GETDATE()
	WHERE id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_get_product_by_id]    Script Date: 12/5/2024 10:54:54 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_get_products]    Script Date: 12/5/2024 10:54:54 AM ******/
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
