#include <ESP8266WiFi.h> // #include <WiFi.h>
#define LED_BUILTIN 2
const char* ssid = "nomewifi";
const char* password = "pwswifi";
WiFiServer serverTcp(10103);
void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);   // turn the LED on (HIGH is the voltage level)
  Serial.begin(115200);
  WiFi.begin(ssid,password);
  while(WiFi.status() != WL_CONNECTED ){ delay(500); }
  Serial.print("Wifi Connected Success!\n\rIP Address: ");
  Serial.println(WiFi.localIP() );
  Serial.print("MAC Address: ");
  Serial.println(WiFi.macAddress());
  serverTcp.begin();
  Serial.println("Server started on port 10103");
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
}
void loop() {
  WiFiClient clientTcp = serverTcp.available();
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  if (!clientTcp) { return; }
  Serial.println("New client connected");
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
  while (true)
  {
    while(!clientTcp.available()){ delay(1); }
    String message = clientTcp.readStringUntil('\r');
    digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(100);                       // wait for a second
    digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
    message.replace("\n","");
    message.replace("\r","");
    Serial.println(message);
    clientTcp.print("ECHO:");
    clientTcp.println(message);
    delay(1);
    if (message.indexOf("CLOSE")>-1)
    {
        Serial.println("Client requested disconnection");
        clientTcp.stop();
    }
  }
}
