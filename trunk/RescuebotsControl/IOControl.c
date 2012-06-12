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
	clearLCD();
	showScreenLCD("Waiting for","briefing.");
}

void show_WAITINGFORDEBRIEFING(void)
{
	clearLCD();
	showScreenLCD("Waiting for","debriefing.");
}

void show_BRIEFINGRECEIVED(uint8_t x, uint8_t y, uint8_t dir)
{
	clearLCD();
	showScreenLCD("Briefing:","X:  Y:  D:  ");
	setCursorPosLCD(1,3);
	writeIntegerLCD(x,10);
	setCursorPosLCD(1,7);
	writeIntegerLCD(y,10);
	setCursorPosLCD(1,11);
	writeIntegerLCD(dir,10);

}

void show_PLACEMEATPOSITION(uint8_t x, uint8_t y, uint8_t dir)
{
	clearLCD();
	showScreenLCD("Place me at:","X:  Y:  D:  ");
	setCursorPosLCD(1,3);
	writeIntegerLCD(x,10);
	setCursorPosLCD(1,7);
	writeIntegerLCD(y,10);
	setCursorPosLCD(1,11);
	writeIntegerLCD(dir,10);
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

void show_LOOKINGFORBEACON(void)
{
	clearLCD();
	showScreenLCD("Looking for","beacon");
}

void show_BEHOLDTHEBEACONOFLIGHT(void)
{
	clearLCD();
	showScreenLCD("***Behold the***","BEACON of LIGHT");
}

void show_NOBEACONFOUND(void)
{
	clearLCD();
	showScreenLCD("No beacon","found.");
}
