Cómo levantar la aplicación localmente
Instalación de la BDD
	En Docker containers
⦁	Instalar Docker (Docker for Windows si se despliega en Windows)
⦁	Descargar imagen de MSSQLServer de DockerHub (docker pull mcr.microsoft.com/mssql/server)
⦁	Levantar una instancia de la imagen
⦁	docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MySQLP@ssw0rd" -p 1433:1433 --name dockermssql --hostname dockermssql -d mcr.microsoft.com/mssql/server
⦁	Conectarse al SQL y ejecutar script de creación de BDD BaseDatos.sql
⦁	sqlcmd -S localhost,1433 -U SA -P "MySQLP@ssw0rd" i {scriptlocation}\BaseDatos.sql
Si se tiene una instancia de SQL server corriendo que se desea utilizar, solo es necesario ejecutar el script BaseDatos.sql en el mismo. Tener en cuenta que, en ese caso, sería necesario modificar la cadena de conexión a BDD en todos los archivos appsettings.json que hay en la solución (uno por cada proyecto API).

Ejecución de los API
En Windows server o workstation:
⦁	Abrir la solución en VS 2022. Compilar la solución.
⦁	Publicar a carpetas locales los API:
⦁	 OpBancarias.Usuarios.Api, OpBancarias.Cuentas.Api, OpBancarias.Clientes.Api, OpBancarias.Movimientos.Api
⦁	Abrir cuatro ventanas del CLI con privilegios elevados. Ejecutar en ellas (un comando en cada una):
⦁	dotnet OpBancarias.Usuarios.Api.dll
⦁	dotnet OpBancarias.Cuentas.Api.dll
⦁	dotnet OpBancarias.Clientes.Api.dll
⦁	dotnet OpBancarias.Movimientos.Api.dll

Ejecución de los API en Docker containers.
⦁	Los proyectos están configurados para desplegarse en Docker containers desde el IDE Visual Studio. Sin embargo, en las pruebas efectuadas no ha sido posible conectarse a los API a pesar de que aparentemente, al ver el log del container, está el servicio levantado y escuchando por el puerto correcto.
⦁	Para desplegar en containers desde el IDE VS:
⦁	Ejecutar los proyectos de API (OpBancarias.Usuarios.Api, pBancarias.Cuentas.Api, OpBancarias.Clientes.Api, OpBancarias.Movimientos.Api) seleccionando Docker como el destino de la compilación en modo "Start Without Debugging".

Ejecución de pruebas con  Postman
⦁	Abrir en el postman la collection OpBancarias.postman_collection.json
⦁	Ir al método UsuariosAPI/GetToken para obtener un token para el usuario jorge.ulloa (será válido por 10 minutos, al cabo de los cuales se debe volver a obtener el token).
⦁	Actualizar el valor de la variable BearerToken del entorno localhost con el token obtenido.
⦁	Ejecutar el resto de métodos.

Tecnologías utilizadas 
_ .NET Core 6.0
_ EF Core 6.0
_ MSSQL 2017 (docker image)
_ C#

Diagrama de Arquitectura
Se empleó una arquitectura de microservicios, con cuatro microservicios que se conectan a una BDD común. Los microservcios son Web API que deben ser consumidos por una aplicación cliente luego de autenticarse usando JWT.
 
