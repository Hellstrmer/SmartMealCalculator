// #include <Arduino.h>
// #include <WebSocketsClient_Generic.h>
// #include <wifi.h>
// #include <config.h>
// #include <HTTPClient.h>
// #include <HX711.h>
// #include <SPI.h>
// #include <Wire.h>
// #include <Adafruit_GFX.h>
// #include <Adafruit_SSD1306.h>


// // Buttons

// const int touchPin = T7;
// const int touchPowerTreshold = 80;
// const int UnitPin = T5;
// const int touchUnitTreshold = 69;
// const int TarePin = T9;
// const int touchTareTreshold = 74;
// int previousTouchPower;
// int previousTouchUnit;
// int previousTouchTare;
// bool PowerOn;
// bool Tare;
// bool Unit;

// // SignalR
// WebSocketsClient client;
// String handshake = "{\"protocol\":\"json\",\"version\":1}";

// // HX711
// const int LoadCell_DOUT_Pin = 17;
// const int LoadCell_SCK_Pin = 16;
// long TareOffset = 0;
// long Weight = 0;
// float calibration_factor = 1;
// HX711 scale;

// //Screen
// // #define OLED_DC 18 
// // #define OLED_RESET 19  
// // #define OLED_CS 23  
// // #define OLED_SDA 13
// // #define OLED_SCL 5
// #define SCREEN_WIDTH 128
// #define SCREEN_HEIGHT 64
// #define OLED_RESET     -1 // Reset pin # (or -1 if sharing Arduino reset pin)
// #define SCREEN_ADDRESS 0x3C
// #define SDA_PIN 22
// #define SCL_PIN 21


// Adafruit_SSD1306 display(SCREEN_WIDTH, SCREEN_HEIGHT, &Wire, OLED_RESET);

// static const unsigned char PROGMEM logo_bmp[] =
// { 0b00000000, 0b11000000,
//   0b00000001, 0b11000000,
//   0b00000001, 0b11000000,
//   0b00000011, 0b11100000,
//   0b11110011, 0b11100000,
//   0b11111110, 0b11111000,
//   0b01111110, 0b11111111,
//   0b00110011, 0b10011111,
//   0b00011111, 0b11111100,
//   0b00001101, 0b01110000,
//   0b00011011, 0b10100000,
//   0b00111111, 0b11100000,
//   0b00111111, 0b11110000,
//   0b01111100, 0b11110000,
//   0b01110000, 0b01110000,
//   0b00000000, 0b00110000 };

// // Delay
// unsigned long previousMillis = 0;
// unsigned long previousMillisTare = 0;
// const long interval = 500;

// //Tare
// enum TareState {
//   Tare_Idle,
//   Tare_Wait,
//   Tare_Read
// };
// TareState tareState = Tare_Idle;
// unsigned long tareStartTime = 0;
// const int TareCount = 5;
// int CurrentIndex = 0;
// long sum = 0;

// void StartWifi()
// {
//   WiFi.begin(ssid, password);

//   Serial.print("Connecting to Wi-Fi");
//   while (WiFi.status() != WL_CONNECTED)
//   {
//     delay(500);
//     Serial.print(".");
//   }
//   Serial.println("\nConnected to Wi-Fi!");
// }


// void WebsocketEvent(WStype_t type, uint8_t *payload, size_t length)
// {
//   switch (type)
//   {
//   case WStype_DISCONNECTED:
//     Serial.println("Disconnected from WebSocket server");
//     break;
//   case WStype_CONNECTED:
//     Serial.println("Connected to WebSocket server");
//     client.sendTXT(handshake);
//     delay(300);
//     break;
//   }
// }

// void SendMessage(String message)
// {
//   String SignalRMessage = String("{\"arguments\":[\"") + message + "\"],\"invocationId\":\"0\",\"target\":\"SendMessage\",\"type\":1}";
//   client.sendTXT(SignalRMessage);
// }

// void SendWeight(String barcode, int weight)
// {
//   String SignalRMessage = "{\"arguments\":[\"" + barcode + "\"," + String(weight) + "],\"invocationId\":\"0\",\"target\":\"SendWeightData\",\"type\":1}";
//   client.sendTXT(SignalRMessage);
// }

