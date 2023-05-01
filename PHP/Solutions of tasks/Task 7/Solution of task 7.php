<?php 

$array = range(1, 1000);
$array2 = range(1, 10);

foreach($array as $item){
		foreach($array2 as $key){
			if(substr_count($item, $key) > 1) {
				$key_bad = array_search($item, $array);
					unset($array[$key_bad]);
			}
		}	
	}

$sum=0;

// foreach ($array as $number) {
//   echo $number . '<br>';
// }

foreach($array as $value) {
  $sum = $sum + $value;
}

echo $sum;

?>