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
		// inizializzo cURL
		$curlSES = curl_init();

		// imposto la URL della risorsa remota da scaricare
        curl_setopt($curlSES, CURLOPT_URL, "http://localhost/tracks/3505");
		// evito che il contenuto remoto venga passato a print
        curl_setopt($curlSES, CURLOPT_RETURNTRANSFER, true);
		// imposto il tipo di chiamata html (GET, DELETE, POST, PUT)
        curl_setopt($curlSES, CURLOPT_CUSTOMREQUEST, "DELETE");
		// Imposto uno user-agent in modo arbitrario
		curl_setopt($curlSES, CURLOPT_USERAGENT, 'php client User-Agent');
		// Imposto che vengano risolti eventuale redirect
		curl_setopt($curlSES, CURLOPT_FOLLOWLOCATION, true);
		
		$track = array(
		  'ID' => '343',
		  'Album' => array('resource' => 1),
		  'Bytes' => 11170334,
		  'Composer' => 'Jury Maiolo',
		  'Genre' => array('resource' => 4),
		  'Mediatype' => array('resource' => 3),
		  'Milliseconds' => 343719,
		  'Name' => 'Mi fai spaccare il mondo',
		  'UnitPrice' => 0.56
		);
		// trasformo il mio array associativo in JSON
		$dati = json_encode($track);
		//echo $dati;
		
		// preparo l'invio dei dati col metodo POST
		//curl_setopt($curlSES, CURLOPT_PUT, true);
		//curl_setopt($curlSES, CURLOPT_POSTFIELDS, $dati);
		// imposto gli header correttamente
		//curl_setopt($curlSES, CURLOPT_HTTPHEADER, array(
		//  'Content-Type: application/json',
		//  'Content-Length: ' . strlen($dati))
		//);
		
		// eseguo la chiamata e ricevo la risposta
		$result = curl_exec($curlSES);

		// catturare eventuali errori
		if($result === false)
		{
			echo "Error Number:".curl_errno($curlSES)."<br>";
			echo "Error String:".curl_error($curlSES);
		}
		
		$array = json_decode($result, true);
		//echo "ID traccia aggiunta:" . $array["ID"]["resource"]; // ID è un array associativo all’interno di un altro array associativo
		
		// chiudo cURL
		curl_close($curlSES);
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