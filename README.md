# Aseguradores

Bienvenido al proyecto backend desarrollado en .NET Core que consta de dos microservicios: Beneficiary.Service y Carrier.Service. Este sistema utiliza Entity Framework Core para interactuar con la base de datos, y como parte del proceso de instalación, es necesario realizar algunas configuraciones.

## Instalación
```bash
git clone https://github.com/rdani2005/AseguradoresBackend.git
cd AseguradoresBackend
```
## 2. Configurar la Base de Datos
Modifique los archivos appsettings.json de cada microservicio (Beneficiary.Service y Carrier.Service) con la información de conexión a su base de datos. Asegúrese de proporcionar la cadena de conexión correcta en la sección "ConnectionStrings".

Ejemplo (Beneficiary.Service/appsettings.json):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=NombreDelServidor;Database=BeneficiaryDB;User=Usuario;Password=Contraseña;"
  },
  // ...
}

```

## 3. Actualizar la Base de Datos
```bash
# Para Beneficiary.Service

## Datos de cosas por hacer
En el codigo, tenemos que modificar el contacto entre las APIs, por el contrario a como estan, deberian de estar dentro del App Settings.

Adicional, se debe planificar en una forma de enviarlo a produccion, y que este sea optimo, a futuro, se podria adjuntar un DockerImage, o Kubernetes
cd Beneficiary.Service
Update-Database

# Para Carrier.Service
cd ../Carrier.Service
Update-Database
```

## Estructura del Proyecto
1. Beneficiary.Service: Microservicio encargado de gestionar los beneficiarios.
2. Carrier.Service: Microservicio encargado de gestionar los transportistas.


## Datos importantes
Por defecto, ambos microservicios van a correr en los puertos `localhost:5000` y `localhost:6000`. En Https estos van a correr en los puertos: `localhost:5001` y `localhost:6001`. Esto puede ser modificado en el archivo `Service/Properties/launchSettings.json`, correspondiente a ambos microservicios.

```json
...
  "profiles": {
    "Beneficiary.Service": {
      "commandName": "Project",
      "dotnetRunMessages": true,
      "launchBrowser": true,
      "launchUrl": "swagger",
      "applicationUrl": "https://localhost:6001;http://localhost:6000", // Aqui se pueden modificar los puertos
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
...
```
## Cambios a realizar
1. Se debe desarrollar un ambiente de Pruebas y de Produccion.
2. Se deberia poder poner las APIs a consultar en el App Settings, debido a que se estan consultando directamente, esto para comunicacion por HTTPS para los microservicios.
3. Incluir a futuro un bus de mensajes, para pasar a algo asincrono.
