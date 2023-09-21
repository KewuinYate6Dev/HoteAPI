DELIMITER //

CREATE PROCEDURE HuespedesByReservaId(IN reserva_id INT)
BEGIN
    SELECT nombres, apellidos, documento, edad, reserva, fecha_nacimiento, genero, tipo_documento, email, telefono
    FROM huespedes
    WHERE reserva = reserva_id;
END //

DELIMITER ;