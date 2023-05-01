<?php
// 1. A three-digit number is given. Find the sum of its digits.

$var = 578; // Declaring a number
$var .= ''; // Cast it to a string type
echo $a = $var[2] + $var[1] + $var[0]; // Output a variable equal to the sum of the digits of the number


// 2. Given a natural number 8. If it is even, then reduce it by 2 times,
// otherwise increase by 3 times.

// ceil - Rounds up a fraction
if ($number / 2 == ceil($number / 2))
{
$number = $number / 2;
}
else
{
$number = $number *3;
}

echo $number

// 3. Given a number. If it is not less than 50, then print the square of this number, if
// this number is greater than 10 and less than 30, then print zero, otherwise print
// the word "Error".

$a = 45; // Set the number $a, for example, 45
if ($a > 50)
{
   echo $b=pow($a,2);
}

elseif ($a > 10 & $a < 30)
{
   echo 0;
}

else
{
   echo "Error";
}

?>


<?php

// 4. The user selects a country from the drop-down list (Türkiye, Egypt or
// Italy) enters the number of days to stay and whether it has a discount
// (checkbox). Output the cost of the holiday, which is calculated as the product of
// number of days by 400. Further this number increases by 10% if Egypt is selected,
// and 12% if Italy is selected. And then this number is reduced by 5%, if specified
// discount.

?>

<html>
  <head>
    <meta charset="utf-8" />
    <title>Page title</title>
  </head>
  <body>

    <?php
  echo '<pre>';
     print_r($_POST);
     echo '</pre>';
    
     if (isset($_POST['hero']))
     {
       if($_POST['hero'] == 1)
       {
         if(isset($_POST['option']))
         {
           echo" to Italy with a discount - ". ($_POST['a']*400*1.12*0.95);}
         else
         {
           echo" to Italy without discount - ". ($_POST['a']*400*1.12);}
         }
       elseif( $_POST['hero'] == 2)
       {
         if(isset($_POST['option']))
         {
           echo" to Egypt at a discount - ". ($_POST['a']*400*1.1*0.95);
         }
         else
         {
           echo"Egypt without discount - ".($_POST['a']*400*1.1);
         }
       }
       elseif($_POST['hero'] == 3)
       {
         if(isset($_POST['option']))
         {
           echo" to Turkey with a discount - ". ($_POST['a']*400*0.95);
         }
         else
         {
           echo"Turkey without discount - ".($_POST['a']*400);
         }
       }
     }
    ?>

<form method="post">
       <p><select name="hero">
         <option>select a country from the list</option>
         <option value="1">Italy</option>
         <option value="2">Egypt</option>
         <option value="3">Turkey</option>
       </select></p>
      
       <p><b>how many days off?</b></p>
       <input type="text" name="a" />
      
       <p><b>Do you have a discount?</b></p>
       <input type="checkbox" name="option" value="b"><Br>
      
       <p><input type="submit" value="Submit"></p>
     </form>
   </body>
</html>


<?php

// 5. Given an array with the elements 'Hello, ', 'world' and '!'.
// You need to write the phrase 'Hello world!' to the $text variable,
// and then display the contents of this variable on the screen.

$arr = ['Hi ', 'world', '!'];
$text = $arr[0].$arr[1].$arr[2];

/*
The $text variable now contains the string 'Hello world!'.
Let's display it on the screen:
*/

echo $text;

$arr[0] = 'Bye, '; // overwrite the first element of the array
var_dump($arr); //look at the array and make sure it has changed


// 6. An array with numbers is given. Write in the new array only those numbers that contain the number 5.

function getFives($sArr, String $sInt)
{
   $result = [];
   foreach ($sArr as $item)
     // strval - convert to string
     // strpos - returns the position of the first occurrence of a substring
     if (strpos(strval($item), $sInt) !== false)
       $result[] = $item;
   return $result;
}

$searchInt = 5;
$searchArr = [2,1,4,3,5,7,6,9,8,11,10,15,29,25,52,13,51];

echo '<pre>';
var_dump(getFives($searchArr, $searchInt));
echo '</pre>';



// 7. Create an array like [1, [2], [[3]], [[[4]]] ], instead of 4 there can be any integer
// meaning. For example, if 5 - then there will be an array [1, [2], [[3]], [[[4]]], [[[[5]]]] ].

function getMultiArray($level) 
{
  $result = $level;
  
  for ($i = 1; $i < $level; $i++) 
  {
    $result = [$result];
  }
  
  return $result;
}

function strange($value) 
{
  $result = [];
  
  for ($i = 1; $i <= $value; $i++) 
  {
    $result[$i] = getMultiArray($i);
  }
  
  return $result;
}

print_r(strange(5));




// 8. Write a function that will merge two arrays in this way: from, to
// for example, [1, 2, 3] and ['a', 'b', 'c'] it will make [1, 'a', 2, 'b', 3, 'c'].
// Make a similar function that will take parameters
// not two arrays, but an arbitrary number (let the function take as a parameter
// two-dimensional array, where its sub-arrays are what we will be merging).

