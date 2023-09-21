DELIMITER //

CREATE PROCEDURE InsertarContactoEmergencia(
    IN p_nombres VARCHAR(100),
    IN p_apellidos VARCHAR(100),
    IN p_reserva INT,
    IN p_telefono VARCHAR(11)
)
BEGIN
    INSERT INTO ContactosEmergencia (nombres, apellidos, reserva, telefono)
    VALUES (p_nombres, p_apellidos, p_reserva, p_telefono);
    
    select nombres, apellidos, reserva, telefono from contactosemergencia where reserva = p_reserva;
    
END;
//
DELIMITER ;
