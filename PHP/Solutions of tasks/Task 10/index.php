<?php require('main.php'); ?>

<!DOCTYPE html>
<html lang="ru">
    <head>
        <meta charset="UTF-8">
        <link rel="stylesheet" href="style.css">
    </head>

    <body>
        <a name="result"></a>
        <form method="POST" action="#result" class="POST_CALC_1 ">
            <input type="number" name="first" required><br>
            
            <select name="taskOption">
                <option value="plus">+</option>
                <option value="minus">-</option>
                <option value="multiply">*</option>
                <option value="divide">/</option>
            </select>
            
            <input type="number" name="second" required><br>

            <input type="submit" name="send" value="Посчитать">
            
            <div><? echo $result; ?></div>
        </form>
    </body>
</html>