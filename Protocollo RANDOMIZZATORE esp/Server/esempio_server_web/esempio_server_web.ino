#include <ESP8266WiFi.h> // #include <WiFi.h>
const char* ssid = "nomewifi";
const char* password = "passwordwifi";
WiFiServer serverTcp(10103);
void setup() {
  Serial.begin(115200);
  WiFi.begin(ssid,password);
  while(WiFi.status() != WL_CONNECTED ){ delay(500); }
  Serial.print("Wifi Connected Success!, IP Address: ");
  Serial.println(WiFi.localIP() );
  serverTcp.begin();
}
void loop() {
  WiFiClient clientTcp = serverTcp.available();
  if (!clientTcp) { return; }
  while (true)
  {
    while(!clientTcp.available()){ delay(1); }
    String message = clientTcp.readStringUntil('\r');
    Serial.println(message);
    clientTcp.print("ECHO:");
    clientTcp.println(message);
    clientTcp.println();
    delay(1);
    if (message.indexOf("CLOSE")>-1)
    {
        Serial.println("Client requested disconnection");
        clientTcp.stop();
    }
  }
}
