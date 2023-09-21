DELIMITER //

CREATE PROCEDURE UsuarioById(
IN p_usuario INT
)
BEGIN
    SELECT id, nombres, apellidos, email, documento, tipo_usuario
    FROM usuarios
    WHERE id = p_usuario;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE UsuarioByDocumentAndPassword(
IN p_documento INT,
IN p_password varchar(50)
)
BEGIN
    SELECT u.id, u.nombres, u.apellidos, u.email, u.documento, tu.nombres as tipo_usuario
    FROM usuarios u
    INNER JOIN tiposusuario tu ON tu.id = u.tipo_usuario
    WHERE u.documento = p_documento
    AND u.contrasena = p_password;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE ReservaById(IN reserva_id INT)
BEGIN

    SELECT R.id, R.habitacion, R.usuario, R.llegada, R.salida, R.noches, R.total, R.estado,
    H.hotel, H.cantidad_personas, H.costo_base, H.impuestos, H.tipo_habitacion, H.piso, H.tipo_vista,
    T.nombre, T.direccion, T.ciudad, T.telefono
    FROM Reservas R
    INNER JOIN Habitaciones H ON R.habitacion = H.id
    INNER JOIN hoteles T ON T.id = H.hotel
    WHERE R.id = reserva_id;
    
    
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE ListarReservasPorHotel(IN hotel_id INT)
BEGIN

    SELECT R.id, R.habitacion, R.usuario, R.llegada, R.salida, R.noches, R.total, R.estado
    ,(SELECT count(HU.id)
		FROM huespedes HU
        WHERE HU.reserva = R.id) AS cant_huespedes
    FROM Reservas R
    INNER JOIN Habitaciones H ON R.habitacion = H.id
    INNER JOIN hoteles T ON T.id = H.hotel
    WHERE T.id = hotel_id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertarReserva(
    IN p_habitacion INT,
    IN p_usuario INT,
    IN p_llegada DATETIME,
    IN p_salida DATETIME,
    IN p_noches INT,
    IN p_total INT,
    IN p_estado SMALLINT
)
BEGIN
    declare total_reserva INT;
    
    Select ((costo_base + impuestos)* p_noches) into total_reserva
    from habitaciones 
    where id = p_habitacion;

    INSERT INTO Reservas (habitacion, usuario, llegada, salida, noches, total, estado)
    VALUES (p_habitacion, p_usuario, p_llegada, p_salida, p_noches, total_reserva, p_estado);
    
    SELECT id, habitacion, usuario, llegada, salida, noches, total, estado FROM Reservas WHERE id = LAST_INSERT_ID();



END;
//
DELIMITER ;


DELIMITER //

CREATE PROCEDURE InsertarHuesped(
    IN p_nombres VARCHAR(100),
    IN p_apellidos VARCHAR(100),
    IN p_documento VARCHAR(100),
    IN p_edad VARCHAR(100),
    IN p_reserva INT,
    IN p_fecha_nacimiento DATETIME,
    IN p_genero VARCHAR(1),
    IN p_tipo_documento SMALLINT,
    IN p_email VARCHAR(50),
    IN p_telefono VARCHAR(11)
)
BEGIN
    INSERT INTO Huespedes (nombres, apellidos, documento, edad, reserva, fecha_nacimiento, genero, tipo_documento, email, telefono)
    VALUES (p_nombres, p_apellidos, p_documento, p_edad, p_reserva, p_fecha_nacimiento, p_genero, p_tipo_documento, p_email, p_telefono);
	
    select * from huespedes where reserva = p_reserva;

END;
//
DELIMITER ;


DELIMITER //

CREATE PROCEDURE InsertarHotel(
    IN p_nombre VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_ciudad VARCHAR(100),
    IN p_telefono VARCHAR(11),
    IN p_habilitado BOOLEAN
)
BEGIN
    INSERT INTO Hoteles (nombre, direccion, ciudad, telefono, habilitado)
    VALUES (p_nombre, p_direccion, p_ciudad, p_telefono, p_habilitado);
END;
//
DELIMITER ;

DELIMITER //

CREATE PROCEDURE InsertarHabitacion(
    IN p_hotel INT,
    IN p_cantidad_personas INT,
    IN p_costo_base INT,
    IN p_impuestos double,
    IN p_tipo_habitacion INT,
    IN p_piso SMALLINT,
    IN p_tipo_vista INT
)
BEGIN
    INSERT INTO Habitaciones (hotel, cantidad_personas, costo_base, impuestos, tipo_habitacion, piso, tipo_vista, habilitado)
    VALUES (p_hotel, p_cantidad_personas, p_costo_base, p_impuestos, p_tipo_habitacion, p_piso, p_tipo_vista, 1);
END;
//
DELIMITER ;


DELIMITER //

