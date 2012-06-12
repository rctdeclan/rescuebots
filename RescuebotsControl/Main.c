#include "CoreControl.h"
#include "RP6Control_I2CMasterLib.h"


void initMain(void)
{
	initRP6Control(); // Always call this first! The Processor will not work
					  // correctly otherwise.

	initCoreControl();
	writeString_P("\n\nRescuebots T21\n");
	setLEDs(0b1111);

	showScreenLCD("################", "################");

	// Play a sound to indicate that our program starts:
	sound(100,40,64);
	sound(170,40,0);
	mSleep(400);
	setLEDs(0b0000);

	showScreenLCD("Rescuebots", "Fontys ICT T21");
	mSleep(1000);
}

void updateMain(void)
{
	updateCoreControl();
}

int main(void)
{
	initMain();

	for(;;)
		updateMain();
	return 0;
}
