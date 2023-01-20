import java.net.InetAddress;
import java.util.HashMap;

public class MessageHandler
{
    // dizionario che memorizza lo stato della connessione relativa ad ogni ip
    public static HashMap<InetAddress, State> ipStringState = new HashMap<InetAddress, State>();

    // metodo statico che gestisce un messaggio di un client
    public static byte[] handle(byte[] byteData, int lenght, InetAddress address, int port)
    {
        double min = 0;
        double max = 1;

        if (MessageHandler.ipStringState.containsKey(address))
        {
            min = MessageHandler.ipStringState.get(address).Min;
            max = MessageHandler.ipStringState.get(address).Max;
        }

        String answer = "";
        String message = "";
        try
        {
            message = new String(byteData, 0, lenght);

            //pulizia del messaggio dagli a capo
            message = message.replace("\n", "");
            message = message.replace("\r", "");

            message = message.toLowerCase();
            //se c'è un messaggio get oppure get[min;max]
            if (message.equals("get") || (message.startsWith("get") && message.contains("[") && message.contains("]") && message.contains(";")))
            {
                //se c'è il parametro min e max impostali nelle variabili di stato
                if (message.contains("[") && message.contains("]") && message.contains(";"))
                {
                    String[] arrOfStr = message.split("\\[");
                    String param = arrOfStr[1];
                    try{
                        min = Double.parseDouble(param.split("\\;")[0]);
                        max = Double.parseDouble(param.split("\\;")[1].substring(0, param.split(";")[1].indexOf("]")));
                        if (MessageHandler.ipStringState.containsKey(address))
                        {
                            MessageHandler.ipStringState.get(address).Min = min;
                            MessageHandler.ipStringState.get(address).Max = max;
                        }
                        else
                        {
                            State s = new State(min, max);
                            MessageHandler.ipStringState.put(address, s);
                        }
                    }
                    catch(Exception e){

                    }
                }

                //generazione del numero random dal min al max
                double res = min + Math.random() * (max - min);

                //preparazione della risposta
                answer = (Double.valueOf(res)).toString();
            }
            else if (message.length() > 0) //messaggio non vuoto sconosciuto
            {
                //preparazione della risposta
                answer = "UNKNOWN COMMAND";
            }
        }
        catch (Exception e)
        {
            System.out.println("Error: " + e.getMessage());
        }
        return answer.getBytes();
    }
}
