USE [master]
GO
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'AHI')
	BEGIN
		ALTER DATABASE [AHI] SET single_user WITH ROLLBACK IMMEDIATE
		DROP DATABASE [AHI]
	END
GO
CREATE DATABASE [AHI]
GO
USE [AHI]
GO
CREATE TABLE [dbo].[dispositivo](
	[iddispositivo] [int] IDENTITY(1,1) NOT NULL,
	[mac] [char](17) NOT NULL,
PRIMARY KEY
(
	[iddispositivo] ASC
)
)

GO

GO

GO

GO

GO
CREATE TABLE [dbo].[ubicacion](
	[idubicacion] [int] IDENTITY(1,1) NOT NULL,
	[idviaje] [int] NOT NULL,
	[latitud] [varchar](20) NOT NULL,
	[longitud] [varchar](20) NOT NULL,
	[hora] [datetime] NOT NULL,
PRIMARY KEY
(
	[idubicacion] ASC
)
)

GO

GO

GO

GO

GO
CREATE TABLE [dbo].[usuario](
	[idusuario] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](12) NOT NULL,
	[password] [char](32) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
PRIMARY KEY
(
	[idusuario] ASC
)
)

GO

GO

GO

GO
CREATE TABLE [dbo].[usuarioviaje](
	[idusuario] [int] NOT NULL,
	[idviaje] [int] NOT NULL,
PRIMARY KEY
(
	[idusuario] ASC,
	[idviaje] ASC
)
)

GO

GO

GO

GO
CREATE TABLE [dbo].[vehiculo](
	[idvehiculo] [int] IDENTITY(1,1) NOT NULL,
	[placa] [char](7) NOT NULL,
PRIMARY KEY
(
	[idvehiculo] ASC
)
)

GO

GO

GO

GO

GO
CREATE TABLE [dbo].[viaje](
	[idviaje] [int] IDENTITY(1,1) NOT NULL,
	[iddispositivo] [int] NOT NULL,
	[idvehiculo] [int] NOT NULL,
	[descripcion] [varchar](100) NOT NULL,
	[horainicio] [datetime] NOT NULL,
	[horafin] [datetime] NULL,
PRIMARY KEY
(
	[idviaje] ASC
)
)

GO

GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([idusuario], [username], [password], [nombre]) VALUES (1, N'admin', N'21232f297a57a5a743894a0e4a801fc3', N'Administrador')
INSERT [dbo].[usuario] ([idusuario], [username], [password], [nombre]) VALUES (2, N'cliente1', N'202cb962ac59075b964b07152d234b70', N'Cliente 1')
INSERT [dbo].[usuario] ([idusuario], [username], [password], [nombre]) VALUES (3, N'cliente2', N'202cb962ac59075b964b07152d234b70', N'Cliente 2')
INSERT [dbo].[usuario] ([idusuario], [username], [password], [nombre]) VALUES (4, N'cliente3', N'202cb962ac59075b964b07152d234b70', N'Cliente 3')
SET IDENTITY_INSERT [dbo].[usuario] OFF
SET IDENTITY_INSERT [dbo].[vehiculo] ON 

INSERT [dbo].[vehiculo] ([idvehiculo], [placa]) VALUES (1, N'ABA-001')
INSERT [dbo].[vehiculo] ([idvehiculo], [placa]) VALUES (2, N'BCB-002')
INSERT [dbo].[vehiculo] ([idvehiculo], [placa]) VALUES (3, N'CDC-003')
INSERT [dbo].[vehiculo] ([idvehiculo], [placa]) VALUES (4, N'DED-004')
SET IDENTITY_INSERT [dbo].[vehiculo] OFF


GO
ALTER TABLE [dbo].[dispositivo] ADD UNIQUE
(
	[mac] ASC
)
GO


GO
ALTER TABLE [dbo].[usuario] ADD UNIQUE
(
	[username] ASC
)
GO


GO
ALTER TABLE [dbo].[vehiculo] ADD UNIQUE
(
	[placa] ASC
)
GO
ALTER TABLE [dbo].[ubicacion] ADD  DEFAULT (getdate()) FOR [hora]
GO
ALTER TABLE [dbo].[viaje] ADD  DEFAULT (getdate()) FOR [horainicio]
GO
ALTER TABLE [dbo].[ubicacion]  WITH CHECK ADD FOREIGN KEY([idviaje])
REFERENCES [dbo].[viaje] ([idviaje])
GO
ALTER TABLE [dbo].[usuarioviaje]  WITH CHECK ADD FOREIGN KEY([idusuario])
REFERENCES [dbo].[usuario] ([idusuario])
GO
ALTER TABLE [dbo].[usuarioviaje]  WITH CHECK ADD FOREIGN KEY([idviaje])
REFERENCES [dbo].[viaje] ([idviaje])
GO
ALTER TABLE [dbo].[viaje]  WITH CHECK ADD FOREIGN KEY([iddispositivo])
REFERENCES [dbo].[dispositivo] ([iddispositivo])
GO
ALTER TABLE [dbo].[viaje]  WITH CHECK ADD FOREIGN KEY([idvehiculo])
REFERENCES [dbo].[vehiculo] ([idvehiculo])
GO