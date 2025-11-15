<?php
$hostname = 'localhost';
$username = 'root';
$password = '';
$database = 'roofjumper';
$secretKey = "mySecretKey";
 
try 
{
	$dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, 
           $username, $password);
} 
catch(PDOException $e) 
{
	echo '<h1>An error has ocurred.</h1><pre>', $e->getMessage() 
            ,'</pre>';
}
 
$hash = $_GET['hash'];
$realHash = hash('sha256', $_GET['name'] . $_GET['score'] . $_GET['time'] . $secretKey);

if($realHash == $hash) 
{ 
    $sth = $dbh->prepare('INSERT INTO scores (name, score, time) VALUES (:name, :score, :time)');
    try 
    {
        $sth->bindParam(':name', $_GET['name'], PDO::PARAM_STR);
        $sth->bindParam(':score', $_GET['score'], PDO::PARAM_INT);
        $sth->bindParam(':time', $_GET['time'], PDO::PARAM_INT);
        $sth->execute();
    }
    catch(Exception $e) 
    {
        echo '<h1>An error has occurred.</h1><pre>', $e->getMessage() ,'</pre>';
    }
}
?>