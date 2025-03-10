#include <Arduino.h>
#include <WebSocketsClient_Generic.h>
#include <wifi.h>
#include <config.h>
#include <HTTPClient.h>
#include <HX711.h>

//Buttons

const int touchPin = T7;
const int touchPowerTreshold = 80;
const int UnitPin = T5;
const int touchUnitTreshold = 69;
const int TarePin = T9;
const int touchTareTreshold = 74;
int previousTouchPower;
int previousTouchUnit;
int previousTouchTare;
bool PowerOn;
bool Tare;
bool Unit;


//SignalR
WebSocketsClient client;
String handshake = "{\"protocol\":\"json\",\"version\":1}";

//HX711
const int LoadCell_DOUT_Pin = 17;
const int LoadCell_SCK_Pin = 16;
long TareOffset = 0;
long Weight = 0;
float calibration_factor = 1;
HX711 scale;

//Delay
unsigned long previousMillis = 0;
const long interval = 500;

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

void SendWeight(string barcode, int weight){
  String SignalRMessage = String("{\"arguments\":[\"") + barcode +  weight "\"],\"invocationId\":\"0\",\"target\":\"SendWeightData\",\"type\":1}";
  client.sendTXT(SignalRMessage);
}

void TareWeight(){

  TareOffset = scale.read();
}

long Readweight(){
  long rawValue = scale.read() - TareOffset;
  
  if (rawValue > 0) {
    Weight = rawValue * calibration_factor;
    return Weight;
  } 
  return 0;   
}

void CalibrateWeight(){
  calibration_factor = 1010.0 / 314674.0;
  Serial.println("Calibrated!");
}

void ReadButtons(){
  int touchValuePowerButton = touchRead(touchPin);
  int touchValueUnitButton = touchRead(UnitPin);
  int touchValueTareButton = touchRead(TarePin);


  if (touchValuePowerButton < touchPowerTreshold && previousTouchPower == 0){
    PowerOn = true;
    SendMessage("Startad!");
    previousTouchPower = touchValuePowerButton;
  } 
  else if (touchValueUnitButton < touchUnitTreshold && previousTouchUnit == 0){    
    Unit = true;
    SendMessage("Unit");
    CalibrateWeight();
    previousTouchUnit = touchValueUnitButton;
  } 
  else if (touchValueTareButton < touchTareTreshold && previousTouchTare == 0){
    Tare = true;    
    TareWeight();
    SendMessage("Tare");
    Serial.println(Readweight());

    previousTouchTare = touchValueTareButton;
  } else if (touchValuePowerButton > touchPowerTreshold
    && touchValueUnitButton > touchUnitTreshold  
    && touchValueTareButton > touchTareTreshold) {
    previousTouchPower = 0;
    previousTouchUnit = 0;
    previousTouchTare = 0;
    PowerOn = false;
    Tare = false;
    Unit = false;
  }  
}



void setup()
{
  //Wifi
  Serial.begin(9600);
  delay(1000);

  // Serial.println("Activated deep sleep..");

  // esp_sleep_enable_touchpad_wakeup();
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
TareWeight();
CalibrateWeight();
}

void loop()
{

  client.loop();
  unsigned long currentMillis = millis();
  if (currentMillis - previousMillis >= interval){
    previousMillis = currentMillis;
    static int messageCount = 0;
    if (messageCount == 0) {
      if (scale.wait_ready_timeout(1000)) {
        Serial.print("HX711 reading: ");
        Serial.println(Readweight());
      } else {
        Serial.println("HX711 not found.");
      }
    }
    // } else if (messageCount == 1) {
    //   analogWrite(displayPin, 0);
    // }
   messageCount = (messageCount + 1) % 2;
 }  
  
  ReadButtons();
  SendWeight("ESP", Readweight());
}


