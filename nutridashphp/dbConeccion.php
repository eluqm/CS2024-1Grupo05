<?php

try{
    //Cadena de conexión, nombre de usuario y contraseña
    $pdo = new PDO('mysql:host=localhost;dbname=pruebadash', 'admin', '1234');
    // Configuración del modo de error de PDO para lanzar excepciones
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $pdo->exec('SET NAMES "utf8"');
}catch(PDOException $e){
    echo "ERROR CONECTING TO DATABASE". $e->getMessage();
    exit();
}   

?>
