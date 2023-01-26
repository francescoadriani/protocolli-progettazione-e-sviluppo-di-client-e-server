#include <ESP8266WiFi.h>

#ifndef APSSID
#define APSSID "ESPap"
#define APPSK "123456789"
#endif
/* Set these to your desired credentials. */
const char *ssid = APSSID;
const char *password = APPSK;


WiFiServer server(80);

IPAddress myIP;

void setup() {

  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, HIGH);

  Serial.println();
  Serial.print("Configuring access point...");
  /* You can remove the password parameter if you want the AP to be open. */
  WiFi.softAP(ssid, password);

  myIP = WiFi.softAPIP();
  Serial.print("AP IP address: ");
  Serial.println(myIP);

  // Start the server
  server.begin();
  Serial.println(F("Server started"));

  // Print the IP address
  Serial.println(myIP);

}

void loop() {

  // Check if a client has connected
  WiFiClient client = server.available();
  if (!client) {
    return;
  }

  // Wait until the client sends some data
  Serial.println("Ciao!");
  while (!client.available()) {
    delay(1);
  }

  // Read the first line of the request
  String request = client.readStringUntil('\r');
  Serial.println(request);
  client.flush();

  // Match the request

  int value = LOW;
  if (request.indexOf("/LED=ON") != -1)  {
    digitalWrite(LED_BUILTIN, HIGH);
    value = HIGH;
  }
  if (request.indexOf("/LED=OFF") != -1)  {
    digitalWrite(LED_BUILTIN, LOW);
    value = LOW;
  }
  if (request.indexOf("/LED=LAMP") != -1)  {

    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);
    delay(100);
    digitalWrite(LED_BUILTIN, LOW);
    delay(100);
    digitalWrite(LED_BUILTIN, HIGH);

  }


  // Set ledPin according to the request
  //digitalWrite(ledPin, value);

  // Return the response
  client.println("HTTP/1.1 200 OK");
  client.println("Content-Type: text/html");
  client.println(""); //  do not forget this one
  client.println("<!DOCTYPE HTML>");
  client.println("<html>");
  client.println("<head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
  client.println("<link rel=\"icon\" href=\"data:,\">");

  client.println("<style>html { font-family: Helvetica; display: inline-block; margin: 0px auto; text-align: center; color:black;}");
  client.println(".button { background-color: #FF0000; border-radius: 12px; color: white; padding: 32px 16px;");
  client.println("text-decoration: none; font-size: 30px; margin: 2px; cursor: pointer;}</style></head>");


  client.println("<body><h1>ESP8266 WEB SERVER</h1>");
  client.print("<h2>LO STATO DEL LED: </h2>");

  if (value == HIGH) {
    client.print("<h3>ON : SONO ATTIVO</h3>");
  }
  if (value == LOW) {
    client.print("<h3>OFF : SONO SPENTO</h3>");
  }

  client.println("<br><br>");
  client.println("<a href=\"/LED=ON\"\"><button class=\"button\">ACCENDI </button></a>");
  client.println("<a href=\"/LED=OFF\"\"><button class=\"button\">SPEGNI </button></a><br />");
  client.println("<a href=\"/LED=LAMP\"\"><button class=\"button\">LAMPEGGIA </button></a><br />");
  client.println("</html>");

  delay(1);
  Serial.println("Client disconnesso");
  Serial.println("");

}
