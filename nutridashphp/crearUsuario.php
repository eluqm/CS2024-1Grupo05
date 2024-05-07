<?php
// Inclusión del archivo que contiene la configuración de la conexión a la base de datos
include "dbConeccion.php";

// Recuperación del nombre de usuario enviado a través de POST
$nombre = $_POST['nombre'];
$contrasena = hash('sha256', $_POST['contrasena']);

// Uso de consulta preparada para evitar inyecciones SQL
$sql = "SELECT nombre FROM jugadores WHERE nombre = :nombre";
$stmt = $pdo->prepare($sql);
$stmt->execute(['nombre' => $nombre]);

// Verificación de si se encontró algún resultado
if($stmt->rowCount() > 0)
{
    // Si el nombre de usuario ya existe, se envía una respuesta indicando el error
    $data = array('done' => false , 'message' => "Error, nombre de usuario ya existe.");
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
}
else
{
    // Si el nombre de usuario no existe, se procede a insertar el nuevo registro
    $sql = "INSERT INTO jugadores SET nombre = :nombre, contrasena = :contrasena";
    $stmt = $pdo->prepare($sql);
    $stmt->execute(['nombre' => $nombre, 'contrasena' => $contrasena]);

     // Se envía una respuesta indicando el éxito del registro
    $data = array('done' => true , 'message' => "Registro exitoso.");
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
}
?>
