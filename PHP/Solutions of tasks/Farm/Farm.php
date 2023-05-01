<?php

require 'Cow.php';
require 'Chicken.php';

class Farm
{
  private $cows = [];
  private $chickens = [];
  private $amountOfEggs;
  private $amountOfMilk;

  // Constructor, adding cows and chickens 
  // and initialising products...
  public function __construct($countOfCows, $countOfChickens)
  {
    $this->amountOfEggs = 0;
    $this->amountOfMilk = 0;

    $this->cows = array_fill(0, $countOfCows, new Cow);        
    $this->chickens = array_fill(0, $countOfChickens, new Chicken);
   
  }

  // Adding cows...
  public function addCows($countOfCows) 
  {
    $this->cows = array_fill(end(array_keys($this->cows)), $countOfCows + count($this->cows), new Cow);  
   
  }

  // Adding chickens...
  public function addChickens($countOfChickens) 
  {
    $this->chickens = array_fill(end(array_keys($this->chickens)), $countOfChickens + count($this->chickens), new Chicken);
   
  }

  // Getting products and saving it value...
  public function getProductsFromAnimals() 
  {
    $cows = $this->cows;
    $chickens = $this->chickens;

    foreach ($cows as $cow) {
      $this->amountOfMilk += $cow->getProducts();
    }   

    foreach ($chickens as $chicken) {
      $this->amountOfEggs += $chicken->getProducts();
    }
    
  }

  public function getAmountOfEggs() 
  {
    return $amountOfEggs;
  }

  public function getAmountOfMilk() 
  {
    return $amountOfMilk;
  }

  public function printTotalAmountOfProducts() 
  {
    echo "Litres of milk = $this->amountOfMilk <br>";
    echo "Count of eggs = $this->amountOfEggs <br><br>";
  }
  
  public function printAmountOfAnimals() 
  {
    echo "Count of cows = " . count($this->cows) . "<br>";
    echo "Count of chickens = " .  count($this->chickens) . "<br><br>";
  }

}
?>