<?php
    // Send variables for the MySQL database class.
	//Questi prima, con scoutgametest in locale
    $database = mysqli_connect('localhost', '67529', '4sP7eJ1BxjusZcgp') or die('Could not connect: ' . mysqli_error());
    mysqli_select_db($database, '67529') or die('Could not select database');
 
    //$query = "SELECT * FROM `scores` ORDER by `score` DESC LIMIT 5";
	$query = "SELECT * FROM `scores` ORDER by `score` DESC";
    $result = mysqli_query($database, $query) or die('Query failed: ' . mysqli_error());
 
    $num_results = mysqli_num_rows($result);  
 
    for($i = 1; $i <= $num_results  ; $i++)
    {
         $row = mysqli_fetch_array($result);
         echo "#" . $i . " <width=60%>"  . $row['name'] . "</width><pos=70%>" . $row['score'] . "\n";
		 //echo "<br />";
		 
    }
?>