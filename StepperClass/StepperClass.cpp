#include "Arduino.h"
#include "StepperClass.h"

StepperClass::StepperClass(byte step, byte dir, byte enable, byte ms1, byte ms2, byte ms3, /*byte sleep, */int step_rev, int type)
{
   fStepPin = step;
   fDirPin = dir;
   fEnablePin = enable;
   fEnabled = false;
   fMS1Pin = ms1; fMS2Pin = ms2; fMS3Pin = ms3;   
//   fSleepPin = sleep;


   fStepsPerRevolutionDefault = step_rev;
   fStepsPerRevolution = fStepsPerRevolutionDefault;

   fPosition = 0; fMaxPosition = 0; fMinPosition = 0;
   fTargetPosition = 0; fRelativePosition = 0;
   fSpeed = 0; fMaxSpeedDelay = 0; fMinSpeedDelay = 0;
   fMicroStep = 0;  

   fType = type;
   if (fType==DRV8825) {
     fMicroSteps[MICROSTEP1] = 32;
     fMicroSteps[MICROSTEP2] = 16;
     fMicroSteps[MICROSTEP4] = 8;
     fMicroSteps[MICROSTEP8] = 4;
     fMicroSteps[MICROSTEP16] = 2;
     fMicroSteps[MICROSTEP32] = 1;
   } else if (fType==A4988){
     fMicroSteps[MICROSTEP1] = 16;
     fMicroSteps[MICROSTEP2] = 8;
     fMicroSteps[MICROSTEP4] = 4;
     fMicroSteps[MICROSTEP8] = 2;
     fMicroSteps[MICROSTEP16] = 1;
   }
   fRangeCheck = false;
   fStepTime_microsec = 2;
   
   fLastStep = 0;
   fRealStepTime = 0;
   fRealLastStep = 0;

   fIsDelayCompensation = false;
}

void StepperClass::Init(long minspeedd, long maxspeedd)
{   

   fEnabled = false;

   pinMode(fDirPin, OUTPUT);
   pinMode(fStepPin, OUTPUT);
   pinMode(fMS1Pin, OUTPUT);
   pinMode(fMS2Pin, OUTPUT);  
   pinMode(fMS3Pin, OUTPUT);
//   pinMode(fSleepPin, OUTPUT);
   pinMode(fEnablePin, OUTPUT);

   digitalWrite(fEnablePin, HIGH);      
//   digitalWrite(fSleepPin, HIGH);
   digitalWrite(fStepPin, HIGH);

   fMaxSpeedDelay = maxspeedd; fMinSpeedDelay = minspeedd;
    
}


void StepperClass::EnablePower(boolean enable)
{
   if (fEnabled==enable)
      return;
   fEnabled=enable;   
   if (fEnabled){
     digitalWrite(fEnablePin, LOW);          
     delayMicroseconds(1);         
   }
    else 
     digitalWrite(fEnablePin, HIGH);
}

long StepperClass::Step(int dir)
{
    if (fRangeCheck){
      if ((dir==STEP_BACKWARD)&&(fPosition-fMicroSteps[fMicroStep]<fMinPosition))
          return 0;
      if ((dir==STEP_FORWARD)&&(fPosition+fMicroSteps[fMicroStep]>fMaxPosition)) 
          return 0;
    }

    EnablePower(true);
  
    if (dir==STEP_BACKWARD)    
      digitalWrite(fDirPin, HIGH);
    else if (dir==STEP_FORWARD)
      digitalWrite(fDirPin, LOW);     
    delayMicroseconds(1);  
   
    digitalWrite(fStepPin,HIGH);
    delayMicroseconds(fStepTime_microsec);
    digitalWrite(fStepPin,LOW);
    delayMicroseconds(fStepTime_microsec);
   
    unsigned long t = micros(); 
    if (fRealLastStep==0)
      fRealStepTime = GetSpeed();
    else 
      fRealStepTime =  t - fRealLastStep;    
    fRealLastStep = t;

    fPosition+=dir*fMicroSteps[fMicroStep];
   
    return dir*fMicroSteps[fMicroStep];

}   

long StepperClass::Roll(int dir)
{
      if ( micros() - fLastStep < fSpeed)
         return 0;     

      if (micros()-fRealLastStep < fMaxSpeedDelay) 
         return 0;

      long res = Step(dir);
      if (res!=0){
          if ((fLastStep==0)||(fIsDelayCompensation==0))
            fLastStep = fRealLastStep; 
         else  
            fLastStep += fSpeed;   //  Для компенсации пропущенных шагов  
      }

      return res;
}

void StepperClass::Stop()
{
      fLastStep = 0;
      fRealLastStep = 0;
}


void StepperClass::SetMicroStep(int new_micro_step)
{
    if (new_micro_step!=fMicroStep){
      switch (new_micro_step) {
        case MICROSTEP1:
         digitalWrite(fMS1Pin, LOW); 
         digitalWrite(fMS2Pin, LOW);
         digitalWrite(fMS3Pin, LOW);
         fMicroStep = new_micro_step;        
        break;
        case MICROSTEP2:
         digitalWrite(fMS1Pin, HIGH); 
         digitalWrite(fMS2Pin, LOW);
         digitalWrite(fMS3Pin, LOW);
         fMicroStep = new_micro_step;
        break;
        case MICROSTEP4:
         digitalWrite(fMS1Pin, LOW); 
         digitalWrite(fMS2Pin, HIGH);
         digitalWrite(fMS3Pin, LOW);
         fMicroStep = new_micro_step;
        break;
        case MICROSTEP8:
         digitalWrite(fMS1Pin, HIGH); 
         digitalWrite(fMS2Pin, HIGH);
         digitalWrite(fMS3Pin, LOW);
         fMicroStep = new_micro_step;
        break;
        case MICROSTEP16:
         if (fType==DRV8825){
           digitalWrite(fMS1Pin, LOW); 
           digitalWrite(fMS2Pin, LOW);
           digitalWrite(fMS3Pin, HIGH);
         } else if (fType==A4988){
           digitalWrite(fMS1Pin, HIGH); 
           digitalWrite(fMS2Pin, HIGH);
           digitalWrite(fMS3Pin, HIGH);
         }
         fMicroStep = new_micro_step;
        break;
        case MICROSTEP32:
         digitalWrite(fMS1Pin, HIGH); 
         digitalWrite(fMS2Pin, HIGH);
         digitalWrite(fMS3Pin, HIGH);
         fMicroStep = new_micro_step;
        break;
      }
      fStepsPerRevolution = fMicroSteps[fMicroStep]*fStepsPerRevolutionDefault;  // change this to fit the number of steps per revolution    
    }
}

long StepperClass::SetSpeed(long speed)
{
/*     if (speed>fMinSpeedDelay)
          fSpeed = fMinSpeedDelay;
     else if (speed<fMaxSpeedDelay)
          fSpeed = fMaxSpeedDelay;                  
     else  */
          fSpeed = speed;

     return fSpeed;
}


long StepperClass::GetSpeed()
{
     return fSpeed;
}

void StepperClass::SetCompensation(bool compensate)
{
     fIsDelayCompensation = true;               	
     fLastStep = 0;
}

bool StepperClass::GetCompensation()
{
     return fIsDelayCompensation;
}
