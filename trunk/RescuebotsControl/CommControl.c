/*
 * CommControl.c
 *
 *  Created on: May 30, 2012
 *      Author: root
 */

#include "CommControl.h"
#include "CoreControl.h"

commState cState;


char readCharBlock(void)
{
	while (getBufferLength()==0) {}
	return readChar();
}

void initCommControl(void)
{
	clearReceptionBuffer();
	//waiting for briefing
	show_WAITINGFORBRIEFING();
	//TODO: ADD A ELEVATOR WAITING SONG HERE
	cState = receiving;

	uint8_t x = (uint8_t) readCharBlock();
	uint8_t y = (uint8_t) readCharBlock();
	dir = (direction) readCharBlock();
	//uint8_t y = 0;
	//uint8_t x = 0;
	//dir = facingEast;
	cells[0].x = x;
	cells[0].y = y;
	cells[0].dir = dir;

	show_BRIEFINGRECEIVED(x,y,dir);

	cState = idle;
	mSleep(1000);
}

void sendData(void)
{
	clearReceptionBuffer();
	cState = sending;
	show_WAITINGFORDEBRIEFING();
	readCharBlock();
	mSleep(10);


	writeIntegerLength(totalCellsFindCourse,10,3);writeChar('\n');
	for(int i=0;i<totalCellsFindCourse;i++)
	{
		writeString(cells[i].north ? "NY" : "NN");writeChar('\n');
		writeString(cells[i].east ? "EY" : "EN");writeChar('\n');
		writeString(cells[i].south ? "SY" : "SN");writeChar('\n');
		writeString(cells[i].west ? "WY" : "WN");writeChar('\n');
		writeIntegerLength(cells[i].x,10,2);writeChar('\n');
		writeIntegerLength(cells[i].y,10,2);writeChar('\n');
		writeIntegerLength(cells[i].a,10,2);writeChar('\n');
		writeIntegerLength(cells[i].dir,10,2);writeChar('\n');
	}
	if (!softEndFind)
	{
		writeChar('F');writeChar('\n');
		cState = idle;
		return;
	}
	else
	{
		readCharBlock();

		mSleep(10);

		writeIntegerLength(totalCellsReturnCourse,10,3);writeChar('\n');
		for(int i=0;i<totalCellsReturnCourse;i++)
		{
			writeString(cells[i].north ? "NY" : "NN");writeChar('\n');
			writeString(cells[i].east ? "EY" : "EN");writeChar('\n');
			writeString(cells[i].south ? "SY" : "SN");writeChar('\n');
			writeString(cells[i].west ? "WY" : "WN");writeChar('\n');
			writeIntegerLength(cells[i].x,10,2);writeChar('\n');
			writeIntegerLength(cells[i].y,10,2);writeChar('\n');
			writeIntegerLength(cells[i].a,10,2);writeChar('\n');
		}
		if (!softEndReturn)
		{
			writeChar('F');writeChar('\n');
			cState = idle;
			return;
		}
	}

}


