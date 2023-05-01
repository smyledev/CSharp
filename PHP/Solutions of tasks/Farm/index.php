<?php

require 'Farm.php';

$farm = new Farm(10, 20);

$farm->printAmountOfAnimals(); 

for ($i = 1; $i <= 7; $i++) {
    $farm->getProductsFromAnimals();   
}

$farm->printTotalAmountOfProducts();   
    
$farm->addChickens(5);

$farm->addCows(1);

$farm->printAmountOfAnimals(); 

for ($i = 1; $i <= 7; $i++) {
    $farm->getProductsFromAnimals();   
}

$farm->printTotalAmountOfProducts(); 

?>