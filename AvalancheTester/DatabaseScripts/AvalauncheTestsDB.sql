USE [master]
GO
/****** Object:  Database [AvalauncheTestsDB]    Script Date: 17.10.2015 г. 21:46:34 ******/
CREATE DATABASE [AvalauncheTestsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AvalauncheTestsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AvalauncheTestsDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AvalauncheTestsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AvalauncheTestsDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AvalauncheTestsDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AvalauncheTestsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AvalauncheTestsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AvalauncheTestsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AvalauncheTestsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AvalauncheTestsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AvalauncheTestsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET RECOVERY FULL 
GO
ALTER DATABASE [AvalauncheTestsDB] SET  MULTI_USER 
GO
ALTER DATABASE [AvalauncheTestsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AvalauncheTestsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AvalauncheTestsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AvalauncheTestsDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AvalauncheTestsDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'AvalauncheTestsDB', N'ON'
GO
USE [AvalauncheTestsDB]
GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[OrganisationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Organizations_1] PRIMARY KEY CLUSTERED 
(
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[PlaceId] [int] NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
	[Area] [geometry] NULL,
 CONSTRAINT [PK_Place] PRIMARY KEY CLUSTERED 
(
	[PlaceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tests]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[TetsId] [int] IDENTITY(1,1) NOT NULL,
	[TestResults] [nvarchar](250) NOT NULL,
	[DangerLevel] [int] NOT NULL,
	[Position] [geography] NULL,
	[PlaceId] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Slope] [float] NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[TetsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tests Organisations]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests Organisations](
	[TestId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Tests Organisations] PRIMARY KEY CLUSTERED 
(
	[TestId] ASC,
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users Organisations]    Script Date: 17.10.2015 г. 21:46:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users Organisations](
	[UserId] [int] NOT NULL,
	[OrganisationId] [int] NOT NULL,
 CONSTRAINT [PK_Users Organisations] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[OrganisationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Place] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Place] ([PlaceId])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Place]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_Users]
GO
ALTER TABLE [dbo].[Tests Organisations]  WITH CHECK ADD  CONSTRAINT [FK_Tests Organisations_Organizations] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organizations] ([OrganisationId])
GO
ALTER TABLE [dbo].[Tests Organisations] CHECK CONSTRAINT [FK_Tests Organisations_Organizations]
GO
ALTER TABLE [dbo].[Tests Organisations]  WITH CHECK ADD  CONSTRAINT [FK_Tests Organisations_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([TetsId])
GO
ALTER TABLE [dbo].[Tests Organisations] CHECK CONSTRAINT [FK_Tests Organisations_Tests]
GO
ALTER TABLE [dbo].[Users Organisations]  WITH CHECK ADD  CONSTRAINT [FK_Users Organisations_Organizations] FOREIGN KEY([OrganisationId])
REFERENCES [dbo].[Organizations] ([OrganisationId])
GO
ALTER TABLE [dbo].[Users Organisations] CHECK CONSTRAINT [FK_Users Organisations_Organizations]
GO
ALTER TABLE [dbo].[Users Organisations]  WITH CHECK ADD  CONSTRAINT [FK_Users Organisations_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Users Organisations] CHECK CONSTRAINT [FK_Users Organisations_Users]
GO
USE [master]
GO
ALTER DATABASE [AvalauncheTestsDB] SET  READ_WRITE 
GO
