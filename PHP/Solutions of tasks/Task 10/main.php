<?php 

if($_POST['first']) { $first = strip_tags( $_POST['first'] ); }

if($_POST['second']) { $second = strip_tags( $_POST['second'] ); }

if($_POST['send']) 
{
    if($_POST['taskOption']) 
    { 
        if($_POST['taskOption'] == 'plus') 
        { 
            $result = 'Результат сложения :<br>'. ($first + $second);
        }

        else if($_POST['taskOption'] == 'minus')
        { 
            $result = 'Результат минусования :<br>'.($first - $second);
        }

        else if($_POST['taskOption'] == 'multiply')
        { 
            $result = 'Результат умножения :<br>'. $first * $second;
        }

        else if($_POST['taskOption'] == 'divide')
        { 
            if ($second != 0) $result = 'Результат деления:<br>'. $first / $second;
            
            else $result = 'Делить на ноль нельзя <br>';
        }
    }
    
}

?>