Wampserver database setup:
run wampserver64 and open phpmyadmin
create the database, name it roofjumper

create the scores table:
CREATE TABLE scores (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL,
    score INT UNSIGNED NOT NULL,
    time INT UNSIGNED NOT NULL
);

create RoofJumperGame folder - wamp64/www/RoofJumperGame
create 2 files - display.php and addscore.php (you can access the data inside by looking in the included PHP folder)
