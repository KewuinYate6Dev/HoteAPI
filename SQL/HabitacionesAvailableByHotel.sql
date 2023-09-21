DELIMITER //

CREATE PROCEDURE HabitacionesAvailableByHotel(
IN p_ciudad VARCHAR(50), 
IN p_cantidad_personas INT,
IN p_fecha_entrada DATETIME,
IN p_fecha_salida DATETIME
)
BEGIN
    SELECT 
    h.id AS hotel_id,
    h.nombre AS nombre_hotel,
    h.direccion AS direccion_hotel,
    h.ciudad AS ciudad_hotel,
    h.telefono AS telefono_hotel, 
    ha.id AS habitacion_id,
    ha.cantidad_personas AS capacidad_habitacion,
    ha.costo_base,
    ha.piso,
    tv.descripcion AS tipo_vista
FROM 
    Hoteles h
INNER JOIN 
    Habitaciones ha ON h.id = ha.hotel
LEFT JOIN 
	tiposvista tv ON tv.id = ha.tipo_vista
WHERE 
    h.ciudad LIKE concat("%", p_ciudad, "%")
    AND h.habilitado = TRUE
    AND ha.cantidad_personas >= p_cantidad_personas
    AND ha.habilitado = TRUE
    AND NOT EXISTS (
        SELECT 1
        FROM Reservas r
        WHERE ha.id = r.habitacion
        AND (
            (p_fecha_entrada BETWEEN r.llegada AND r.salida)
            OR (p_fecha_salida BETWEEN r.llegada AND r.salida)
            OR (r.llegada BETWEEN p_fecha_entrada AND p_fecha_salida)
        )
    );
END //

DELIMITER ;