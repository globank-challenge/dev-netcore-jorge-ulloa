USE [master]
GO
/****** Object:  Database [OpBancarias]    Script Date: 7/28/2022 7:41:48 PM ******/
CREATE DATABASE [OpBancarias]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OpBancarias', FILENAME = N'/var/opt/mssql/data/OpBancarias.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OpBancarias_log', FILENAME = N'/var/opt/mssql/data/OpBancarias_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [OpBancarias] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OpBancarias].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OpBancarias] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OpBancarias] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OpBancarias] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OpBancarias] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OpBancarias] SET ARITHABORT OFF 
GO
ALTER DATABASE [OpBancarias] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OpBancarias] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OpBancarias] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OpBancarias] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OpBancarias] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OpBancarias] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OpBancarias] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OpBancarias] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OpBancarias] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OpBancarias] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OpBancarias] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OpBancarias] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OpBancarias] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OpBancarias] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OpBancarias] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OpBancarias] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [OpBancarias] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OpBancarias] SET RECOVERY FULL 
GO
ALTER DATABASE [OpBancarias] SET  MULTI_USER 
GO
ALTER DATABASE [OpBancarias] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OpBancarias] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OpBancarias] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OpBancarias] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OpBancarias] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'OpBancarias', N'ON'
GO
ALTER DATABASE [OpBancarias] SET QUERY_STORE = OFF
GO
USE [OpBancarias]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] NOT NULL,
	[UserName] [nvarchar](450) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[EstadoActivo] [bit] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [nvarchar](12) NOT NULL,
	[Tipo] [int] NOT NULL,
	[SaldoInicial] [decimal](12, 2) NOT NULL,
	[EstadoActivo] [bit] NOT NULL,
	[ClienteId] [int] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime2](7) NOT NULL,
	[Tipo] [int] NOT NULL,
	[Valor] [decimal](12, 2) NOT NULL,
	[Saldo] [decimal](12, 2) NOT NULL,
	[CuentaId] [int] NOT NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [nvarchar](10) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[Direccion] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 7/28/2022 7:41:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220718002824_InitialMigrationv1', N'6.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220718131417_SecondMigration', N'6.0.7')
GO
INSERT [dbo].[Clientes] ([Id], [UserName], [Password], [EstadoActivo]) VALUES (10, N'marianela.montalvo', N'5678', 1)
INSERT [dbo].[Clientes] ([Id], [UserName], [Password], [EstadoActivo]) VALUES (12, N'juan.osorio', N'1245PQssw0rd-', 1)
INSERT [dbo].[Clientes] ([Id], [UserName], [Password], [EstadoActivo]) VALUES (13, N'jose.lema', N'1234', 1)
GO
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (7, N'478758', 1, CAST(2000.00 AS Decimal(12, 2)), 1, 13)
INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (8, N'225487', 2, CAST(100.00 AS Decimal(12, 2)), 1, 10)
INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (9, N'495878', 1, CAST(0.00 AS Decimal(12, 2)), 1, 12)
INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (11, N'496825', 1, CAST(540.00 AS Decimal(12, 2)), 1, 10)
INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (14, N'585545', 2, CAST(1000.00 AS Decimal(12, 2)), 1, 13)
INSERT [dbo].[Cuentas] ([Id], [Numero], [Tipo], [SaldoInicial], [EstadoActivo], [ClienteId]) VALUES (15, N'495878', 1, CAST(0.00 AS Decimal(12, 2)), 1, 12)
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
GO
SET IDENTITY_INSERT [dbo].[Movimientos] ON 

INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (3, CAST(N'2022-07-21T11:45:30.2792523' AS DateTime2), 2, CAST(600.00 AS Decimal(12, 2)), CAST(700.00 AS Decimal(12, 2)), 8)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (4, CAST(N'2022-07-21T11:46:10.3721352' AS DateTime2), 2, CAST(150.00 AS Decimal(12, 2)), CAST(150.00 AS Decimal(12, 2)), 15)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (5, CAST(N'2022-07-21T11:47:36.7589218' AS DateTime2), 1, CAST(-575.00 AS Decimal(12, 2)), CAST(1425.00 AS Decimal(12, 2)), 7)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (6, CAST(N'2022-07-21T11:47:55.8246134' AS DateTime2), 1, CAST(-540.00 AS Decimal(12, 2)), CAST(0.00 AS Decimal(12, 2)), 11)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (7, CAST(N'2022-07-28T19:22:51.3160281' AS DateTime2), 2, CAST(1.00 AS Decimal(12, 2)), CAST(151.00 AS Decimal(12, 2)), 15)
INSERT [dbo].[Movimientos] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (8, CAST(N'2022-07-28T19:23:23.5701224' AS DateTime2), 1, CAST(-1.00 AS Decimal(12, 2)), CAST(150.00 AS Decimal(12, 2)), 15)
SET IDENTITY_INSERT [dbo].[Movimientos] OFF
GO
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([Id], [Identificacion], [Nombre], [Apellido], [Edad], [Direccion], [Telefono]) VALUES (10, N'1756880302', N'Marianela', N'Montalvo', 40, N'Amazonas y NNUU', N'097548965')
INSERT [dbo].[Personas] ([Id], [Identificacion], [Nombre], [Apellido], [Edad], [Direccion], [Telefono]) VALUES (12, N'1756880303', N'Juan', N'Osorio', 90, N'13 junio y Equinoccial', N'098874587')
INSERT [dbo].[Personas] ([Id], [Identificacion], [Nombre], [Apellido], [Edad], [Direccion], [Telefono]) VALUES (13, N'1756880301', N'Jose', N'Lema', 40, N'Otavalo sn y principal', N'098254785')
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [UserName], [Password]) VALUES (1, N'jorge.ulloa', N'MySecur3P@ss12345')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Clientes_UserName]    Script Date: 7/28/2022 7:41:48 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Clientes_UserName] ON [dbo].[Clientes]
(
	[UserName] ASC
)
WHERE ([UserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuentas_ClienteId]    Script Date: 7/28/2022 7:41:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Cuentas_ClienteId] ON [dbo].[Cuentas]
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Movimientos_CuentaId]    Script Date: 7/28/2022 7:41:48 PM ******/
CREATE NONCLUSTERED INDEX [IX_Movimientos_CuentaId] ON [dbo].[Movimientos]
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cuentas] ADD  CONSTRAINT [DF__Cuentas__Cliente__4222D4EF]  DEFAULT ((0)) FOR [ClienteId]
GO
ALTER TABLE [dbo].[Movimientos] ADD  CONSTRAINT [DF__Movimient__Cuent__412EB0B6]  DEFAULT ((0)) FOR [CuentaId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Personas_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Personas] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Personas_Id]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_Clientes_ClienteId] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_Clientes_ClienteId]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas_CuentaId] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas_CuentaId]
GO
USE [master]
GO
ALTER DATABASE [OpBancarias] SET  READ_WRITE 
GO
