#include <IRremote.h>

int RECV_PIN = 2;
IRrecv irrecv(RECV_PIN);
decode_results results;

void setup()
{
  Serial.begin(9600);
  irrecv.enableIRIn(); // Start the receiver
}

const char* keyToString(const unsigned long& key)
{
  if (key == 0xFFA25D) return "CH-";
  if (key == 0xFF629D) return "CH";
  if (key == 0xFFE21D) return "CH+";
  if (key == 0xFF22DD) return "PREV";
  if (key == 0xFF02FD) return "NEXT";
  if (key == 0xFFC23D) return "PLAY/PAUSE";
  if (key == 0xFFE01F) return "VOL-";
  if (key == 0xFFA857) return "VOL+";
  if (key == 0xFF906F) return "EQ";
  if (key == 0xFF6897) return "0";
  if (key == 0xFF9867) return "100+";
  if (key == 0xFFB04F) return "200+";
  if (key == 0xFF30CF) return "1";
  if (key == 0xFF18E7) return "2";
  if (key == 0xFF7A85) return "3";
  if (key == 0xFF10EF) return "4";
  if (key == 0xFF38C7) return "5";
  if (key == 0xFF5AA5) return "6";
  if (key == 0xFF42BD) return "7";
  if (key == 0xFF4AB5) return "8";
  if (key == 0xFF52AD) return "9";
}

void loop() 
{
  if (irrecv.decode(&results)) 
  {
    Serial.println(keyToString(results.value));
    irrecv.resume(); // Receive the next value
  }
}