// ... - variable number of arguments
function array_strange_merge(...$arrays)
{
   $maxLength = 0;
   foreach ($arrays as $array)
   {
     $maxLength = max(count($array), $maxLength);
   }
  
   $result = [];
   for ($i = 0; $i < $maxLength; $i++)
   {
     foreach ($arrays as $array)
     {
       // isset - determines if the variable was set to a non-null value
       if (isset($array[$i]))
       {
         $result[] = $array[$i];
       }
     }
   }
  
   return $result;
}

print_r(array_strange_merge([1,2,3], ['a','b','c','d']));



// 9. Given an array of the form [1, ' , 2, ' , ' , 3] - that is, it contains empty strings. Delete everything
// such elements from this array.*

$unCleanList = [1, '' , 2, '', '', 3];
$filteredList = array_filter($unCleanList);
var_dump($filteredList);


// 10. Write a function that correctly adds hours and minutes.
// Examples: the following is given to the function input - 1h20min + 50min - as a result, the function
// prints 2h10min.

function h2s($h)
{
   // % is the integer remainder after dividing $a by $b.
   // division by 24 implements getting the number of hours on the output no more than 23
   $h = $h/3600% 24; // $h = 2 for the given set of initial data (7800 sec)
   $h = intdiv($h, 10) ? $h : "0" . $h;
   $h = $h == 0 ? $h = "00" : $h;
   return $h;
}

function m2s($m)
{
   // division by 60 implements getting the number of minutes at the output no more than 60
   $m = $m/60% 60; // m = 10 (130 - 60*2) for a given set of initial data (7800 sec)
   $m = intdiv($m, 10) ? $m : "0" . $m;
   $m = $m == 0 ? $m = "00" : $m;
   return $m;
}

function s($s)
{
   // division by 60 implements getting the number of seconds on the output no more than 60
   $s = $s % 60; // m = 0 for a given set of initial data (7800 sec)
   $s = intdiv($s, 10) ? $s : "0" . $s;
   $s = $s == 0 ? $s = "00" : $s;
   return $s;
}

function normal2seconds($normal)
{
   $hms = explode(":",$normal);
   $h = $hms[0]*3600;
   $m = $hms[1]*60;
   $s = $hms[2];
   return $h+$m+$s;
}

function seconds2normal($seconds)
{
   $v = max($seconds,0);
   $h = h2s($v);
   $m = m2s($v);
   $s = s($v);
   return "$h:$m:$s";
}

function timePlus()
{
   // func_get_args - returns an array containing the function's arguments
   $times = func_get_args();
   $time = 0;
   for ($i=0;$i<count($times);$i++)
   {
     // explode - splits a string using a delimiter and returns an array of strings
     // $nowtime = explode(":",$times[$i]);
     // $time += normal2seconds(" $nowtime[0]:$nowtime[1]:$nowtime[2]");

     $time += normal2seconds($times[$i]);
   }

   return seconds2normal($time);
}

130 min = 7800 sec
echo timePlus("01:20:00","00:50:00");



// 11. Create an array $arr, a =>1, b=>2, c=>3. Find the sum of the elements of this array.

$arr = ['a'=>1, 'b'=>2, 'c'=>3];
echo $arr['a'] + $arr['b'] + $arr['c'];


// 12. Create an associative array of days of the week. The keys in it should be
// numbers of days from the beginning of the week (Monday must have key 1, Tuesday must have key 2 and
// etc.). Print the day of the week corresponding to the value of the $day variable.

пусть текущий день – четверг, тогда:
$arr = [1=>'пн', 2=>'вт', 3=>'ср', 4=>'чт', 5=>'пт', 6=>'сб', 7=>'вс'];
$day = 4;
echo $arr[$day];


13. Дан многомерный массив
$arr = [
'sp'=>['azul', 'rojo', 'verde'],
'en'=>['blue', 'red', 'green'],
];
Выведите с его помощью слово 'azul' .

$arr = [
  'sp'=>['azul', 'rojo', 'verde'],
  'en'=>['blue', 'red', 'green'],
];

echo $arr['sp'][0]; //выведет 'azul'



// 14. Create a two-dimensional array. The first two keys are 'ru' and 'en' . Let the first
// the key contains an element that is an array of the names of the days of the week in Russian, and
// the second one is in English. Use this array to output Monday in Russian and
// Wednesday in English (let Monday be the first day).

$arr = [
  'ru'=>[1=>'пн', 'вт', 'ср', 'чт', 'пт', 'сб', 'вс'],
  'en'=>[1=>'mn', 'ts', 'wd', 'th', 'fr', 'st', 'sn'],
];

$ru = 'ru';
$en = 'en';
$day1 = 1;
$day2 = 3;

echo $arr[$ru][$day1]; //пн
echo "<br>";
echo $arr[$en][$day2]; //wd


