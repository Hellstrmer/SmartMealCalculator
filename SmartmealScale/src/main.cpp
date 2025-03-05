#include <Arduino.h>
#include <WebSocketsClient_Generic.h>
#include <wifi.h>
#include <config.h>
#include <HTTPClient.h>
#include <HX711.h>

//Buttons

const int touchPin = T7;
const int touchPowerTreshold = 80;
const int UnitPin = T9;
const int touchUnitTreshold = 80;
const int TarePin = T5;
const int touchTareTreshold = 80;
int previousTouch;


//SignalR
WebSocketsClient client;
String handshake = "{\"protocol\":\"json\",\"version\":1}";

//HX711
const int LoadCell_DOUT_Pin = 16;
const int LoadCell_SCK_Pin = 4;
HX711 scale;

//Delay
unsigned long previousMillis = 0;
const long interval = 200;

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
  //Wifi
  Serial.begin(9600);
  WiFi.begin(ssid, password);

  Serial.print("Connecting to Wi-Fi");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("\nConnected to Wi-Fi!");

//SignalR
  client.begin("192.168.50.51", 5099, "/weight");
  client.onEvent(WebsocketEvent);

//HX711
scale.begin(LoadCell_DOUT_Pin, LoadCell_SCK_Pin);
}

void loop()
{

  client.loop();
  unsigned long currentMillis = millis();
  // if (currentMillis - previousMillis >= interval){
  //   previousMillis = currentMillis;
  //   static int messageCount = 0;
  //   if (messageCount == 0) {
  //       Screen();
  //   }
  //   // } else if (messageCount == 1) {
  //   //   analogWrite(displayPin, 0);
  //   // }
  //  messageCount = (messageCount + 1) % 2;
 // }  
  int touchValuePowerButton = touchRead(touchPin);
  int touchValueUnitButton = touchRead(UnitPin);
  int touchValueTareButton = touchRead(TarePin);
  bool PowerOn;
  bool Tare;
  bool Unit;

  if (touchValuePowerButton < touchPowerTreshold && previousTouch == 0){
    PowerOn = true;
    SendMessage("Startad!");
    previousTouch = touchValuePowerButton;
  }  else if (touchValueUnitButton < touchUnitTreshold && previousTouch == 0){
    Tare = true;
    SendMessage("Unit");
  } else if (touchValueTareButton < touchTareTreshold && previousTouch == 0){
    Unit = true;
    SendMessage("Unit");
  } else if (touchValuePowerButton > touchPowerTreshold
    && touchValueUnitButton > touchUnitTreshold  
    && touchValueTareButton > touchTareTreshold) {
    previousTouch = 0;
    PowerOn = false;
    Tare = false;
    Unit = false;
  }

  
}


