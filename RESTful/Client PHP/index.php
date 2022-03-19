<!DOCTYPE html>
<html lang="it">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Apache Php</title>


    <style>
        *{
            user-select: none;
        }

        body{
            background-color: #5D737E;
        }

        td, tr{
            border: 1px solid black;
            text-align: center;
            width: 200px;
            height: 30px;

            color: #040F16;
            background-color: #E7DCD0;
        }

        .bA{
            border: 1px solid black;
            text-align: center;
            width: 200px;
            height: 40px;
            
            font-weight: bold;
            font-size: 20px;

            color: #E7DCD0;
            background-color: #6D454C;
        }

    </style>


</head>
<body>
    
    <center>

    <?php

        $curlSES = curl_init(); 
        curl_setopt($curlSES, CURLOPT_URL, "http://10.205.1.189/tracks/");
        curl_setopt($curlSES, CURLOPT_RETURNTRANSFER, true);
        curl_setopt($curlSES, CURLOPT_CUSTOMREQUEST, "GET");
        curl_setopt($curlSES, CURLOPT_HEADER, false); 
        $result = curl_exec($curlSES);
        curl_close($curlSES);
        $array = json_decode($result, true);
		//echo $array[0]["Name"];
    ?>
    <br><br><br>
    <table>
        <tr>
            <td class="bA">ID</td> <td class="bA">N°</td> <td class="bA">Nome</td> <td class="bA">AlbumID</td> <td class="bA">Compositore</td> <td class="bA">Genere</td> <td class="bA">MediaFile</td> <td class="bA">Lunghezza</td>
        </tr>
        <?php
            for($i = 0; $i < count($array); $i++){
                echo "<tr>";
                echo "<td>". $array[$i]["ID"]["resource"] ."</td> <td>1</td> <td>".$array[$i]["Name"]."</td> <td>4</td> <td>".$array[$i]["Composer"]."</td> <td>".$array[$i]["Genre"]["resource"]."</td> <td>".$array[$i]["MediaType"]["resource"]."</td> <td>".$array[$i]["Milliseconds"]."</td>";
                echo "</tr>";
            }
        ?>
    </table>
</body>
</html>