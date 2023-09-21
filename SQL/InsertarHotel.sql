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