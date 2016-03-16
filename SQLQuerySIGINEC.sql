CREATE DATABASE SIGINEC

GO

USE SIGINEC 

GO

CREATE TABLE Persona
(
	Id_Persona int identity(1,1) primary key,
	Tipo_Documento char(2) not null,
	Numero_Documento varchar(50)not null,
	Nombre_1 varchar(50)not null,
	Nombre_2 varchar(50),
	Apellido_1 varchar(50) not null,
	Apellido_2 varchar(50),
	Email varchar(100) not null,
	Cargo varchar(100) 
)

GO

CREATE TABLE Usuario
(
	Id_Usuario int identity(1,1) primary key,
	Nick_usuario varchar(50)not null,
	Password_Usuario varchar(32)not null,
	Activo int, 
	Id_Persona int,
	foreign key (Id_Persona) references Persona(Id_Persona)
	on delete cascade 
)

GO

CREATE TABLE Cliente
(
	Id_Cliente int identity(1,1) primary key,
	Direccion varchar(100)not null,
	Telefono varchar(10) not null,
	Activo int, 
	Id_Persona int,
	foreign key (Id_Persona) references Persona(Id_Persona)
	on delete cascade
)

GO

CREATE TABLE Dispositivo
(
	Id_Dispositivo int identity(1,1) primary key,
	Nombre varchar(100) not null,
	Clase varchar(100) not null,
	Marca varchar(100) not null,
	Modelo varchar(100) not null,
	Caracteristicas varchar(max),
	Configuracion varchar(max),
	Cantidad int
)

GO

CREATE TABLE Estado_Dispositivo
(
	id_Estado int identity(1,1) primary key,
	Estado varchar(15) not null,
	Activo int
)

GO

CREATE TABLE Estados_Op
(
	id_Estado int identity(1,1) primary key,
	Estado_Op varchar(15),
	Activo int
)

GO

CREATE TABLE Ingreso_Dispositivo
(
	Id_Ingreso int identity(1,1) primary key,
	Observaciones varchar(max),
	Id_Dispositivo int,
	Usuario_Registra int,
	Fecha_Registro datetime
	foreign key (Usuario_Registra) references Usuario(Id_Usuario)
	on delete cascade,
	foreign key	(Id_Dispositivo) references Dispositivo(Id_Dispositivo)
	on delete cascade
)

GO

CREATE TABLE Solicitud_Dispositivo
(
	Id_Solicitud int identity(1,1) primary key,
	Observaciones varchar(max),
	Estado_Solicitud int,
	Id_Dispositivo int,
	Id_Cliente int,
	Usuario_SolDispositivo int, 
	Fecha_Solicitud datetime,
	foreign key (Id_Dispositivo) references Dispositivo(Id_Dispositivo)
	on delete cascade,
	foreign key (Id_Cliente) references Cliente(Id_Cliente)
	on delete cascade,
	foreign key (Usuario_SolDispositivo) references Usuario(Id_Usuario),
	foreign key (Estado_Solicitud) references Estados_Op(Id_Estado)
	on delete cascade
)

GO

CREATE TABLE Seguimiento_SolDispositivo
(
	Id_Seguimiento int identity(1,1) primary key,
	Seguimiento varchar(max) not null,
	Usuario_Seguimiento int not null,
	Fecha_Seguimiento Datetime,
	Id_SolicitudDisp int,
	foreign key (Id_SolicitudDisp) references Solicitud_Dispositivo(Id_Solicitud)
	on delete cascade
)

GO

CREATE TABLE Solicitud_BajoStock
(
	Id_Solicitud int identity(1,1) primary key,
	Observaciones varchar(max),
	Estado_Solicitud int,
	Usuario_SolBajoStock int,
	Fecha_Solicitud datetime,
	Usuario_Responsable int,
	foreign key (Usuario_SolBajoStock) references Usuario(Id_Usuario),
	foreign key (Estado_Solicitud) references Estados_Op(Id_Estado)
	on delete cascade
)

GO 

CREATE TABLE Detalle_Solicitud_BajoStock
(
	Id_Detalle int identity(1,1) primary key,
	Id_Dispositivo int,
	Cantidad int,
	Id_Solicitud_Stock int,
	foreign key (Id_Dispositivo) references Dispositivo(Id_Dispositivo)
	on delete cascade,
	foreign key (Id_Solicitud_Stock) references Solicitud_BajoStock(Id_Solicitud)
	on delete cascade
)

GO

CREATE TABLE Seguimiento_BajoStock
(
	Id_Seguimiento int identity(1,1) primary key,
	Seguimiento varchar(max) not null,
	Usuario_Seguimiento int not null,
	Fecha_Seguimiento Datetime,
	Id_Solicitud int,
	foreign key (Id_Solicitud) references Solicitud_BajoStock(Id_Solicitud)
	on delete cascade
)

GO

CREATE TABLE Bitacora
(
	Id_Bitacora int identity(1,1) primary key,
	Id_Dispositivo int,
	Estado_Dispositivo int,
	Detalles_Revision varchar(max),
	Observaciones varchar(max),
	Usuario_Registra int,
	Fecha_Registro datetime
	foreign key (Id_Dispositivo) references Dispositivo(Id_Dispositivo)
	on delete cascade,
	foreign key (Usuario_Registra) references Usuario(Id_Usuario)
	on delete cascade,
	foreign key (Estado_Dispositivo) references Estado_Dispositivo(Id_Estado)
	on delete cascade
)

GO

CREATE TABLE Detalle_Bitacora
(
	Id_Detalle int identity(1,1) primary key,
	Fotografia varchar(max),
	Observacion_Foto varchar(max),
	Id_Bitacora int,
	Foreign key (Id_Bitacora) references Bitacora(Id_Bitacora)
	on delete cascade
)

