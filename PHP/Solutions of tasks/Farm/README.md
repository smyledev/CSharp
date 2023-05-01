# Village life simulator on the OOP

In the summer, to take a break from the bustle of the city, you went to your uncle's farm. After a few days of rest you got bored, and you decided to practice programming. Having entered a barn where cows and chickens live, and having seen how an automatic milk and egg collector works, you decided to describe its work in the OOP paradigm.

Given:
There are 10 cows and 20 chickens in the barn;
A cow can give 8-12 liters of milk per milking;
A hen can lay 0-1 eggs per clutch;
Each cow and chicken on the farm has a unique registration number.

## Task

To implement, using php, an object-oriented system: a prototype for collecting products. Implement the class structure, use the inheritance mechanism.

The Farm class should be autonomous, keeping records of animal numbers, collecting and counting products. The solution should be extensible, adding new types of animals and products should not lead to modification of the Farm class itself.

The system must be able to:
Add animals to the barn one by one.
Collect products from all animals registered in the barn.
Calculate the total number of collected products.

The task is designed to test the skills of working with OOP.

The method of initialization of the list of animals is up to you.

When running the main.php script in the console:
The system should add animals to the barn (10 cows and 20 chickens).
Display information about the number of each type of animal on the farm.
Collect products 7 times (a week) (milk cows and collect eggs from chickens).
Display the total number of pieces collected per week. eggs and liters of milk.
Add 5 more chickens and 1 cow to the farm (we went to the market, bought animals).
Display information about the number of each type of animal on the farm again.
Again 7 times (a week) we collect products and display the result on the screen.