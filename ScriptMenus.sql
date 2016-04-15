use SIGINEC

go

create table Menu1
(
	id_Menu1 int identity(1,1) primary key,
	pagina varchar(50) not null,
	activa int 
)

go

create table Menu2
(
	id_Menu2 int identity(1,1) primary key,
	subPagina varchar(50) not null,
	activa int,
	id_Menu1 int,
	foreign key (id_Menu1) references Menu1(id_Menu1)
)

go

insert into Menu1 (pagina, activa) values ('Dispositivo', 1)
insert into Menu1 (pagina, activa) values ('Bitacora', 1)
insert into Menu1 (pagina, activa) values ('Administración', 1)
insert into Menu1 (pagina, activa) values ('Informes', 1)

go

insert into Menu2 (subpagina, activa, id_Menu1) values ('Ingreso Dispositivo', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitud Dispositivo', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitud BajoStock', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Persona', 1, 3)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Configuración', 1, 3)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Ingreso Dispositivo', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitudes Dispositivo', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitudes BajoStock', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Bitacoras', 1, 4)