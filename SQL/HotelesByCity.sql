DELIMITER //

CREATE PROCEDURE HotelesByCity(IN p_ciudad varchar(50))
BEGIN
    SELECT id, nombre, direccion, ciudad, telefono, habilitado
    FROM hoteles
    WHERE ciudad like CONCAT("%", p_ciudad, "%") and habilitado = 1;
END //

DELIMITER ;