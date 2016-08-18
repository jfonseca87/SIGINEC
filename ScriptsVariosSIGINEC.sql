use siginec

--Crea el usuario administrador
insert into persona (Tipo_Documento, Numero_Documento, Nombre_1, Apellido_1, Email)
values ('CC', '1111111', 'Administrador', 'Administrador', 'admin@siginec.com')

insert into usuario (Nick_usuario, Password_Usuario, Activo, Tipo_Usuario)
values ('admin', '0192023a7bbd73250516f069df18b500', '1', 'adm')

go

--Creación Cliente de Prueba
insert into persona (Tipo_Documento, numero_documento, nombre_1, Apellido_1, Email, Cargo)
values ('NIT', '900123456', 'Claro', ' ', 'ingenieria@claro.com.co', 'Cliente')

insert into cliente (direccion, telefono, activo, Id_Persona)
values ('Calle 5 # 4 - 46', '7500500', 1, 2)

go

--Datos Menu1
insert into Menu1 (pagina, activa) values ('Dispositivo', 1)
insert into Menu1 (pagina, activa) values ('Bitacora', 1)
insert into Menu1 (pagina, activa) values ('Administración', 1)
insert into Menu1 (pagina, activa) values ('Informes', 1)

go

--Datos Menu2
insert into Menu2 (subpagina, activa, id_Menu1) values ('Ingreso Dispositivo', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitud Dispositivo', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitud BajoStock', 1, 1)
insert into Menu2 (subpagina, activa, id_Menu1) values
('Bitacora', 1, 2)
insert into Menu2 (subPagina, activa, id_Menu1) values ('Personas', 1, 3)
insert into Menu2 (subPagina, activa, id_Menu1) values ('Usuarios', 1, 3)
insert into Menu2 (subPagina, activa, id_Menu1) values ('Clientes', 1, 3)
insert into Menu2 (subPagina, activa, id_Menu1) values ('Responsables', 1, 3)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Ingreso Dispositivo', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitudes Dispositivo', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Solicitudes BajoStock', 1, 4)
insert into Menu2 (subpagina, activa, id_Menu1) values ('Bitacoras', 1, 4)

go

--Datos para los estados de las solicitudes
insert into Estados_Op (Estado_Op, activo) values ('En proceso', 1)
insert into Estados_Op (Estado_Op, activo) values ('Cerrado', 1)

go

--Datos para los estados del dispositivo
insert into Estado_Dispositivo (Estado, Activo) values ('Disponible', 1)
insert into Estado_Dispositivo (Estado, Activo) values ('Reparación', 1)
insert into Estado_Dispositivo (Estado, Activo) values ('Casa Matriz', 1)