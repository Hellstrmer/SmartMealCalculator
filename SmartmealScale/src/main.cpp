#include <Arduino.h>
#include <WebSocketsClient_Generic.h>
#include <wifi.h>
#include <config.h>
#include <HTTPClient.h>
#include <HX711.h>

// Buttons

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

// SignalR
WebSocketsClient client;
String handshake = "{\"protocol\":\"json\",\"version\":1}";

// HX711
const int LoadCell_DOUT_Pin = 17;
const int LoadCell_SCK_Pin = 16;
long TareOffset = 0;
long Weight = 0;
float calibration_factor = 1;
HX711 scale;

// Delay
unsigned long previousMillis = 0;
unsigned long previousMillisTare = 0;
const long interval = 500;

//Tare
enum TareState {
  Tare_Idle,
  Tare_Wait,
  Tare_Read
};
TareState tareState = Tare_Idle;
unsigned long tareStartTime = 0;
const int TareCount = 5;
int CurrentIndex = 0;
long sum = 0;

void StartWifi()
{
  WiFi.begin(ssid, password);

  Serial.print("Connecting to Wi-Fi");
  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nConnected to Wi-Fi!");
}


void WebsocketEvent(WStype_t type, uint8_t *payload, size_t length)
{
  switch (type)
  {
  case WStype_DISCONNECTED:
    Serial.println("Disconnected from WebSocket server");
    break;
  case WStype_CONNECTED:
    Serial.println("Connected to WebSocket server");
    client.sendTXT(handshake);
    delay(300);
    break;
  }
}

void SendMessage(String message)
{
  String SignalRMessage = String("{\"arguments\":[\"") + message + "\"],\"invocationId\":\"0\",\"target\":\"SendMessage\",\"type\":1}";
  client.sendTXT(SignalRMessage);
}

void SendWeight(String barcode, int weight)
{
  String SignalRMessage = "{\"arguments\":[\"" + barcode + "\"," + String(weight) + "],\"invocationId\":\"0\",\"target\":\"SendWeightData\",\"type\":1}";
  client.sendTXT(SignalRMessage);
}

long Readweight()
{
  long rawValue = scale.read() - TareOffset;
   return rawValue * calibration_factor;
}

void TareWeight()
{
  switch(tareState) {
    case Tare_Idle:
    if (Tare) {
      tareState = Tare_Wait;
      tareStartTime = millis();
      break;
    }
    break;
    
    case Tare_Wait:
    if (millis() - tareStartTime >= 500) {
      tareState = Tare_Read;
    }
    break;

    case Tare_Read:
    if (CurrentIndex < TareCount)
    {
      sum += scale.read();
      CurrentIndex += 1;
    } else 
    {
      TareOffset = sum / TareCount;
      CurrentIndex = 0;
      sum = 0;
      Tare = false;
      tareState = Tare_Idle;
      Serial.println("Tare!");
    }
    break;
  }
}

void CalibrateWeight()
{
  calibration_factor = 1010.0 / 314674.0;
  Serial.println("Calibrated!");
}

void CheckTouchButtons(int pin, int threshold, bool &state, int &previousState, String message, void (*callback)() = nullptr)
{
  int touchValue = touchRead(pin);
  if (touchValue < threshold && previousState == 0)
  {
    state = true;
    SendMessage(message);
    if (callback)
    {
      previousMillisTare = millis();
      callback();
    }
    previousState = touchValue;
  }
  else if (touchValue > threshold)
  {
    previousState = 0;
    state = false;
  }
}

void ReadButtons()
{
  CheckTouchButtons(touchPin, touchPowerTreshold, PowerOn, previousTouchPower, "Startad!");
  CheckTouchButtons(UnitPin, touchUnitTreshold, Unit, previousTouchUnit, "Unit", CalibrateWeight);
  CheckTouchButtons(TarePin, touchTareTreshold, Tare, previousTouchTare, "Tare", TareWeight);
}

void ProcessWeight()
{
  long weight = Readweight();
  // unsigned long currentMillis = millis();
  // if (currentMillis - previousMillis >= interval)
  // {
  //   previousMillis = currentMillis;
  //   if (scale.wait_ready_timeout(1000))
  //   {
  //     Serial.print("HX711 reading: ");
  //     Serial.println(weight);
  //   }
  // }
  SendWeight("ESP", weight);
}


void StartSignalR()
{
client.begin("192.168.50.51", 5099, "/weight");
client.onEvent(WebsocketEvent);
}

void StartScale()
{
  scale.begin(LoadCell_DOUT_Pin, LoadCell_SCK_Pin);
  Tare = true;
  TareWeight();
  CalibrateWeight();
}

void setup()
{
  Serial.begin(9600);
  delay(100);
  StartWifi();
  StartSignalR();
  StartScale();
}

void loop()
{
  client.loop();
  ReadButtons();
  ProcessWeight();
  TareWeight();
}
