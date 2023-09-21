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