// long Readweight()
// {
//   long rawValue = scale.read() - TareOffset;
//    return rawValue * calibration_factor;
// }

// void TareWeight()
// {
//   switch(tareState) {
//     case Tare_Idle:
//     if (Tare) {
//       tareState = Tare_Wait;
//       tareStartTime = millis();
//       break;
//     }
//     break;
    
//     case Tare_Wait:
//     if (millis() - tareStartTime >= 500) {
//       tareState = Tare_Read;
//     }
//     break;

//     case Tare_Read:
//     if (CurrentIndex < TareCount)
//     {
//       sum += scale.read();
//       CurrentIndex += 1;
//     } else 
//     {
//       TareOffset = sum / TareCount;
//       CurrentIndex = 0;
//       sum = 0;
//       Tare = false;
//       tareState = Tare_Idle;
//       Serial.println("Tare!");
//     }
//     break;
//   }
// }

// void CalibrateWeight()
// {
//   calibration_factor = 1010.0 / 314674.0;
//   Serial.println("Calibrated!");
// }

// void CheckTouchButtons(int pin, int threshold, bool &state, int &previousState, String message, void (*callback)() = nullptr)
// {
//   int touchValue = touchRead(pin);
//   if (touchValue < threshold && previousState == 0)
//   {
//     state = true;
//     SendMessage(message);
//     if (callback)
//     {
//       previousMillisTare = millis();
//       callback();
//     }
//     previousState = touchValue;
//   }
//   else if (touchValue > threshold)
//   {
//     previousState = 0;
//     state = false;
//   }
// }

// void ReadButtons()
// {
//   CheckTouchButtons(touchPin, touchPowerTreshold, PowerOn, previousTouchPower, "Startad!");
//   CheckTouchButtons(UnitPin, touchUnitTreshold, Unit, previousTouchUnit, "Unit", CalibrateWeight);
//   CheckTouchButtons(TarePin, touchTareTreshold, Tare, previousTouchTare, "Tare", TareWeight);
// }

// void ProcessWeight()
// {
//   long weight = Readweight();
//   // unsigned long currentMillis = millis();
//   // if (currentMillis - previousMillis >= interval)
//   // {
//   //   previousMillis = currentMillis;
//   //   if (scale.wait_ready_timeout(1000))
//   //   {
//   //     Serial.print("HX711 reading: ");
//   //     Serial.println(weight);
//   //   }
//   // }
//   SendWeight("ESP", weight);
// }


// void StartSignalR()
// {
// client.begin("192.168.50.51", 5099, "/weight");
// client.onEvent(WebsocketEvent);
// }

// void StartScale()
// {
//   scale.begin(LoadCell_DOUT_Pin, LoadCell_SCK_Pin);
//   Tare = true;
//   TareWeight();
//   CalibrateWeight();
// }

// void setup()
// {
//   Serial.begin(9600);
//   Wire.begin(SDA_PIN, SCL_PIN);
//   delay(100);
//   StartWifi();
//   StartSignalR();
//   StartScale();
//   if(!display.begin(SSD1306_SWITCHCAPVCC, SCREEN_ADDRESS)) {
//     Serial.println(F("SSD1306 allocation failed"));
//     for(;;); // Don't proceed, loop forever
//   }

//   // Show initial display buffer contents on the screen --
//   // the library initializes this with an Adafruit splash screen.å
//   display.display();
//   delay(2000); // Pause for 2 seconds

//   // Clear the buffer
//   display.clearDisplay();

//   // Draw a single pixel in white
//   display.drawPixel(10, 10, SSD1306_WHITE);

//   // Show the display buffer on the screen. You MUST call display() after
//   // drawing commands to make them visible on screen!
//   display.display();
//   delay(2000);
//   Serial.println("Setup Done!");
// }

// void loop()
// {
//   client.loop();
//   ReadButtons();
//   ProcessWeight();
//   TareWeight();
// }
#include <Arduino.h>
#include <Wire.h>
#include "SSD1306Wire.h"
// #define SDA_PIN 21
// #define SCL_PIN 22

