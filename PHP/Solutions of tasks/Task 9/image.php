<?php
    // Создаём пустое изображение и добавляем текст
    $im = imagecreatetruecolor(120, 20);
    $text_color = imagecolorallocate($im, 233, 14, 91);
    imagestring($im, 1, 5, 5,  'A Simple Text String', $text_color);
    
    // Устанавливаем тип содержимого в заголовок, в данном случае image/jpeg
    header('Content-Type: image/jpeg');
    
    // Выводим изображение
    imagejpeg($im);
    
    // Освобождаем память
    imagedestroy($im);
?>
