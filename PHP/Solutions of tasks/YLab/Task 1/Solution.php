<!-- task 1  -->

<!DOCTYPE html>
<html lang="ru">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Task 2</title>
</head>

<body>
  <form method="POST">
    <div class="form-group">
      <label>Имя</label>
      <input class="form-control" name="name" placeholder="Имя">  
    </div>
    
    <div class="form-group">
      <label>Фамилия</label>
      <input class="form-control" name="surname" placeholder="Фамилия">
    </div>

    <br>
    
    <button type="submit">Отправить</button>
  </form>

  <br>

  <?php
  if(!empty($_POST["name"]) && !empty($_POST["surname"])) 
  {
    $name = (string) $_POST["name"]; 
    $surname = (string) $_POST["surname"];

    echo "Привет, $surname $name";
  }
  ?>

</body>
</html>