RedArborAPI
Implementación de API web con .NET 8 y DDD

Capas
RedArbor.Domain: contiene entidades, validaciones del dominio e interfaces de los repositorios.
RedArbor.Application: contiene todos los comandos y queries
RedArbor.Infrastructure: contiene la infraestructura de respositorio de datos y seguridad
RedArbor.API: el host de la API web.
RedArbor.Test: contiene clases de prueba unitaria basadas en el marco de prueba MsTest

Validación
Validación de datos con FluentValidation

Cómo ejecutar la aplicación:

Cree una base de datos vacía con el nombre test.
Ejecuta migraciones Update-database 
Establezca la cadena de conexión en el appsettings.json
