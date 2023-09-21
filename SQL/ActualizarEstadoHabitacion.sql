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