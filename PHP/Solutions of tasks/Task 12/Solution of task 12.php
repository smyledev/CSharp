<?php 

$secret = '3810';

$guess  = ['0856', '2679', '1234', '5678', '0183', '3801', '3810'];

foreach ($guess as $number) {
    $matches = 0;
    $exactly = 0;
 
    for ($i = 0, $length = strlen($secret); $i < $length; $i++) {
        $position = strpos($secret, $number[$i]);
 
        if ($position !== false) {
            $matches++;
        }
 
        if ($position === $i) {
            $exactly++;
        }
    }
 
    echo 'Число ' . $number . ': угадано ' . $matches . ', из них на своём месте - ' . $exactly . '<br>';
}

?> 