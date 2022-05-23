USE [master]
GO

/****** Object:  Database [MassTransitDB]    Script Date: 2022/05/12 19:22:09 ******/
CREATE DATABASE [MassTransitDB]
    CONTAINMENT = NONE
    ON  PRIMARY
    ( NAME = N'MassTransitDB', FILENAME = N'/var/opt/mssql/data/MassTransitDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
    LOG ON
    ( NAME = N'MassTransitDB_log', FILENAME = N'/var/opt/mssql/data/MassTransitDB.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [MassTransitDB].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO

ALTER DATABASE [MassTransitDB] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [MassTransitDB] SET ANSI_NULLS OFF
GO

ALTER DATABASE [MassTransitDB] SET ANSI_PADDING OFF
GO

ALTER DATABASE [MassTransitDB] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [MassTransitDB] SET ARITHABORT OFF
GO

ALTER DATABASE [MassTransitDB] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [MassTransitDB] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [MassTransitDB] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [MassTransitDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [MassTransitDB] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [MassTransitDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [MassTransitDB] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [MassTransitDB] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [MassTransitDB] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [MassTransitDB] SET  DISABLE_BROKER
GO

ALTER DATABASE [MassTransitDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [MassTransitDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [MassTransitDB] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [MassTransitDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [MassTransitDB] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [MassTransitDB] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [MassTransitDB] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [MassTransitDB] SET RECOVERY FULL
GO

ALTER DATABASE [MassTransitDB] SET  MULTI_USER
GO

ALTER DATABASE [MassTransitDB] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [MassTransitDB] SET DB_CHAINING OFF
GO

ALTER DATABASE [MassTransitDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO

ALTER DATABASE [MassTransitDB] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO

ALTER DATABASE [MassTransitDB] SET DELAYED_DURABILITY = DISABLED
GO

ALTER DATABASE [MassTransitDB] SET ACCELERATED_DATABASE_RECOVERY = OFF
GO

ALTER DATABASE [MassTransitDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [MassTransitDB] SET  READ_WRITE
GO

USE [MassTransitDB]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 2022/05/12 19:13:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Login](
                              [LoginId] [int] IDENTITY(1,1) NOT NULL,
                              [Password] [nvarchar](15) NOT NULL,
                              [Username] [nvarchar](50) NOT NULL,
                              [IsActive] [bit] NOT NULL,
                              [CreatedDate] [datetime] NOT NULL
                                  CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED
                                      (
                                       [LoginId] ASC
                                          )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
                              CONSTRAINT [IX_Login] UNIQUE NONCLUSTERED
                                  (
                                   [Username] ASC
                                      )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO

ALTER TABLE [dbo].[Login] ADD  CONSTRAINT [DF_Login_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

USE [MassTransitDB]
GO

/****** Object:  StoredProcedure [dbo].[pr_GetAllLogins]    Script Date: 2022/05/12 19:30:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[pr_GetAllLogins]
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT lg.LoginId,
           lg.CreatedDate,
           lg.IsActive,
           lg.Username
    FROM [dbo].[Login] lg
    ORDER BY lg.LoginId
END
GO

USE [MassTransitDB]
GO

/****** Object:  StoredProcedure [dbo].[pr_GetLoginByUsernameAndPassword]    Script Date: 2022/05/12 19:30:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_GetLoginByUsernameAndPassword]
    -- Add the parameters for the stored procedure here
    @Username NVARCHAR(50),
    @Password NVARCHAR(15)
AS
BEGIN

    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT lg.LoginId,
           lg.UserName,
           lg.[Password],
           lg.IsActive,
           lg.CreatedDate
    FROM dbo.[Login] lg
    WHERE lg.UserName = @Username AND lg.[Password] = @Password

END
GO

USE [MassTransitDB]
GO

/****** Object:  StoredProcedure [dbo].[pr_LoginExists]    Script Date: 2022/05/12 19:31:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_LoginExists]
@Username NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert statements for procedure here
    SELECT lg.LoginId
    FROM dbo.[Login] lg
    WHERE lg.UserName = @Username
END
GO

USE [MassTransitDB]
GO

/****** Object:  StoredProcedure [dbo].[pr_RegisterLogin]    Script Date: 2022/05/12 19:35:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Alistair Ockhuis
-- Create date: 17/02/2022
-- Description:	Add Account
-- =============================================
CREATE PROCEDURE [dbo].[pr_RegisterLogin]
    -- Add the parameters for the stored procedure here
    @Username NVARCHAR(50),
    @Password NVARCHAR(15)
AS
BEGIN
    INSERT INTO dbo.[Login] (UserName, [Password])
    VALUES (@Username, @Password)
END
GO





