<em> Hotel API </em>

## Introducción

Este proyecto tiene como fin exponer API's para una empresa turistica y poder gestionar hoteles, habitaciones y reservas.

## Instalación
Para poder usar este proyecto, sigue estos pasos:
1. Clona el proyecto en tu maquina

2. Dentro de la carpeta SQL estan 2 Archivos; primero ejecuta el archivo 1.ScriptCreateDB y segundo ejecuta el archivo 2.ScriptsProcedureStore esto se debe hacer sobre el motor Mysql.

3. Abre la solucion HotelWeb.Api.Web que esta dentro de la carpeta Hotel.Api.Web

4. Una vez abierto el proyecto en el archivo appsettings.json configura las credenciales de la coneccion MySqlConnection

## Como Probar las API's

1. Ejecuta el proyecto

2. Este proyecto tiene integrado Swagger por lo que podras ver las apis documentadas y listas para usar

3. Utiliza la API de api/Login/Login (cuando ejecutaste los scripts sql se crearon unos usuarios )
    sin embargo aqui tienes 2 que puedes usar: (Admin) 10102020 - Clave123,  (Cliente) 1004501091 - Clave123

    o ejecuta esta consulta en la DB para que crees tu usuario tipo cliente y puedes ver como llega el correo despues de hacer una reserva ->>>> INSERT INTO usuarios (`id`, `nombres`, `apellidos`, `email`, `documento`, `tipo_documento`, `tipo_usuario`, `contrasena`) VALUES ('1111111', 'nombre', 'apellido', 'miCorreo@gmail.com', '1111111', '1', '3', 'miClave123');


4. Una vez obtienes el token del login lo vas a copiar en el autorize de swagger (Recuerda que las apis que puede usar un usuario tipo cliente son /api/Habitacion/HabitacionAvailableByHotel y /api/Reserva/Reserva)

![Authorizador swagger](https://github.com/KewuinYate6Dev/HoteAPI/blob/main/Imagenes/authorize.png)

5. ya puedes comenzar a utilizar las API's

