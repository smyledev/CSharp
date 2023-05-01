<!-- task 3  -->

<!DOCTYPE html>
<html lang="ru">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Task 4</title>
</head>

<body>
  <form action="file.php" method="POST">
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

  <?php
  if(!empty($_POST["name"]) && !empty($_POST["surname"])) 
  {
    $name = (string) $_POST["name"]; 
    $surname = (string) $_POST["surname"];

    $host = '127.0.0.1';
    $db   = 'test';
    $user = 'root';
    $pass = '';
    $charset = 'utf8';

    $dsn = "mysql:host=$host;dbname=$db;charset=$charset";
    $opt = [
      PDO::ATTR_ERRMODE            => PDO::ERRMODE_EXCEPTION,
      PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
      PDO::ATTR_EMULATE_PREPARES   => false,
    ];
    $db = new PDO($dsn, $user, $pass, $opt);

    $stmt = $db->prepare('INSERT INTO users (Name, Surname) VALUES (?, ?)');
    $stmt->execute($name, $surname);

  }
?>

</body>
</html>