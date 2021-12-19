#include <ESP8266WiFi.h> // #include <WiFi.h>
const char* ssid = "esp8266_AP";
const char* password = "12345678";
WiFiServer serverTcp(10103);
void setup() {
  randomSeed(analogRead(0));
  Serial.begin(115200);
  WiFi.mode(WIFI_AP); // attivazione della wifi come client wifi
  WiFi.softAP(ssid, password); // attiva la modalità Access Point 
  Serial.println();
  Serial.print("MAC Address: ");
  Serial.println(WiFi.macAddress());
  Serial.print("AP activated!, SSID: ");
  Serial.println(ssid);
  Serial.print("PASSWORD: ");
  Serial.println(password);
  Serial.print("AP ip address: ");
  Serial.println(WiFi.softAPIP());
  
  //WiFi.begin(ssid,password);
  //while(WiFi.status() != WL_CONNECTED ){ delay(500); }
  //Serial.print("Wifi Connected Success!, IP Address: ");
  //Serial.println(WiFi.localIP() );
  serverTcp.begin();
  Serial.println("Server started on port 10103");
}
void loop() {
  WiFiClient clientTcp = serverTcp.available();
  if (!clientTcp) { return; }
  Serial.println("New client connected");
  double min=0;
  double max=1.0;
  while (true)
  {
    String answer = "";
    while(!clientTcp.available()){ delay(1); }
    String message = clientTcp.readStringUntil('\r');
    message.replace("\n","");
    message.replace("\r","");
    Serial.println(message);
    delay(1);
    if (message.indexOf("GET")>-1)
    {
      if (message.indexOf("GET[")>-1)
      {
        int indexOfSeparator = message.indexOf(";");
        int indexOfSecondPare = message.indexOf("]");
        if (indexOfSeparator>-1 && indexOfSecondPare>-1)
        {
          min = message.substring(4,indexOfSeparator).toDouble();
          max = message.substring(indexOfSeparator+1,indexOfSecondPare).toDouble();
        }
      }
      double rnd = random(1000000);
      double rndMinMax = min + (rnd * (max-min) /1000000.0);
      answer = String(rndMinMax);
    }
    else if (message.indexOf("CLOSE")>-1)
    {
        Serial.println("Client requested disconnection");
        clientTcp.stop();
    }
    else
      answer = "COMMAND UNKNOW";
    clientTcp.println(answer);
  }
}
