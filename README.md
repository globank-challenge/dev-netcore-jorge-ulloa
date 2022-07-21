# BankingOperationsAPI


#Instrucciones de instalación y despliegue 
#Cómo levantar la aplicación localmente
Instalar Docker (Docker for Windows si se despliega en Windows)
Descargar imagen de MSSQLServer de DockerHub (docker pull mcr.microsoft.com/mssql/server)
Levantar una instancia de la imagen
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MySQLP@ssw0rd" -p 1433:1433 --name dockermssql --hostname dockermssql -d mcr.microsoft.com/mssql/server

Ejecutar el script de creación de la BDD  BaseDatos.sql (puede ser desde SQL Management Studio o usando sqlcmd)
User SA Password MySQLP@ssw0rd

_En Visual  Studio
	Abrir la solución OperacionesBancariasAPI.sln
	Setear como startup project el proyecto de API que se desea ejecutar
		OpBancarias.Clientes.Api
		OpBancarias.Cuentas.Api
		OpBancarias.Movimientos.Api
	Ejecutar Run Seleccionando el proyecto API 
	
	Debe abrirse el Swagger en localhost
	
	Si se desea deployar localmente a Docker, ejecutar seleccionando Docker
	


Diagrama de arquitectura 

Tenemos 3 docker containers donde se ejecutan los microservicios de las web apis
Las web appi acceden a la BDD OpBancarias que está en un MSSQLServer dentro de un container

Tecnologías utilizadas 
Clean Arquitecture, Domain Driven Design, .Net Core 6, Dockers, MSSQL Server
