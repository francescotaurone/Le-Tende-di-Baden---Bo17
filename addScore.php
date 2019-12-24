<?php 
        $db = mysqli_connect('localhost', '67529', '4sP7eJ1BxjusZcgp') or die('Could not connect: ' . mysql_error()); 
        mysqli_select_db($db, '67529') or die('Could not select database');
 
        // Strings must be escaped to prevent SQL injection attack. 
        //$name = mysqli_real_escape_string( $db, $_GET['name']); 
        //$score = mysqli_real_escape_string($_GET['score'], $db); 
        $name = mysqli_real_escape_string( $db, $_GET['name']); 
        $score = mysqli_real_escape_string($db, $_GET['score']); 
		
		$hash = $_GET['hash']; 
		
		
        $secretKey="chiaveSegreta"; # Change this value to match the value stored in the client javascript below 
		//questo sotto andrÃ  tolto
		$hash = md5($name . $score . $secretKey);
		
		
        $real_hash = md5($name . $score . $secretKey); 
        if($real_hash == $hash) { 
            // Send variables for the MySQL database class. 
            $query = "insert into scores values (NULL, '$name', '$score');"; 
            $result = mysqli_query($db, $query) or die('Query failed: ' . mysql_error()); 
        } 
?>