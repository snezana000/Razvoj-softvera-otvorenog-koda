USE [master]
GO
/****** Object:  Database [Turisticka agencija]    Script Date: 09/10/2022 16:16:44 ******/
CREATE DATABASE [Turisticka agencija] ON  PRIMARY 
( NAME = N'Turisticka agencija', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Turisticka agencija.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Turisticka agencija_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Turisticka agencija_log.LDF' , SIZE = 504KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Turisticka agencija] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Turisticka agencija].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Turisticka agencija] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Turisticka agencija] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Turisticka agencija] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Turisticka agencija] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Turisticka agencija] SET ARITHABORT OFF
GO
ALTER DATABASE [Turisticka agencija] SET AUTO_CLOSE ON
GO
ALTER DATABASE [Turisticka agencija] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Turisticka agencija] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Turisticka agencija] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Turisticka agencija] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Turisticka agencija] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Turisticka agencija] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Turisticka agencija] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Turisticka agencija] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Turisticka agencija] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Turisticka agencija] SET  ENABLE_BROKER
GO
ALTER DATABASE [Turisticka agencija] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Turisticka agencija] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Turisticka agencija] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Turisticka agencija] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Turisticka agencija] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Turisticka agencija] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Turisticka agencija] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Turisticka agencija] SET  READ_WRITE
GO
ALTER DATABASE [Turisticka agencija] SET RECOVERY SIMPLE
GO
ALTER DATABASE [Turisticka agencija] SET  MULTI_USER
GO
ALTER DATABASE [Turisticka agencija] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Turisticka agencija] SET DB_CHAINING OFF
GO
USE [Turisticka agencija]
GO
/****** Object:  Table [dbo].[Paket]    Script Date: 09/10/2022 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Paket](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KodPaketa] [int] NULL,
	[Naziv] [varchar](50) NULL,
	[Tip_paketa] [varchar](30) NULL,
	[Opis] [text] NULL,
	[Popust] [int] NULL,
	[TrajanjeOD] [date] NULL,
	[TrajanjeDO] [date] NULL,
 CONSTRAINT [PK__Paket__3214EC27CD0D0716] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Klijent]    Script Date: 09/10/2022 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Klijent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KodKlijenta] [int] NULL,
	[Ime] [varchar](30) NULL,
	[Prezime] [varchar](30) NULL,
	[Telefon] [int] NULL,
	[Adresa] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Destinacija]    Script Date: 09/10/2022 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Destinacija](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KodDrzave] [int] NULL,
	[Drzava] [varchar](30) NULL,
	[Mesto] [varchar](30) NULL,
	[Tip_Odmora] [varchar](10) NULL,
	[Kontinent] [varchar](15) NULL,
 CONSTRAINT [PK__Destinci__3214EC27F5790DA2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Ugovor]    Script Date: 09/10/2022 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ugovor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KodUgovora] [int] NULL,
	[Cena] [int] NULL,
	[Datum] [datetime] NULL,
	[Nocenja] [int] NULL,
	[BrojPutnika] [int] NULL,
	[ID_klijent] [int] NULL,
	[ID_paketa] [int] NULL,
	[ID_PocetnaDestinacija] [int] NULL,
	[ID_KrajnaDestinacija] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotel]    Script Date: 09/10/2022 16:16:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hotel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[KodHotela] [int] NULL,
	[Naziv] [varchar](20) NULL,
	[Adresa] [varchar](30) NULL,
	[Bazen] [varchar](5) NULL,
	[Klima] [varchar](5) NULL,
	[Parking] [varchar](5) NULL,
	[ID_destinacije] [int] NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