CREATE PROCEDURE InsertarContactoEmergencia(
    IN p_nombres VARCHAR(100),
    IN p_apellidos VARCHAR(100),
    IN p_reserva INT,
    IN p_telefono VARCHAR(11)
)
BEGIN
    INSERT INTO ContactosEmergencia (nombres, apellidos, reserva, telefono)
    VALUES (p_nombres, p_apellidos, p_reserva, p_telefono);
    
    select nombres, apellidos, reserva, telefono from contactosemergencia where reserva = p_reserva;
    
END;
//
DELIMITER ;


DELIMITER //

CREATE PROCEDURE HuespedesByReservaId(IN reserva_id INT)
BEGIN
    SELECT nombres, apellidos, documento, edad, reserva, fecha_nacimiento, genero, tipo_documento, email, telefono
    FROM huespedes
    WHERE reserva = reserva_id;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE HotelesByCity(IN p_ciudad varchar(50))
BEGIN
    SELECT id, nombre, direccion, ciudad, telefono, habilitado
    FROM hoteles
    WHERE ciudad like CONCAT("%", p_ciudad, "%") and habilitado = 1;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE HabitacionesAvailableByHotel(
IN p_ciudad VARCHAR(50), 
IN p_cantidad_personas INT,
IN p_fecha_entrada DATETIME,
IN p_fecha_salida DATETIME
)
BEGIN
    SELECT 
    h.id AS hotel_id,
    h.nombre AS nombre_hotel,
    h.direccion AS direccion_hotel,
    h.ciudad AS ciudad_hotel,
    h.telefono AS telefono_hotel, 
    ha.id AS habitacion_id,
    ha.cantidad_personas AS capacidad_habitacion,
    ha.costo_base,
    ha.piso,
    tv.descripcion AS tipo_vista
FROM 
    Hoteles h
INNER JOIN 
    Habitaciones ha ON h.id = ha.hotel
LEFT JOIN 
	tiposvista tv ON tv.id = ha.tipo_vista
WHERE 
    h.ciudad LIKE concat("%", p_ciudad, "%")
    AND h.habilitado = TRUE
    AND ha.cantidad_personas >= p_cantidad_personas
    AND ha.habilitado = TRUE
    AND NOT EXISTS (
        SELECT 1
        FROM Reservas r
        WHERE ha.id = r.habitacion
        AND (
            (p_fecha_entrada BETWEEN r.llegada AND r.salida)
            OR (p_fecha_salida BETWEEN r.llegada AND r.salida)
            OR (r.llegada BETWEEN p_fecha_entrada AND p_fecha_salida)
        )
    );
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE ContEmergenciaByReservaId(IN reserva_id INT)
BEGIN
    SELECT nombres, apellidos, reserva, telefono
    FROM contactosemergencia
    WHERE reserva = reserva_id;
END //

DELIMITER ;

DELIMITER //

CREATE PROCEDURE ActualizarHotel(
    IN p_id INT,
    IN p_nombre VARCHAR(100),
    IN p_direccion VARCHAR(100),
    IN p_ciudad VARCHAR(100),
    IN p_telefono VARCHAR(11),
    IN p_habilitado BOOLEAN
)
BEGIN
    UPDATE Hoteles
    SET
        nombre = p_nombre,
        direccion = p_direccion,
        ciudad = p_ciudad,
        telefono = p_telefono,
        habilitado = p_habilitado
    WHERE
        id = p_id;
END;
//
DELIMITER ;

DELIMITER //

CREATE PROCEDURE ActualizarHabitacion(
    IN p_id INT,
    IN p_hotel INT,
    IN p_cantidad_personas INT,
    IN p_costo_base INT,
    IN p_impuestos INT,
    IN p_tipo_habitacion INT,
    IN p_piso SMALLINT,
    IN p_tipo_vista INT
)
BEGIN
    UPDATE Habitaciones
    SET
        hotel = p_hotel,
        cantidad_personas = p_cantidad_personas,
        costo_base = p_costo_base,
        impuestos = p_impuestos,
        tipo_habitacion = p_tipo_habitacion,
        piso = p_piso,
        tipo_vista = p_tipo_vista
    WHERE
        id = p_id;
END;
//
DELIMITER ;

DELIMITER //

CREATE PROCEDURE ActualizarEstadoHotel(
    IN p_id INT
)
BEGIN
	declare p_habilitado boolean;
	select habilitado into p_habilitado from Hoteles where id = p_id;
	
    
    UPDATE Hoteles
    SET
        habilitado = Not p_habilitado
    WHERE
        id = p_id;
END;
//
DELIMITER ;


DELIMITER //

CREATE PROCEDURE ActualizarEstadoHabitacion(
    IN p_id INT
)
BEGIN
	declare p_habilitado boolean;
	select habilitado into p_habilitado from Habitaciones where id = p_id;
	
    
    UPDATE Habitaciones
    SET
        habilitado = Not p_habilitado
    WHERE
        id = p_id;
END;
//
DELIMITER ;



