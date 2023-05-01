<?php 
  $file = file("count.txt");
  $count = implode("", $file);
  $count++;
  $myfile = fopen("count.txt","w");
  fputs($myfile,$count);
  fclose($myfile);
?>

Просмотров: 
<?= $count; ?> 
<img src="image.php"/>