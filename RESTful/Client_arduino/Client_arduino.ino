/**
   PostHTTPClient.ino

    Created on: 21.11.2016

*/

#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>

/* this can be run with an emulated server on host:
        cd esp8266-core-root-dir
        cd tests/host
        make ../../libraries/ESP8266WebServer/examples/PostServer/PostServer
        bin/PostServer/PostServer
   then put your PC's IP address in SERVER_IP below, port 9080 (instead of default 80):
*/
//#define SERVER_IP "10.0.1.7:9080" // PC address with emulation on host
#define SERVER_IP "10.2.232.51:80"

#ifndef STASSID
#define STASSID "ASUS_RiceWLan"
#define STAPSK  "pippoplutopaperinominnie"
#endif

void setup() {

  Serial.begin(115200);

  Serial.println();
  Serial.println();
  Serial.println();

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
  if ((WiFi.status() == WL_CONNECTED)) {

    WiFiClient client;
    HTTPClient http;

    Serial.print("[HTTP] begin...\n");
    // configure traged server and url
    http.begin(client, "http://" SERVER_IP "/tracks/"); //HTTP
    http.addHeader("Content-Type", "application/json");

    Serial.print("[HTTP] POST...\n");
    // start connection and send HTTP header and body
    String postString="";
    postString+="{\n\r";
    postString+="\"Album\":{\n\r";
    postString+="\"href\":\"Contenuto stringa\",\n\r";
    postString+="\"resource\":9223372036854775807\n\r";
    postString+="},\n\r";
    postString+="\"Bytes\":9223372036854775807,\n\r";
    postString+="\"Composer\":\"Contenuto stringa\",\n\r";
    postString+="\"Genre\":{\n\r";
    postString+="\"href\":\"Contenuto stringa\",\n\r";
    postString+="\"resource\":9223372036854775807\n\r";
    postString+="},\n\r";
    postString+="\"ID\":{\n\r";
    postString+="\"href\":\"Contenuto stringa\",\n\r";
    postString+="\"resource\":9223372036854775807\n\r";
    postString+="},\n\r";
    postString+="\"MediaType\":{\n\r";
    postString+="\"href\":\"Contenuto stringa\",\n\r";
    postString+="\"resource\":12\n\r";
    postString+="},\n\r";
    postString+="\"Milliseconds\":5,\n\r";
    postString+="\"Name\":\"Contenuto stringa\",\n\r";
    postString+="\"UnitPrice\":12678967.543233\n\r";
    postString+="}\n\r";

    //int httpCode = http.POST("{\"students\":\"Francesco\"}");
    int httpCode = http.POST(postString);

    // httpCode will be negative on error
    if (httpCode > 0) {
      // HTTP header has been send and Server response header has been handled
      Serial.printf("[HTTP] POST... code: %d\n", httpCode);

      // file found at server
      if (httpCode == HTTP_CODE_OK) {
        const String& payload = http.getString();
        Serial.println("received payload:\n<<");
        Serial.println(payload);
        Serial.println(">>");
      }
    } else {
      Serial.printf("[HTTP] POST... failed, error: %s\n", http.errorToString(httpCode).c_str());
    }

    http.end();
  }

  delay(10000);
}
