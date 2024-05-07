<?php
// Inclusión del archivo que contiene la configuración de la conexión a la base de datos
include "dbConeccion.php";

$nombre = $_POST['nombre'];
$contrasena = hash('sha256', $_POST['contrasena']);

// Uso de consulta preparada para evitar inyecciones SQL
$sql = "SELECT nombre FROM jugadores WHERE nombre = :nombre";
$stmt = $pdo->prepare($sql);
$stmt->execute(['nombre' => $nombre]);

if($stmt->rowCount() > 0)
{
    $data = array('done' => false , 'message' => "Error, nombre de usuario ya existe.");
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
}
else
{
    $sql = "INSERT INTO jugadores SET nombre = :nombre, contrasena = :contrasena";
    $stmt = $pdo->prepare($sql);
    $stmt->execute(['nombre' => $nombre, 'contrasena' => $contrasena]);

    $data = array('done' => true , 'message' => "Registro exitoso.");
    Header('Content-Type: application/json');
    echo json_encode($data);
    exit();
}
?>
