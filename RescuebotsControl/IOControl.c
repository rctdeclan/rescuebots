/*
 * IOControl.c
 *
 *  Created on: Jun 6, 2012
 *      Author: root
 */
#include "RP6Control_I2CMasterLib.h"

void show_WAITINGFORBRIEFING(void)
{
	showScreenLCD("Waiting for","briefing.");
}

void show_BRIEFINGRECEIVED(uint8_t x, uint8_t y)
{
	showScreenLCD("Briefing:","");
}

void show_PLACEMEATPOSITION(uint8_t x, uint8_t y)
{
	showScreenLCD("Place me at:","");
}
