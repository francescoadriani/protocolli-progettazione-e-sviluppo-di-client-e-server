import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.SocketException;

public class App {
    public static void main(String[] args) throws Exception {
        try{
        /* Instantiate client socket. 
            No need to bind to a specific port */
            DatagramSocket clientSocket = new DatagramSocket();
            
            // Get the IP address of the server
            InetAddress IPAddress = InetAddress.getByName("localhost");
            
            // Creating corresponding buffers
            byte[] sendingDataBuffer = new byte[1024];
            byte[] receivingDataBuffer = new byte[1024];
            
            /* Converting data to bytes and 
            storing them in the sending buffer */
            String sentence = "GET[0;1000]\n\r";
            sendingDataBuffer = sentence.getBytes();
            
            // Creating a UDP packet 
            DatagramPacket sendingPacket = new DatagramPacket(sendingDataBuffer,sendingDataBuffer.length,IPAddress, 10108);
            
            // sending UDP packet to the server
            clientSocket.send(sendingPacket);
            
            // Get the server response .i.e. capitalized sentence
            DatagramPacket receivingPacket = new DatagramPacket(receivingDataBuffer,receivingDataBuffer.length);
            clientSocket.receive(receivingPacket);
            
            // Printing the received data
            String receivedData = new String(receivingPacket.getData());
            System.out.println("Sent from the server: "+receivedData);
            
            // Closing the socket connection with the server
            clientSocket.close();
        }
        catch(SocketException e) {
            e.printStackTrace();
        }
    }
}
