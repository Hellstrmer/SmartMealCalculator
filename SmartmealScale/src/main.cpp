#include <Arduino.h>
#include <WebSocketsClient_Generic.h>
#include <wifi.h>
#include <config.h>
#include <HTTPClient.h>

WebSocketsClient client;
String handshake = "{\"protocol\":\"json\",\"version\":1}";
unsigned long previousMillis = 0;
const long interval = 2000;

void WebsocketEvent(WStype_t type, uint8_t* payload, size_t length){
  switch (type) {
    case WStype_DISCONNECTED:
      Serial.println("Disconnected from WebSocket server");
      break;
    case WStype_CONNECTED:
      Serial.println("Connected to WebSocket server");      
      client.sendTXT(handshake);
      delay(1000);
      
      break;
    case WStype_TEXT:
      Serial.printf("Received message: %s\n", payload);
      break;
  }
}

void SendMessage(String message){
  String SignalRMessage = String("{\"arguments\":[\"") + message + "\"],\"invocationId\":\"0\",\"target\":\"SendMessage\",\"type\":1}";
  client.sendTXT(SignalRMessage);
}


void setup()
{
  Serial.begin(9600);
  WiFi.begin(ssid, password);

  Serial.print("Connecting to Wi-Fi");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("\nConnected to Wi-Fi!");


  client.begin("192.168.50.51", 5099, "/weight");
  client.onEvent(WebsocketEvent);

}

void loop()
{
  client.loop();
  unsigned long currentMillis = millis();
  if (currentMillis - previousMillis >= interval){
    previousMillis = currentMillis;
    static int messageCount = 0;
    if (messageCount == 0) {
      SendMessage("Hej!");
    } else if (messageCount == 1) {
      SendMessage("Funkar va?");
    }
    messageCount = (messageCount + 1) % 2;
  }  
}


