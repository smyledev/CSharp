<?php 

$cars = [
    ['name' => 'Такси 1', 'position' => rand(0, 1000), 'isFree' => (bool) rand(0, 1)],
    ['name' => 'Такси 2', 'position' => rand(0, 1000), 'isFree' => (bool) rand(0, 1)],
    ['name' => 'Такси 3', 'position' => rand(0, 1000), 'isFree' => (bool) rand(0, 1)],
    ['name' => 'Такси 4', 'position' => rand(0, 1000), 'isFree' => (bool) rand(0, 1)],
    ['name' => 'Такси 5', 'position' => rand(0, 1000), 'isFree' => (bool) rand(0, 1)],
];


$passenger = rand(0, 1000);

$car_saved = [];
$cars_saved = [];

$name = [];
$position = [];
$distance = [];
$state = [];

foreach($cars as $car) {
  $name = $car['name'];
  $position = $car['position'];
  $distance = abs($position - $passenger);
  $state = $car['isFree'] ? '(свободен)' : '(занят)';
  $car_saved = [$name, $position, $distance, $state];
  array_push($cars_saved, $car_saved);
}

 $min;

for($i = 0; $i < count($cars_saved); $i++) {
  if($cars_saved[$i][3] === '(свободен)') {
    $min = $cars_saved[$i][2];
    break;
  }
}

for($i = 0; $i < count($cars_saved); $i++) {
  if($cars_saved[$i][3] === '(свободен)' and $min > $cars_saved[$i][2]) {
    $min = $cars_saved[$i][2];
  }
}


foreach($cars_saved as $car) {
  echo '"' . $car[0] . ', стоит на ' . $car[1] . ' км, до пассажира ' . $car[2] . ' км ' . $car[3]; 
  if ($min === $car[2]) echo ' - едет это такси';  
  echo '"<br>';
}

?> 
