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