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