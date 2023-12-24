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
cd Beneficiary.Service
Update-Database

# Para Carrier.Service
cd ../Carrier.Service
Update-Database
```

## Estructura del Proyecto
1. Beneficiary.Service: Microservicio encargado de gestionar los beneficiarios.
2. Carrier.Service: Microservicio encargado de gestionar los transportistas.
