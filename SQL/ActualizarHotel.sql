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