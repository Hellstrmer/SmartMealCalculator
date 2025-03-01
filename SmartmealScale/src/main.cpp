#include <Arduino.h>
#include <ArduinoWebsockets.h>
#include <wifi.h>
#include <config.h>

const uint16_t websockets_server_port = 5099;

using namespace websockets;

void onMessageCallback(WebsocketsMessage message) {
  Serial.print("Got Message: ");
  Serial.print("message.data()");
}

void onEventCallback(WebsocketsEvent event, String data) {

  if(event == WebsocketsEvent::ConnectionOpened){
    Serial.println("Connection Opened");
  } else if (event == WebsocketsEvent::ConnectionClosed){
    Serial.println("Connection Closed");
  } else if (event == WebsocketsEvent::GotPing) {
    Serial.println("Got a ping!");
  } else if (event == WebsocketsEvent::GotPong) {
    Serial.println("Got a pong!");
  }
}

WebsocketsClient client;

void setup() {
  Serial.begin(9600);
  WiFi.begin(ssid, password);

  for(int i = 0; i < 10 && WiFi.status() != WL_CONNECTED; i++){
    Serial.print(".");
    delay(1000);
  }

  client.onMessage(onMessageCallback);

  // Run Callback when events are occuring
  client.onEvent(onEventCallback);

  // Connect to server
  client.connect(websocket_server_host);

  client.send("Hello from Esp!");

  client.ping();
}

void loop() {
  client.poll();
}

