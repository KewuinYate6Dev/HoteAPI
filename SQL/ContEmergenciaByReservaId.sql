DELIMITER //

CREATE PROCEDURE ContEmergenciaByReservaId(IN reserva_id INT)
BEGIN
    SELECT nombres, apellidos, reserva, telefono
    FROM contactosemergencia
    WHERE reserva = reserva_id;
END //

DELIMITER ;