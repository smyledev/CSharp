<?php 

$data = [
  [
    'nickname' => 'Nick',
    'city' => 'Москва',
    'delivery' => 'Да',    
    'goods' =>    
    [
      [
      'name_of_product' => 'Бетон',
      'price' => '100',         
      ],

      [
      'name_of_product' => 'Герб',
      'price' => '150',         
      ],

    ],
  
  ],

  [
    'nickname' => 'Чебурашка',
    'city' => 'Челябинск',
    'delivery' => 'Нет',    
    'goods' => [],
  
  ],


  [
    'nickname' => 'Black',
    'city' => 'Казань',
    'delivery' => 'Нет',
    'goods' =>    
    [
      [
      'name_of_product' => 'Квадрат',
      'price' => '799',         
      ],

    ],
  
  ],

];


foreach ($data as $master) {
  if ($master['nickname'] == 'Nick') {
    foreach ($master['goods'] as $product) {
      echo 'Product: ' . $product['name_of_product'] . '; Price: ' . $product['price'] . '<br>';
    }
  }
    
}


?> 
