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