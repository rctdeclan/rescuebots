/*
 * CoreControl.c
 *
 *  Created on: May 30, 2012
 *      Author: Ron Engels, Declan Bullock, Bart Peters
 */
#include "ASI.h"
#include "MoveControl.h"
#include "CommControl.h"
#include "PositionControl.h"
#include "RP6Control_I2CMasterLib.h"

modeState mState;
int dist360 = 2480;
int cellCounter = 0;
direction dir;
bool softEndFind = true;
bool softEndReturn = true;
int totalCellsFindCourse = 0;
int totalCellsReturnCourse = 0;
int beaconDetectedOnCell;
bool found = false;

void onEnterCellHandler(void)
{
	beep(100,10);
	show_ENTERCELL();

	updateWallData(); //add additional data to last cell.

	cellCounter++; //officially entered this cell.

	//cellCounter will from this point always be higher than 0.
	switch(dir)
	{
		case facingNorth: cells[cellCounter].y = cells[cellCounter-1].y - 1;break;
		case facingEast: cells[cellCounter].x = cells[cellCounter-1].x + 1;break;
		case facingSouth: cells[cellCounter].y = cells[cellCounter-1].y + 1;break;
		case facingWest: cells[cellCounter].x = cells[cellCounter-1].x - 1;break;
	}

	cells[cellCounter].dir = dir;
}

void onLeaveCellHandler(void)
{
	show_LEAVECELL();
	updateWallData();

	mSleep(300);
	show_WHICHWALL(cells[cellCounter].north,cells[cellCounter].east,cells[cellCounter].south,cells[cellCounter].west);
	mSleep(300);

	beep(40,10);
//	writeString("\nCELL:");
//	writeInteger(cellCounter,10);
//	writeChar('\n');
//	writeString("North: ");
//	if (cells[cellCounter].north) writeChar('Y');
//	writeChar('\n');
//	writeString("East: ");
//	if (cells[cellCounter].east) writeChar('Y');
//	writeChar('\n');
//	writeString("South: ");
//	if (cells[cellCounter].south) writeChar('Y');
//	writeChar('\n');
//	writeString("West: ");
//	if (cells[cellCounter].west) writeChar('Y');
//	writeChar('\n');
//	writeString("Action: ");
//	writeInteger(cells[cellCounter].a,10);
//	writeChar('\n');
//	writeString("X: ");
//	writeInteger(cells[cellCounter].x,10);
//	writeString(", Y: ");
//	writeInteger(cells[cellCounter].y,10);
//	writeChar('\n');
}

void initCoreControl(void)
{
	initLCD();
	initASI();
	initMoveControl();
	initPositionControl();


	initCommControl();

	show_PLACEMEATPOSITION(cells[0].x,cells[0].y,dir);
	//PRESS ANY KEY TO CONTINUE
	while(!getPressedKeyNumber()) {}

//	cells[0].north = true;
//	cells[0].east = true;
//	cells[0].south = false;
//	cells[0].west = false;
//	cells[0].x = 0;
//	cells[0].y = 0;
//	cells[0].a = tLeft;
//	cells[1].north = false;
//	cells[1].east = true;
//	cells[1].south = true;
//	cells[1].west = false;
//	cells[1].x = 1;
//	cells[1].y = 0;
//	cells[1].a = t180;
//	cells[2].north = false;
//	cells[2].east = false;
//	cells[2].south = false;
//	cells[2].west = true;
//	cells[2].x = 2;
//	cells[2].y = 0;
//	cells[2].a = tRight;
//	cells[3].north = false;
//	cells[3].east = true;
//	cells[3].south = false;
//	cells[3].west = false;
//	cells[3].x = 2;
//	cells[3].y = 1;
//	cells[3].a = tRight;
//	cells[4].north = false;
//	cells[4].east = true;
//	cells[4].south = true;
//	cells[4].west = false;
//	cells[4].x = 4;
//	cells[4].y = 5;
//	cells[4].a = mForward;
//
//	softEndFind=false;
//
//	sendData();
	mState = calibrating;
	//mState = undefined;

}


/*
 * costFunction & calibrate
 *
 *      Author: Declan Bullock
 */
int costFunction(int a[], int b[])
{
	int beta = 0;
	if (sizeof(a)!=sizeof(b)) return 16384;
	int length = sizeof(a)/sizeof(int);
	for (int j = 0; j < length; ++j)
	{
		beta = pow(a[j]-b[j],2);
	}
	return beta / (2*length);
}

