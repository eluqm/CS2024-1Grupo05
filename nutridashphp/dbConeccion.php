<?php

try{
    //Cadena de conexión, nombre de usuario y contraseña
    $pdo = new PDO('mysql:host=localhost;dbname=pruebadash', 'admin', '1234');
    // Configuración del modo de error de PDO para lanzar excepciones
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    // Ejecución de una sentencia SQL directamente en la base de datos para configurar la codificación de caracteres
    $pdo->exec('SET NAMES "utf8"');
}catch(PDOException $e){
    // Captura de la excepción PDOException lanzada por un error en la base de datos
    // Imprime un mensaje de error personalizado seguido del mensaje de la excepción real
    echo "ERROR CONECTING TO DATABASE". $e->getMessage();
    exit();
}   

?>
