<!-- task 2  -->

<?php

abstract class AbstractClass
{
    abstract public function save();

    abstract public function new($prefix);

    public function getName($input, $strength = 16) 
    {
        $input_length = strlen($input);
        $random_string = '';
        for($i = 0; $i < $strength; $i++) 
        {
            $random_character = $input[mt_rand(0, $input_length - 1)];
            $random_string .= $random_character;
        }

        return $random_string;
    }   
    
}

class ConcreteClass extends AbstractClass
{
    public function save() 
    {
        return "Save";
    }

    public function new($prefix) 
    {
        return $prefix;
    }

    public function getName($input, $strength = 16) 
    {
        $strRandom = parent::getName($input, $strength);
        return  "$strRandom Здорово!";
    }
}


$ConClass = new ConcreteClass();

$strName = "Ivan"; 

$permitted_chars = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';

$resultSave = $ConClass->save();
$resultNew = $ConClass->new($strName);
$resultGetName = $ConClass->getName($permitted_chars, 10);

echo nl2br("
    $resultSave \n
    $resultNew \n
    $resultGetName \n");

?>