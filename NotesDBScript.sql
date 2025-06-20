USE [master]
GO
/****** Object:  Database [NotesDB]    Script Date: 4/28/2025 9:40:14 AM ******/
CREATE DATABASE [NotesDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NotesDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\NotesDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NotesDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS01\MSSQL\DATA\NotesDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NotesDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NotesDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NotesDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NotesDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NotesDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NotesDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NotesDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [NotesDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [NotesDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NotesDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NotesDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NotesDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NotesDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NotesDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NotesDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NotesDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NotesDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NotesDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NotesDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NotesDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NotesDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NotesDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NotesDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NotesDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NotesDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NotesDB] SET  MULTI_USER 
GO
ALTER DATABASE [NotesDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NotesDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NotesDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NotesDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NotesDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NotesDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [NotesDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [NotesDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NotesDB]
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 4/28/2025 9:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[NoteTitle] [varchar](100) NOT NULL,
	[NoteContent] [text] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateUpdated] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotesTags]    Script Date: 4/28/2025 9:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotesTags](
	[NoteID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_NotesTags] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC,
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 4/28/2025 9:40:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[TagID] [int] IDENTITY(1,1) NOT NULL,
	[TagContent] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[NotesTags]  WITH CHECK ADD FOREIGN KEY([NoteID])
REFERENCES [dbo].[Notes] ([NoteID])
GO
ALTER TABLE [dbo].[NotesTags]  WITH CHECK ADD FOREIGN KEY([TagID])
REFERENCES [dbo].[Tags] ([TagID])
GO
USE [master]
GO
ALTER DATABASE [NotesDB] SET  READ_WRITE 
GO
