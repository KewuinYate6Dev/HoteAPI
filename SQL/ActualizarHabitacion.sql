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