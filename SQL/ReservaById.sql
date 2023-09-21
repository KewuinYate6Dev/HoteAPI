DELIMITER //

CREATE PROCEDURE ReservaById(IN reserva_id INT)
BEGIN

    SELECT R.id, R.habitacion, R.usuario, R.llegada, R.salida, R.noches, R.total, R.estado,
    H.hotel, H.cantidad_personas, H.costo_base, H.impuestos, H.tipo_habitacion, H.piso, H.tipo_vista,
    T.nombre, T.direccion, T.ciudad, T.telefono
    FROM Reservas R
    INNER JOIN Habitaciones H ON R.habitacion = H.id
    INNER JOIN hoteles T ON T.id = H.hotel
    WHERE R.id = reserva_id;
    
    
END //

DELIMITER ;