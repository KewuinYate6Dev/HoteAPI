create database HotelApii;

use HotelApii;

create table TiposUsuario(
	id int not null primary key auto_increment,
    nombres varchar(20) not null
);


create table Usuarios(
	id int not null primary key,
    nombres varchar(50) not null,
    apellidos varchar(50) not null,
    email varchar(50) not null,
    documento varchar(50) not null,
    tipo_documento smallint not null /*1. CC, 2. CE*/,
    tipo_usuario int not null,
    contrasena varchar(50) not null,
    constraint fk_us_tpUs foreign key (tipo_usuario) references tiposusuario(id)
);



create table Hoteles(
	id int not null primary key auto_increment,
	nombre varchar(100) not null,
    direccion varchar(100) not null,
    ciudad varchar(100) not null,
    telefono varchar(11) not null,
    habilitado boolean
);

create table TiposHabitacion(
	id int not null primary key auto_increment,
    nombre varchar(50) not null

);

create table TiposVista(
	id int not null primary key,
    descripcion varchar(100) not null
);

create table Habitaciones(
	id int not null primary key auto_increment,
    hotel int not null,
    cantidad_personas int not null,
    costo_base int not null,
    impuestos double not null,
    tipo_habitacion int not null,
    piso smallint,
    tipo_vista int not null,
    habilitado boolean not null,
    constraint fk_ha_ho foreign key (hotel) references Hoteles(id),
    constraint fk_ha_tpha foreign key (tipo_habitacion) references tiposhabitacion(id),
    constraint fk_ha_tpvi foreign key (tipo_vista) references tiposvista(id)
    
);

create table Reservas (
	id int not null primary key auto_increment,
    habitacion int not null,
    usuario int not null,
    llegada datetime,
    salida datetime,
    noches int,
    total int,
    estado smallint, /*1 activa, 2 cancelada, 3 cancelada Admin, 4 cumplida*/
    
	constraint fk_re_ha foreign key (habitacion) references Habitaciones(id),
    constraint fk_re_us foreign key (usuario) references Usuarios(id)

);

create table Huespedes(
	id int not null primary key auto_increment,
    nombres varchar(100) not null,
    apellidos varchar(100) not null,
    documento varchar(100) not null,
    edad varchar(100) not null,
	reserva int not null,
    fecha_nacimiento DATETIME NOT NULL,
    genero VARCHAR(1) NOT NULL,
    tipo_documento SMALLINT(1) NOT NULL,
    email VARCHAR(50) NOT NULL,
    telefono VARCHAR(11) NOT NULL,
    
    constraint fk_hu_re foreign key (reserva) references reservas(id)
);

create table ContactosEmergencia(
	id int not null primary key auto_increment,
    nombres varchar(100) not null,
    apellidos varchar(100) not null,
	reserva int not null,
    telefono VARCHAR(11) NOT NULL,
    
    constraint fk_coem_re foreign key (reserva) references reservas(id)
);


INSERT INTO tiposusuario (`nombres`) VALUES ('admin');
INSERT INTO tiposusuario (`nombres`) VALUES ('agente');
INSERT INTO tiposusuario (`nombres`) VALUES ('cliente');


INSERT INTO usuarios (`id`, `nombres`, `apellidos`, `email`, `documento`, `tipo_documento`, `tipo_usuario`, `contrasena`) VALUES ('10102020', 'Admin', 'Admin', 'kevinyate6@gmail.com', '10102020', '1', '1', 'Clave123');
INSERT INTO usuarios (`id`, `nombres`, `apellidos`, `email`, `documento`, `tipo_documento`, `tipo_usuario`, `contrasena`) VALUES ('1024581391', 'Kewuin', 'Yate', 'kevinyate6@gmail.com', '1024581391', '1', '3', 'Clave123');
INSERT INTO usuarios (`id`, `nombres`, `apellidos`, `email`, `documento`, `tipo_documento`, `tipo_usuario`, `contrasena`) VALUES ('896567890', 'Maria', 'Pelaez', 'pelaez@gmail.com', '896567890', '1', '3', 'Clave123');


INSERT INTO tiposhabitacion (`nombre`) VALUES ('Individuales');
INSERT INTO tiposhabitacion (`nombre`) VALUES ('Dobles');
INSERT INTO tiposhabitacion (`nombre`) VALUES ('Cuádruples');
INSERT INTO tiposhabitacion (`nombre`) VALUES ('Suite');
INSERT INTO tiposhabitacion (`nombre`) VALUES ('Junior suite');
INSERT INTO tiposhabitacion (`nombre`) VALUES ('Gran suite');


INSERT INTO tiposvista (`id`, `descripcion`) VALUES ('1', 'Vista al mar');
INSERT INTO tiposvista (`id`, `descripcion`) VALUES ('2', 'Vista A la piscina');
INSERT INTO tiposvista (`id`, `descripcion`) VALUES ('3', 'Vista Parque Principal');
INSERT INTO tiposvista (`id`, `descripcion`) VALUES ('4', 'Vista al centro de la ciudad');
