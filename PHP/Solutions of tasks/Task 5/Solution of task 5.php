<?php 

/* ===== Ваш код ниже ===== */

echo '=== Авторы ===<br>';
// Ваш код здесь

foreach ($data['authors'] as $key => $value) {
  echo $value['name'] . ' - ' . $value['email'] . ' - ' . $value['birthYear'] . '<br>';  
}

echo '<br>';

echo '=== Книги ===<br>';
// Ваш код здесь

foreach ($data['books'] as $book) {
  echo $book['title'] . ' - ' . $data['authors'][$book['author']]['name'] . ' - ' . $book['publishedAt'] . '<br>';  
}

?> 
