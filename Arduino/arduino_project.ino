#include <SoftwareSerial.h>
SoftwareSerial hc06(6,7);

void setup() {
  // put your setup code here, to run once:
  pinMode(2, OUTPUT);
  pinMode(3,INPUT);
  Serial.begin(9600);
  hc06.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  digitalWrite(2,HIGH);
  delayMicroseconds(10);
  digitalWrite(2,LOW);

  if(hc06.available()) {
    Serial.println("hc06 available");
    Serial.write(hc06.read());
  }

  int distance = pulseIn(3, HIGH) * 340/2/10000;

  if(distance > 10) {
    hc06.println("dq");
  }
  
  Serial.print(distance);
  Serial.println("cm");
  
  delay(500);
}
