#include <Arduino.h>
#include <ArduinoJson.h>
#include "time.h"
#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClientSecureBearSSL.h>

#define SERVER_IP "https://testdb-9d6d.restdb.io/rest"

#ifndef STASSID
#define STASSID "---"
#define STAPSK  "---"
#endif

const char* ntpServer = "pool.ntp.org";
const long  gmtOffset_sec = 3600;   //Replace with your GMT offset (seconds)
const int   daylightOffset_sec = 0;  //Replace with your daylight offset (seconds)

ESP8266WiFiMulti WiFiMulti;
DynamicJsonDocument doc(1024);

void setup() {
  Serial.begin(115200);
  WiFi.begin(STASSID, STAPSK);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.print("\nConnected! IP address: ");
  Serial.println(WiFi.localIP());

  configTime(gmtOffset_sec, daylightOffset_sec, ntpServer);
}

void loop() {
  if ((WiFiMulti.run() == WL_CONNECTED)) {
    
    std::unique_ptr<BearSSL::WiFiClientSecure>client(new BearSSL::WiFiClientSecure);
    client->setInsecure();

    HTTPClient https;
    
    Serial.print("[HTTPS] begin...\n");
    if (https.begin(*client, SERVER_IP "/measurements")) 
    {
      https.addHeader("content-type", "application/json");
      https.addHeader("cache-control", "no-cache");
      https.addHeader("x-apikey", "c6d4ca494ebd81e94c3ec107b333a8b0bf14d");
      
      time_t rawtime;
      struct tm * timeinfo;
      time (&rawtime);
      timeinfo = localtime (&rawtime);
      String timestamp=asctime(timeinfo); //con questa funzione viene aggiunto un a capo che il server rest non gradisce
      timestamp.replace("\n","");
      
      doc["key"]="microphone";
      doc["value"]=analogRead(A0);
      doc["timestamp"]=timestamp;
  
      String output;
      serializeJson(doc, output);
      
      Serial.print("[HTTP] POST: ");
      Serial.println(output);

      int httpCode = https.POST(output); 
      
      if (httpCode > 0) {
        Serial.printf("[HTTP] POST... code: %d\n", httpCode);
  
        if (httpCode == HTTP_CODE_OK) {
          const String& payload = https.getString();
          Serial.println("received payload:\n<<");
          Serial.println(payload);
          Serial.println(">>");
        }
      } else {
        Serial.printf("[HTTP] POST... failed, error: %s\n", https.errorToString(httpCode).c_str());
      }
  
      https.end();
    } else {
      Serial.printf("[HTTPS] Unable to connect\n");
    }
  }

  Serial.println("Wait 10s before next round...");
  delay(1000);
}
