USE [master]
GO

/****** Object:  Database [MassTransitDB]    Script Date: 2022/05/12 19:22:09 ******/
CREATE DATABASE [MassTransitLoginDB]
    CONTAINMENT = NONE
    ON  PRIMARY
    ( NAME = N'MassTransitLoginDB', FILENAME = N'/var/opt/mssql/data/MassTransitLoginDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
    LOG ON
    ( NAME = N'MassTransitLoginDB_log', FILENAME = N'/var/opt/mssql/data/MassTransitLoginDB.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [MassTransitDB].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO

ALTER DATABASE [MassTransitLoginDB] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET ANSI_NULLS OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET ANSI_PADDING OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET ARITHABORT OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [MassTransitLoginDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [MassTransitLoginDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET  DISABLE_BROKER
GO

ALTER DATABASE [MassTransitLoginDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [MassTransitLoginDB] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET RECOVERY FULL
GO

ALTER DATABASE [MassTransitLoginDB] SET  MULTI_USER
GO

ALTER DATABASE [MassTransitLoginDB] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [MassTransitLoginDB] SET DB_CHAINING OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO

ALTER DATABASE [MassTransitLoginDB] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO

ALTER DATABASE [MassTransitLoginDB] SET DELAYED_DURABILITY = DISABLED
GO

ALTER DATABASE [MassTransitLoginDB] SET ACCELERATED_DATABASE_RECOVERY = OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [MassTransitLoginDB] SET  READ_WRITE
GO

USE [MassTransitLoginDB]
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

USE [MassTransitLoginDB]
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

USE [MassTransitLoginDB]
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

USE [MassTransitLoginDB]
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

USE [MassTransitLoginDB]
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
	SELECT SCOPE_IDENTITY()
END
GO

USE [master]
GO

/****** Object:  Database [MassTransitDB]    Script Date: 2022/05/12 19:22:09 ******/
CREATE DATABASE [MassTransitAccountDB]
    CONTAINMENT = NONE
    ON  PRIMARY
    ( NAME = N'MassTransitAccountDB', FILENAME = N'/var/opt/mssql/data/MassTransitAccountDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
    LOG ON
    ( NAME = N'MassTransitAccountDB_log', FILENAME = N'/var/opt/mssql/data/MassTransitAccountDB.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
    begin
        EXEC [MassTransitAccountDB].[dbo].[sp_fulltext_database] @action = 'enable'
    end
GO

ALTER DATABASE [MassTransitAccountDB] SET ANSI_NULL_DEFAULT OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET ANSI_NULLS OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET ANSI_PADDING OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET ANSI_WARNINGS OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET ARITHABORT OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET AUTO_CLOSE OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET AUTO_SHRINK OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET AUTO_UPDATE_STATISTICS ON
GO

ALTER DATABASE [MassTransitAccountDB] SET CURSOR_CLOSE_ON_COMMIT OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET CURSOR_DEFAULT  GLOBAL
GO

ALTER DATABASE [MassTransitAccountDB] SET CONCAT_NULL_YIELDS_NULL OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET NUMERIC_ROUNDABORT OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET QUOTED_IDENTIFIER OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET RECURSIVE_TRIGGERS OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET  DISABLE_BROKER
GO

ALTER DATABASE [MassTransitAccountDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET DATE_CORRELATION_OPTIMIZATION OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET TRUSTWORTHY OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET PARAMETERIZATION SIMPLE
GO

ALTER DATABASE [MassTransitAccountDB] SET READ_COMMITTED_SNAPSHOT OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET HONOR_BROKER_PRIORITY OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET RECOVERY FULL
GO

ALTER DATABASE [MassTransitAccountDB] SET  MULTI_USER
GO

ALTER DATABASE [MassTransitAccountDB] SET PAGE_VERIFY CHECKSUM
GO

ALTER DATABASE [MassTransitAccountDB] SET DB_CHAINING OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF )
GO

ALTER DATABASE [MassTransitLoginDB] SET TARGET_RECOVERY_TIME = 60 SECONDS
GO

ALTER DATABASE [MassTransitAccountDB] SET DELAYED_DURABILITY = DISABLED
GO

ALTER DATABASE [MassTransitAccountDB] SET ACCELERATED_DATABASE_RECOVERY = OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [MassTransitAccountDB] SET  READ_WRITE
GO

USE [MassTransitAccountDB]
GO

/****** Object:  Table [dbo].[Gender]    Script Date: 2022/06/07 19:12:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Gender](
                               [GenderId] [int] IDENTITY(1,1) NOT NULL,
                               [Gender] [varchar](5) NOT NULL,
                               CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED
                                   (
                                    [GenderId] ASC
                                       )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [MassTransitAccountDB]
GO

INSERT INTO Gender (Gender) VALUES ('Male')
INSERT INTO Gender (Gender) VALUES ('Female')

USE [MassTransitAccountDB]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 2022/06/07 19:12:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Account](
                                [AcountId] [int] NOT NULL,
                                [Firstname] [nvarchar](50) NOT NULL,
                                [Lastname] [nvarchar](50) NOT NULL,
                                [Email] [nvarchar](50) NOT NULL,
                                [Gender] [int] NOT NULL,
                                [AddressLine1] [nvarchar](50) NOT NULL,
                                [AddressLine2] [nvarchar](50) NOT NULL,
                                [AddressLine3] [nvarchar](50) NULL,
                                [City] [nvarchar](50) NOT NULL,
                                [Country] [nvarchar](50) NOT NULL,
                                [PostalCode] [int] NOT NULL,
                                CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED
                                    (
                                     [AcountId] ASC
                                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Gender] FOREIGN KEY([Gender])
    REFERENCES [dbo].[Gender] ([GenderId])
GO

ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Gender]
GO

USE [MassTransitAccountDB]
GO

/****** Object:  StoredProcedure [dbo].[pr_RegisterAccount]    Script Date: 2022/06/07 19:12:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_RegisterAccount]
    -- Add the parameters for the stored procedure here
    @AccountId INT,
    @Firstname NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email NVARCHAR(50),
    @Gender INT,
    @AddressLine1 NVARCHAR(50),
    @AddressLine2 NVARCHAR(50),
    @AddressLine3 NVARCHAR(50),
    @City NVARCHAR(50),
    @Country NVARCHAR (50),
    @PostalCode INT

AS
BEGIN
    INSERT INTO [dbo].[Account]
    ([AcountId]
    ,[Firstname]
    ,[Lastname]
    ,[Gender]
    ,[AddressLine1]
    ,[AddressLine2]
    ,[AddressLine3]
    ,[City]
    ,[Country]
    ,[PostalCode])
    VALUES
        (@AccountId
        ,@Firstname
        ,@LastName
        ,@Gender
        ,@AddressLine1
        ,@AddressLine2
        ,@AddressLine3
        ,@City
        ,@Country
        ,@PostalCode)
END
GO



