use siginec

insert into persona (Tipo_Documento, Numero_Documento, Nombre_1, Apellido_1, Email)
values ('CC', '1111111', 'Administrador', 'Administrador', 'admin@siginec.com')

insert into usuario (Nick_usuario, Password_Usuario, Activo)
values ('admin', '0192023a7bbd73250516f069df18b500', '1')

select * from persona
select * from usuario
