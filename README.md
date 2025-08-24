#Despues de clonar el repositorio 
# 1. clic en tools
# 2. clic en NuGet Package Manager
# 3. clic en Package Manager Console 
# 4. pegar el siguiente comando para reinstalar paquete Roslyn:

   Install-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform
   
# 5. reconstriur el proyecto en Built/ReBuilt Solution
# 6. Ya estas listo para ejecutar...

------------------------------------------------------------------

# Restauración de la base de datos

Este proyecto utiliza una base de datos llamada `EmpleadosReutilizacion`. Para que la aplicación funcione correctamente, es necesario restaurar esta base en tu instancia local de SQL Server utilizando el archivo de scripts incluido en el repositorio.

### Ubicación del archivo de scripts

El archivo con todos los scripts necesarios se encuentra en el mismo nivel que la carpeta `AplicacionNomina` y el archivo `README.md`. Dentro de este archivo se incluyen:

- Script de creación de la base de datos `EmpleadosReutilizacion`
- Creación de tablas
- Procedimientos almacenados
- Triggers
- Inserts de prueba

### Pasos para restaurar la base de datos

1. **Abrir SQL Server Management Studio (SSMS)**  
   Inicia sesión en tu instancia local de SQL Server.

2. **Ejecutar el script completo**  
   Abre el archivo de scripts desde SSMS y ejecútalo. Esto creará la base de datos `EmpleadosReutilizacion` con toda su estructura y datos de prueba.

3. **Verificar que la base se haya creado correctamente**  
   Asegúrate de que la base `EmpleadosReutilizacion` aparezca en el explorador de objetos de SSMS y que contenga las tablas, procedimientos y datos esperados.

4. **Configurar la cadena de conexión en Web.config**  
   En el archivo `Web.config`, asegúrate de que la cadena de conexión apunte a tu servidor local y a la base `EmpleadosReutilizacion`. Revisar la siguiente sección para más detalles sobre esta configuración.

### Recomendaciones

- Si tu servidor SQL requiere autenticación de Windows, ajusta la cadena de conexión usando `Integrated Security=True`.
- Si el script incluye instrucciones de `DROP DATABASE`, asegúrate de no tener conexiones activas a la base antes de ejecutarlo.
- Si el script está dividido en secciones, ejecútalas en orden para evitar errores de dependencias.

-------------------------------------------------------------------

#Configuración de la cadena de conexión en Web.config

Después de clonar el repositorio, cada desarrollador debe configurar la cadena de conexión en el archivo `Web.config` para que la aplicación pueda conectarse correctamente a la base de datos.

### Paso 1: Abrir el archivo Web.config

Ubica el archivo `Web.config` en la raíz del proyecto. Ábrelo y busca la sección `<connectionStrings>`. Si no existe, agrégala justo debajo de la línea `<configuration>`.

### Paso 2: Agregar la cadena de conexión

Cada persona debe colocar el nombre de su propio servidor SQL en el campo `Server`. La base de datos utilizada es la misma para todos: `EmpleadosReutilizacion`.

Ejemplo de configuración:

```xml
<connectionStrings>
  <add name="con" 
       connectionString="Server=TU_SERVIDOR_SQL;Database=EmpleadosReutilizacion;User ID=TU_USUARIO_SQL;Password=TU_CONTRASEÑA_SQL" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Notas importantes

- Reemplaza `TU_SERVIDOR_SQL` con el nombre de tu instancia de SQL Server. Por ejemplo: `LAPTOP-ABC123\SQLEXPRESS` o `.\SQLEXPRESS`.
- Reemplaza `TU_USUARIO_SQL` y `TU_CONTRASEÑA_SQL` con tus credenciales de acceso.
- Si utilizas autenticación de Windows, puedes usar `Integrated Security=True` en lugar de `User ID` y `Password`:

  ```xml
  connectionString="Server=TU_SERVIDOR_SQL;Database=EmpleadosReutilizacion;Integrated Security=True"
  ```