#define SCREEN_WIDTH 128
#define SCREEN_HEIGHT 64
#define OLED_RESET    -1 // Reset pin # (or -1 if sharing Arduino reset pin)
#define SCREEN_ADDRESS 0x3C // Prova 0x3D om 0x3C inte fungerar


SSD1306Wire  display(0x3c, 21, 22);
#define DEMO_DURATION 3000
typedef void (*Demo)(void);

int demoMode = 0;
int counter = 1;

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.println();


  // Initialising the UI will init the display too.
  display.init();

  display.flipScreenVertically();
  display.setFont(ArialMT_Plain_10);

}

void drawFontFaceDemo() {
    // Font Demo1
    // create more fonts at http://oleddisplay.squix.ch/
    display.setTextAlignment(TEXT_ALIGN_LEFT);
    display.setFont(ArialMT_Plain_10);
    display.drawString(0, 0, "Hello world");
    display.setFont(ArialMT_Plain_16);
    display.drawString(0, 10, "Hello world");
    display.setFont(ArialMT_Plain_24);
    display.drawString(0, 26, "Hello world");
}

void drawTextFlowDemo() {
    display.setFont(ArialMT_Plain_10);
    display.setTextAlignment(TEXT_ALIGN_LEFT);
    display.drawStringMaxWidth(0, 0, 128,
      "Lorem ipsum\n dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore." );
}

void drawTextAlignmentDemo() {
    // Text alignment demo
  display.setFont(ArialMT_Plain_10);

  // The coordinates define the left starting point of the text
  display.setTextAlignment(TEXT_ALIGN_LEFT);
  display.drawString(0, 10, "Left aligned (0,10)");

  // The coordinates define the center of the text
  display.setTextAlignment(TEXT_ALIGN_CENTER);
  display.drawString(64, 22, "Center aligned (64,22)");

  // The coordinates define the right end of the text
  display.setTextAlignment(TEXT_ALIGN_RIGHT);
  display.drawString(128, 33, "Right aligned (128,33)");
}

void drawRectDemo() {
      // Draw a pixel at given position
    for (int i = 0; i < 10; i++) {
      display.setPixel(i, i);
      display.setPixel(10 - i, i);
    }
    display.drawRect(12, 12, 20, 20);

    // Fill the rectangle
    display.fillRect(14, 14, 17, 17);

    // Draw a line horizontally
    display.drawHorizontalLine(0, 40, 20);

    // Draw a line horizontally
    display.drawVerticalLine(40, 0, 20);
}

void drawCircleDemo() {
  for (int i=1; i < 8; i++) {
    display.setColor(WHITE);
    display.drawCircle(32, 32, i*3);
    if (i % 2 == 0) {
      display.setColor(BLACK);
    }
    display.fillCircle(96, 32, 32 - i* 3);
  }
}

void drawProgressBarDemo() {
  int progress = (counter / 5) % 100;
  // draw the progress bar
  display.drawProgressBar(0, 32, 120, 10, progress);

  // draw the percentage as String
  display.setTextAlignment(TEXT_ALIGN_CENTER);
  display.drawString(64, 15, String(progress) + "%");
}


Demo demos[] = {drawFontFaceDemo, drawTextFlowDemo, drawTextAlignmentDemo, drawRectDemo, drawCircleDemo, drawProgressBarDemo};
int demoLength = (sizeof(demos) / sizeof(Demo));
long timeSinceLastModeSwitch = 0;

void loop() {
  // clear the display
  display.clear();
  // draw the current demo method
  demos[demoMode]();

  display.setTextAlignment(TEXT_ALIGN_RIGHT);
  display.drawString(10, 128, String(millis()));
  // write the buffer to the display
  display.display();

  if (millis() - timeSinceLastModeSwitch > DEMO_DURATION) {
    demoMode = (demoMode + 1)  % demoLength;
    timeSinceLastModeSwitch = millis();
  }
  counter++;
  delay(10);
}