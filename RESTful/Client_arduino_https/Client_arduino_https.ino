/**
   BasicHTTPSClient.ino

    Created on: 20.08.2018

*/

#include <Arduino.h>

#include <ESP8266WiFi.h>
#include <ESP8266WiFiMulti.h>

#include <ESP8266HTTPClient.h>

#include <WiFiClientSecureBearSSL.h>
// Fingerprint for demo URL, expires on June 2, 2021, needs to be updated well before this date
const uint8_t fingerprint[20] = {0x40, 0xaf, 0x00, 0x6b, 0xec, 0x90, 0x22, 0x41, 0x8e, 0xa3, 0xad, 0xfa, 0x1a, 0xe8, 0x25, 0x41, 0x1d, 0x1a, 0x54, 0xb3};

#define SERVER_IP "https://api4api.p.rapidapi.com"

#ifndef STASSID
#define STASSID "HUAWEI P40 Pro Rice"
#define STAPSK  "abcdefghijklm"
#endif


ESP8266WiFiMulti WiFiMulti;

void setup() {

  Serial.begin(115200);
  // Serial.setDebugOutput(true);

  Serial.println();
  Serial.println();
  Serial.println();

  for (uint8_t t = 4; t > 0; t--) {
    Serial.printf("[SETUP] WAIT %d...\n", t);
    Serial.flush();
    delay(1000);
  }

  WiFi.begin(STASSID, STAPSK);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("");
  Serial.print("Connected! IP address: ");
  Serial.println(WiFi.localIP());
}

void loop() {
  // wait for WiFi connection
  if ((WiFiMulti.run() == WL_CONNECTED)) {

    std::unique_ptr<BearSSL::WiFiClientSecure>client(new BearSSL::WiFiClientSecure);

    //client->setFingerprint(fingerprint);
    // Or, if you happy to ignore the SSL certificate, then use the following line instead:
    client->setInsecure();

    HTTPClient https;
    
    Serial.print("[HTTPS] begin...\n");
    if (https.begin(*client, SERVER_IP "/beehives/")) 
    {
      https.addHeader("Content-Type", "application/json");
      https.addHeader("X-RapidAPI-Key", "ee464ac4cdmsh113a714202d2649p13e0e7jsn18ac620f97a5");
      https.addHeader("X-RapidAPI-Host", "api4api.p.rapidapi.com");
  
      Serial.print("[HTTP] POST...\n");
      // start connection and send HTTP header and body
      int httpCode = https.POST("{\"key1\":\"hello\",\"key2\":\"world\"}"); //'{"key1":"value","key2":"value"}'
  
      // httpCode will be negative on error
      if (httpCode > 0) {
        // HTTP header has been send and Server response header has been handled
        Serial.printf("[HTTP] POST... code: %d\n", httpCode);
  
        // file found at server
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
  delay(10000);
}
