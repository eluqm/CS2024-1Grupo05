<?php

try{
    $pdo = new PDO('mysql:host=localhost;dbname=pruebadash', 'admin', '1234'); // Corregido aquí
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $pdo->exec('SET NAMES "utf8"');
}catch(PDOException $e){
    echo "ERROR CONECTING TO DATABASE". $e->getMessage();
    exit();
}   

?>