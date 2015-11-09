
$ip= "192.168.1.16"
$port = "80"

#usar $param($ip, $port) para cogermo mediante parametros

echo "Direccion IP:	$ip"
echo "Puerto TCP:	$port"

$connection = new-object net.Sockets.TcpClient

try 
{
	$connection.connect($ip,$port)
	$connection.close()
	
	echo "Puerto $port abierto"

    $strFileName="c:\prueba\j.txt"
    If (Test-Path $strFileName){
        echo "existe el fichero"
    }Else{
        echo "Fichero no encontrado"
    }
}
catch
{
	echo "Puerto $port cerrado"
}
echo " "
pause