/*
 * IOControl.c
 *
 *  Created on: Jun 6, 2012
 *      Author: root
 */
#include "RP6ControlLib.h"
#include "CoreControl.h"

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


void show_SPEED(uint8_t left, uint8_t right)
{
	clearLCD();
	showScreenLCD("L:    R:","");
	setCursorPosLCD(1,3);
	writeIntegerLCD(left,10);
	setCursorPosLCD(1,10);
	writeIntegerLCD(right,10);
}

void show_STARTEDFINDING(void)
{
	clearLCD();
	showScreenLCD("Started Finding","Process");
}

void show_ENTERCELL(void)
{
	clearLCD();
	showScreenLCD("Entering Cell","");
}

void show_LEAVECELL(void)
{
	clearLCD();
	showScreenLCD("Leaving Cell","");
}

void show_ACTION(action a)
{
	clearLCD();
	switch (a)
	{
		case tLeft: showScreenLCD("Turning Left","");break;
		case mForward: showScreenLCD("Moving Forward","");break;
		case tRight: showScreenLCD("Turning Right","");break;
		case t180: showScreenLCD("Turning 180","");break;
	}
}

void show_WHICHWALL(bool north, bool east, bool south, bool west)
{
	clearLCD();
	if(north)
	{
		setCursorPosLCD(0,8);
		writeStringLCD("N");
	}
	if(east)
	{
		setCursorPosLCD(1,0);
		writeStringLCD("E");
	}
	if(south)
	{
		setCursorPosLCD(1,8);
		writeStringLCD("S");
	}
	if(west)
	{
		setCursorPosLCD(1,15);
		writeStringLCD("W");
	}
}

