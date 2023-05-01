# Bugs with fixes


1. To fix Laravel error when starting the project.

Remove the semicolon before the next line in the php.ini file (uncomment) - ;extension=fileinfo


1. "Target class controller does not exist - Laravel" - error.

Correction:
```
use App\Http\Controllers\UserController;

Route::get('/users', [UserController::class, 'index']);
// or
Route::get('/users', 'App\Http\Controllers\UserController@index');
```


# Artisan commands

php artisan route:list - will display a list of routes with names and methods.

php artisan serve - server start

php artisan migrate - migrate migrations to db

php artisan migration:rollback - remove migrations from database

