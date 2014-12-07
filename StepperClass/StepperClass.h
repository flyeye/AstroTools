/*
StepperClass - let you control Polulu stepper motors drivers 
like A4988 and RDRV8825.

Simple usage:
 - Just create using default constructor and call Init();
 - Enable power;
 - Step in STEP_BACKWARD or STEP_FORWARD direction
 
To roll with certain speed: 
 - Set delay between steps in microseconds in fSpeed;
 - Enable power;
 - Roll in STEP_BACKWARD or STEP_FORWARD direction 
To roll with certain speed you need to call Roll() function more frequently 
then fSpeed.

You can also you position limitation by corresponding fields. 
Speed limitation is not implemented yet. 
fTargetPosition and fRelativePosition is useful, but this code 
is also implemented outside.

Author:

Developed by Alexey V. Popov
St.Peterburg, Russia
2014.10.01
9141866@gmail.com

*/
#ifndef StepperClass_h
#define StepperClass_h

#include "Arduino.h"


#define A4988     1
#define DRV8825   2

#define STEP_BACKWARD -1
#define STEP_FORWARD 1
#define STEP_HOLD 0

#define MICROSTEP1 0
#define MICROSTEP2 1
#define MICROSTEP4 2
#define MICROSTEP8 3
#define MICROSTEP16 4
#define MICROSTEP32 5
//const int MICROSTEPS[] = {32,16,8,4,2,1};

class StepperClass
{
  private:
    int fStepPin, fDirPin, fEnablePin, fMS1Pin, fMS2Pin, fMS3Pin;
    unsigned long fLastStep;
    int fType;

    int fStepTime_microsec;

  public:
    long fPosition, fMaxPosition, fMinPosition, fTargetPosition, fRelativePosition;  

    long fSpeed, fMinSpeed, fMaxSpeed; // In this version its not a speed, its a timeframe between steps.                

     
    long fStepsPerRevolution, fStepsPerRevolutionDefault;
    int fMicroStep;
    int fMicroSteps[10]; 
    boolean fEnabled, fRangeCheck;

    StepperClass(byte step, byte dir, byte enable, byte ms1, byte ms2, byte ms3, int step_rev, int type);
    // step, dir - step and dir pins
    // ms1, ms2, ms3 - microstepping pins
    // step_rev - step by revolution
    // type - model of driver (A4988 or DRV8825)

    void Init();  

    void EnablePower(boolean enable);
    void SetMicroStep(int new_micro_step);

    long Step(int dir);     
    long Roll(int dir);


};

#endif