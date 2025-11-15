<?php
$hostname = 'localhost';
$username = 'root';
$password = '';
$database = 'roofjumper';

try {
    $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
} catch(PDOException $e) {
    echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
}

$mode = isset($_GET['mode']) ? $_GET['mode'] : 'highest';

if ($mode === 'fastest') {
    $sth = $dbh->query('SELECT * FROM scores ORDER BY time ASC LIMIT 5');
} else {
    $sth = $dbh->query('SELECT * FROM scores ORDER BY score DESC LIMIT 5');
}

$sth->setFetchMode(PDO::FETCH_ASSOC);
$result = $sth->fetchAll();

if (count($result) > 0) {
    foreach($result as $r) {
        echo $r['name'], "_";
        echo $r['score'], "_";
        echo $r['time'], "_";
    }
}
?>