void calibrate(void)
{
	//get current sensor values. save them.
	//save the sensor values, oh no!!!!
	int origin[4] = {readAvgADC(ADC_4,5),readAvgADC(ADC_3,5),readAvgADC(ADC_5,5),readAvgADC(ADC_2,5)};

	//turn a guess value for 360 degrees.
	move(40,LEFT,dist360,true);

	//on approach of end, read values again. Use alternative to gradient descent to optimize position.
	int num_iters = 100;
	int delta = 2;
	int totaldelta = 0;
	int cost;
	int lastCost = 9000; //initialize
	int alpha = 80;//learning rate
	for(int i=0;i<num_iters;i++)
	{

		int input[4] = {readAvgADC(ADC_4,5),readAvgADC(ADC_3,5),readAvgADC(ADC_5,5),readAvgADC(ADC_2,5)};
		cost = costFunction(input,origin);

		writeString_P("cost:");
		writeInteger(cost,10);
		writeString("\n");

		move(40,3 - (delta>0),abs(delta),true);
		totaldelta += delta;
		writeString_P("totaldelta:");
		writeInteger(totaldelta,10);
		writeString("\n");

		if (i==0) {lastCost = cost;continue;}
		delta = (abs(lastCost-cost)/alpha) * ((lastCost-cost>=0) ? 1 : -1);

		writeString_P("delta:");
		writeInteger(delta,10);
		writeString("\n");

		lastCost = cost;

		if (cost < 800/alpha) break;
	}
	totaldelta+=delta; //add last delta.
	dist360+=totaldelta;
	writeString_P("dist360:");
	writeInteger(dist360,10);
	writeString("\n");
}

void searchForBeacon(void)
{
	show_LOOKINGFORBEACON();
	mSleep(1500);
	if (beaconDetectedOnCell==cellCounter)
	{
		found = true;
		show_BEHOLDTHEBEACONOFLIGHT();
	}
	else
	{
		show_NOBEACONFOUND();
	}
}


/*
 * find
 *
 *      Author: Ron Engels, Bart Peters
 */
void find(void)
{
	show_STARTEDFINDING();
	while (!found)
	{
		checkProcessEndedAbruptly();
		bool left = wallIsLeft();
		bool front = wallIsFront();
		bool right = wallIsRight();

		if (!left)
		{
			cells[cellCounter].a = tLeft;
			onLeaveCellHandler();
			show_ACTION(tLeft);
			turnLeft();
			decEnum(dir,3);
			moveForward();
		}
		else
		{
			if(!front)
			{
				cells[cellCounter].a = mForward;
				onLeaveCellHandler();
				show_ACTION(mForward);
				moveForward();
			}
			else
			{
				if (!right)
				{
					cells[cellCounter].a = tRight;
					onLeaveCellHandler();
					show_ACTION(tRight);
					turnRight();
					incEnum(dir,3);
					moveForward();
				}
				else
				{
					cells[cellCounter].a = t180;
					onLeaveCellHandler();
					show_ACTION(t180);
					turn180();
					decEnum(dir,3);
					decEnum(dir,3);
					moveForward();
				}
			}
		}
		onEnterCellHandler();
		searchForBeacon();
		//TODO:check if victim found (flag in corecontrol. set by eventhandler receiveRC5Data)
	}
	totalCellsFindCourse = cellCounter;
}

/*
 * returnToStart
 *
 *      Author: Ron Engels
 */
void returnToStart(void)
{

	//make a seperate array of commands and extract data from the cells array into it.
	//add a mForward after every tLeft, tRight and t180.
	//apply grammar rules dependent on their priority.
	//execute

	cellCounter = 0;
	//set every returnscells[i] while moving through the course, dependent on the actions array.




	//______________________
	//first reverse the cell array.
//	for (int i = 0;i<totalCellsFindCourse;i++)
//	{
//		returncells[i] = cells[totalCellsFindCourse-i-1];
//		switch (returncells[i].a)
//		{
//			case tLeft: returncells[i].a = tRight;break;
//			case tRight: returncells[i].a = tLeft;break;
//		}
//	}

//	while(true)
//	{
//		checkProcessEndedAbruptly();
//	}
	//TODO: Make returnToStart
}

void end(void)
{
	stop();
	mState = undefined;
	kState = limbo;
	sendData();
}

void checkProcessEndedAbruptly(void)
{
	if(getPressedKeyNumber())
	{
		softEndFind = (mState == finding);
		softEndReturn = (mState == returning);
		end();
	}
}


void updateCoreControl(void)
{
	switch (mState)
	{
		case calibrating: calibrate(); mState = finding;break;
		case finding: find(); mState = returning;break;
		case returning: returnToStart(); mState = undefined; end(); break;
	}
	updatePositionControl();
	updateMoveControl();
}
