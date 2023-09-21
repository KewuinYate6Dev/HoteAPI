DELIMITER //

CREATE PROCEDURE UsuarioById(
IN p_usuario INT
)
BEGIN
    SELECT id, nombres, apellidos, email, documento, tipo_usuario
    FROM usuarios
    WHERE id = p_usuario;
END //

DELIMITER ;