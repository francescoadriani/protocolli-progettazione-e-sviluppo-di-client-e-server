#include <WiFi.h> // #include <ESP8266WiFi.h> // 
#define LED_BUILTIN 2
const char* ssid = "esp8266_AP";
const char* password = "12345678";
WiFiServer serverTcp(10103);
void setup() {
  pinMode(LED_BUILTIN, OUTPUT);
  randomSeed(analogRead(0));
  Serial.begin(9600);
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
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
}
void loop() {
  WiFiClient clientTcp = serverTcp.available();
  digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
  if (!clientTcp) {
    return;
  }
  Serial.println("New client connected");
  digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
  double min = 0;
  double max = 1.0;
  while (true)
  {
    String answer = "";
    while (!clientTcp.available()) {
      delay(1);
    }
    String message = clientTcp.readStringUntil('\r');
    digitalWrite(LED_BUILTIN, HIGH);   // turn the LED on (HIGH is the voltage level)
    delay(100);                       // wait for a second
    digitalWrite(LED_BUILTIN, LOW);    // turn the LED off by making the voltage LOW
    message.replace("\n", "");
    message.replace("\r", "");
    Serial.println(message);
    delay(1);
    if (message.indexOf("GET") > -1)
    {
      if (message.indexOf("GET[") > -1)
      {
        int indexOfSeparator = message.indexOf(";");
        int indexOfSecondPare = message.indexOf("]");
        if (indexOfSeparator > -1 && indexOfSecondPare > -1)
        {
          min = message.substring(4, indexOfSeparator).toDouble();
          max = message.substring(indexOfSeparator + 1, indexOfSecondPare).toDouble();
        }
      }
      double rnd = random(1000000);
      double rndMinMax = min + (rnd * (max - min) / 1000000.0);
      answer = String(rndMinMax);
    }
    else if (message.indexOf("CLOSE") > -1)
    {
      Serial.println("Client requested disconnection");
      clientTcp.stop();
      return;
    }
    else
      answer = "COMMAND UNKNOW";
    clientTcp.println(answer);
  }
}
