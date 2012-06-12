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


void onEnterCellHandler(void)
{
	beep(100,10);
	show_ENTERCELL();

	updateWallData(); //add additional data to last cell.

	cellCounter++;

	//TODO: Set new X and Y depending on current dir!
	//cellCounter will from this point always be higher than 0.
	switch(dir)
	{
		case facingNorth: cells[cellCounter].y--;break;
		case facingEast: cells[cellCounter].x++;break;
		case facingSouth: cells[cellCounter].y++;break;
		case facingWest: cells[cellCounter].x--;break;
	}
}

void onLeaveCellHandler(void)
{
	show_LEAVECELL();
	updateWallData();

	mSleep(300);
	show_WHICHWALL(cells[cellCounter].north,cells[cellCounter].east,cells[cellCounter].south,cells[cellCounter].west);
	mSleep(300);

	beep(40,10);
	writeString("\nCELL:");
	writeInteger(cellCounter,10);
	writeChar('\n');
	writeString("North: ");
	if (cells[cellCounter].north) writeChar('Y');
	writeChar('\n');
	writeString("East: ");
	if (cells[cellCounter].east) writeChar('Y');
	writeChar('\n');
	writeString("South: ");
	if (cells[cellCounter].south) writeChar('Y');
	writeChar('\n');
	writeString("West: ");
	if (cells[cellCounter].west) writeChar('Y');
	writeChar('\n');
	writeString("Action: ");
	writeInteger(cells[cellCounter].a,10);
	writeChar('\n');
	writeString("X: ");
	writeInteger(cells[cellCounter].x,10);
	writeString(", Y: ");
	writeInteger(cells[cellCounter].y,10);
	writeChar('\n');
}

void initCoreControl(void)
{
	initLCD();
	initASI();
	initMoveControl();
	initPositionControl();

	//initCommControl();

	show_PLACEMEATPOSITION(cells[0].x,cells[0].y);
	//PRESS ANY KEY TO CONTINUE
	while(!getPressedKeyNumber()) {}

	mState = finding;//calibrating;
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


bool found = false;
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
		bool left = wallIsLeft();
		bool front = wallIsFront();
		bool right = wallIsRight();

		onLeaveCellHandler();
		if (!left)
		{
			cells[cellCounter].a = tLeft;
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
				show_ACTION(mForward);
				moveForward();
			}
			else
			{
				if (!right)
				{
					cells[cellCounter].a = tRight;
					show_ACTION(tRight);
					turnRight();
					incEnum(dir,3);
					moveForward();
				}
				else
				{
					show_ACTION(t180);
					turn180();
					decEnum(dir,3);
					decEnum(dir,3);
					moveForward();
				}
			}
		}
		onEnterCellHandler();
		//TODO:check if victim found (flag in corecontrol. set by eventhandler receiveRC5Data)
	}
	//moveForward();
	mState = returning;
}

/*
 * returnToStart
 *
 *      Author: Ron Engels
 */
void returnToStart(void)
{
	//TODO: Make returnToStart
	mState = undefined;
}


void updateCoreControl(void)
{
	switch (mState)
	{
		case calibrating: calibrate(); mState = finding;break;
		case finding: find();break;
		case returning: break;
	}
	updatePositionControl();
	updateMoveControl();
}
