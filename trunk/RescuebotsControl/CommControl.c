/*
 * CommControl.c
 *
 *  Created on: May 30, 2012
 *      Author: root
 */

#include "CommControl.h"
#include "CoreControl.h"

commState cState;

void initCommControl(void)
{
	clearReceptionBuffer();
	//waiting for briefing
	show_WAITINGFORBRIEFING();
	//TODO: ADD A ELEVATOR WAITING SONG HERE
	while (getBufferLength()==0) {} //block
	cState = receiving;

	uint8_t x = readChar();
	uint8_t y = readChar();
	//dir = readChar();
	dir = facingNorth;//TODO: Remove this hardcode
	cells[0].x = x;
	cells[0].y = y;

	show_BRIEFINGRECEIVED(x,y);

	cState = idle;
	mSleep(1000);
}
