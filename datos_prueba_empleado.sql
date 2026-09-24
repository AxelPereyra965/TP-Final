/* Empleado de prueba para los casos 7 y 8 del Overview.
   Hash y salt son un relleno: este usuario NO puede iniciar sesión,
   solo sirve para verificar que la tarjeta "Empleados activos" cuenta bien. */
USE ParkControl;

-- Caso 7: la tarjeta debe pasar de 0 a 1
INSERT INTO dbo.Usuarios (Nombre, Apellido, Email, ContraseniaHash, ContraseniaSalt, IdRol, Activo)
VALUES (N'Empleado', N'De Prueba', N'empleado.prueba@parkcontrol.com', 0x00, 0x00, 2, 1);

-- Caso 8: baja lógica, la tarjeta debe volver a 0
-- UPDATE dbo.Usuarios SET Activo = 0 WHERE Email = 'empleado.prueba@parkcontrol.com';

-- Limpieza al terminar
-- DELETE FROM dbo.Usuarios WHERE Email = 'empleado.prueba@parkcontrol.com';
