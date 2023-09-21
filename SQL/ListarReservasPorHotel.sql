DELIMITER //

CREATE PROCEDURE ListarReservasPorHotel(IN hotel_id INT)
BEGIN

    SELECT R.id, R.habitacion, R.usuario, R.llegada, R.salida, R.noches, R.total, R.estado
    ,(SELECT count(HU.id)
		FROM huespedes HU
        WHERE HU.reserva = R.id) AS cant_huespedes
    FROM Reservas R
    INNER JOIN Habitaciones H ON R.habitacion = H.id
    INNER JOIN hoteles T ON T.id = H.hotel
    WHERE T.id = hotel_id;
END //

DELIMITER ;