// 15. Given a multidimensional array $arr. Write a function that accepts strings
// like 'string1.string2.string3' – letters separated by dots, and returns the element
// multidimensional array $arr['string1']['string2']['string3']. Number of dots per line
// can be anything, array nesting is also arbitrary, array keys do not contain dots.

function array_get($array, $key) 
{
  $keys = explode('.', $key);
  $result = $array;

  while ($k = array_shift($keys)) 
  {
    if (empty($result[$k])) 
    { 
      throw new Exception('element '.$key.' found'); 
    }
    $result = $result[$k];
  }
  
  return $result;
}

echo array_get([1,2,[1=>2, 2 => [1,5,3]]], '2.2.1');
echo array_get([1,2, 'table' => [1=>2, 'model' => [1, 'key' => 'cooool',3]]],
'table.model.key');



// 16. Given a multidimensional array. The nesting levels of its subarrays can be
// different. Display a column of all its elements.

$arr = array(
              array(1, 2, 3,), 
              array(
                     array(
                            4, 
                            5, 
                            array(6, 7, 8)
                          ), 
                     9, 
                     10,
                   ),
            );

function sort_array($arr)
{
  foreach($arr as $value)
  {
    if(!is_array($value))
    {
      echo $value."<br>";
    } 
    else
    {
      sort_array($value);
    }
  }
}

sort_array($arr);





// 17. The variable $num can take one of the values: 1, 2, 3, or 4. If it
// has a value of '1', then we will write 'winter' to the $result variable if it has a value of '2'
// – 'spring' and so on. Solve the problem through switch-case.

$num = 2;

switch ($num) 
{
  case 1:
    $result = 'зима';
    break;
  case 2:
    $result = 'весна';
    break;
  case 3:
    $result = 'лето';
    break;
  case 4:
    $result = 'осень';
    break;
}

echo $result;



// 18. The $year variable holds the year. Determine if it is a leap year (in
// this year is February 29). A year will be a leap year in two cases: either it is divisible
// by 4, but not divisible by 100, or divisible by 400. So, the years 1700, 1800 and
// 1900 are not leap years since they are divisible by 100 and not divisible by 400.
// The years 1600 and 2000 are leap years because they are divisible by 400.

$year = 2000;

if ($year % 4 == 0 or ($year % 400 == 0 and $year % 100 != 0)) 
{
  echo 'leap year';
} 
else 
{
  echo 'not a leap year';
}

?>



<!-- Preserve form field values after submit -->


<form action="" method="GET">
    <input name="user" value="<?php if(isset($_REQUEST['user'])) echo $_REQUEST['user']; ?>">
    <input type="submit">
</form>


<?php


/*
     The time() function will return the current time in timestamp format,
     and mktime is for the given date.

     Subtract the results from each other and get the difference in seconds:
*/
echo time() - mktime(12, 0, 0, 2, 1, 2000);


//Find out what day of the week was 29-12-2013:
echo date('w', mktime(0, 0, 0, 12, 29, 13)); //returns '0' - Sunday


// Let's create an object with a date for 2025, 12 months, 31 days,
// then add 3 days and 1 month to it and output in the format

$date = date_create('2025-12-31');
date_modify($date, '3 days 1 month'); // to subtract put a minus in front of the number
echo date_format($date, 'd.m.Y');


// Use a flag to check if there is a value in the array


$arr = ['a', 'b', 'c', 'd', 'с'];
$flag = false; //assume that there is no element 'c' in the array

foreach ($arr as $elem) {
  if ($elem == 'c') {
    $flag = true; //the element exists - redefine the $flag variable
    break;
  }
}

if ($flag === true) {
  echo 'Есть';
} else {
  echo 'Нет';
}


// Getting the pyramid of the following type:
// 1
// 22
// 333
// 4444
// 55555
// 666666
// 7777777
// 88888888
// 999999999

for ($i = 1; $i <= 9; $i++) {
    for ($j = 1; $j <= $i; $j++) {
      echo $i;
    }
    echo '<br>';
}


// Count the number of elements

$arr = ['a', 'b', 'c', 'a', 'a', 'b'];
$count = [];
  
foreach ($arr as $elem) {
  if (!isset($count[$elem])) {
    $count[$elem] = 1;
  } else {
    $count[$elem]++;
  }
}




// Task: let an array with numbers be given, it is necessary to write to the new array only those elements, the sum of the digits in which is from 1 to 9.

function getDigits($num) 
{
  return str_split($num, 1);
}

function arraySum($arr) 
{
  $sum = 0;
  foreach ($arr as $elem) {
    $sum += $elem;
  }

  return $sum;
}

function inRange($num) 
{
  $sum = arraySum(getDigits($num));
  return $sum >= 1 and $sum <= 9;
}

$arr = [12, 19, 28, 13, 14, 345];
$result = [];

foreach ($arr as $elem) {
  if (inRange($elem)) { //если подходит - берем
    $result[] = $elem;
  }
}

var_dump($result);

?>
