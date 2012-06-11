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
int dist360 = 2800;
int cellCounter = 0;

void onEnterCellHandler(void)
{
	//TODO:(Catch what to do if a cell is entered.)
	//push new cell to cells array.
	//cells[cellCounter].north = w
	      cellCounter++;
}

void onLeaveCellHandler(void)
{
	//(Catch what to do if a cell is being left.)
}

void initCoreControl(void)
{

	initLCD();
	initASI();
	initMoveControl();
	initPositionControl();

	MoveControl_onEnterCell(onEnterCellHandler);
	MoveControl_onLeaveCell(onLeaveCellHandler);

	initCommControl();

	show_PLACEMEATPOSITION(cells[0].x,cells[0].y);
	//PRESS ANY KEY TO CONTINUE
	while(!getPressedKeyNumber()) {}

	mState = calibrating;
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
	int lastCost = 9000; //intialize
	int alpha = 20;//learning rate
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
		delta = (abs(lastCost-cost)/alpha) * ((lastCost-cost>0) ? 1 : -1);

		writeString_P("delta:");
		writeInteger(delta,10);
		writeString("\n");

		lastCost = cost;

		if (cost < 1800/alpha) break;
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
	while (!found)
	{
		if (!wallIsLeft())
		{
			turnLeft();
			moveForward();
		}
		else
		{
			if(!wallIsFront())
			{
				moveForward();
			}
			else
			{
				if (!wallIsRight())
				{
					turnRight();
					moveForward();
				}
				else
				{
					turnRight();
					turnRight();
					moveForward();
				}
			}
		}
		//TODO:check if victim found (flag in corecontrol. set by eventhandler receiveRC5Data)
	}
	mState = returning;
}

/*
 * returnToStart
 *
 *      Author: Ron Engels
 */
void returnToStart(void)
{

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
