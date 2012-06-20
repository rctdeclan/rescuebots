/*
 * MoveControl.h
 *
 *  Created on: May 30, 2012
 *      Author: Ron Engels, Declan Bullock, Bart Peters
 */
#include "RP6Control_I2CMasterLib.h"
#include "ASI.h"
#include "MoveControl.h"
#include "PositionControl.h"
#include "IOControl.h"


kineticState kState;
int cellDist = 1800;
uint8_t cruiseSpeed = 40;
uint16_t dirErrorFactor = 50;
uint16_t distErrorFactor = 8;



void initMoveControl(void)
{
	kState = stationary;
	conditionedWallDistance = 160;

}

void updateMoveControl(void)
{
	switch(kState)
	{
		case stationary: break;
		case moving: break;
		case turning: break;
	}
}

//void MoveControl_onEnterCell_DUMMY(void){}
//static void (*MoveControl_onEnterCellHandler)(void) = MoveControl_onEnterCell_DUMMY;
//void MoveControl_onEnterCell(void (*onEnterCellHandler)(void))
//{
//	MoveControl_onEnterCellHandler = onEnterCellHandler;
//}
//
//void MoveControl_onLeaveCell_DUMMY(void){}
//static void (*MoveControl_onLeaveCellHandler)(void) = MoveControl_onLeaveCell_DUMMY;
//void MoveControl_onLeaveCell(void (*onLeaveCellHandler)(void))
//{
//	MoveControl_onLeaveCellHandler = onLeaveCellHandler;
//}

/*
 * moveForward
 *
 *      Author: Ron Engels, Declan Bullock
 */
void moveForward()
{
	kState = moving;
	int leftDirError = 0;
	int rightDirError = 0;
	int leftDistError = 0;
	int rightDistError = 0;
	bool leftFrontExists = false;
	bool leftBackExists = false;
	bool rightFrontExists = false;
	bool rightBackExists = false;

	move(cruiseSpeed,FWD,cellDist,false);


	mSleep(100); //wait otherwise movementComplete flag will still be true.

	while(!drive_status.movementComplete)
	{

		int16_t leftFront = readAvgADC(ADC_5,10);
		int16_t leftBack = readAvgADC(ADC_2,10);
		int16_t rightFront = readAvgADC(ADC_4,10);
		int16_t rightBack = readAvgADC(ADC_3,10);

		leftFrontExists = (leftFront>=higherAvgWallBoundary);
		leftBackExists = (leftBack>=higherAvgWallBoundary);
		rightFrontExists = (rightFront>=higherAvgWallBoundary);
		rightBackExists = (rightBack>=higherAvgWallBoundary);

//		writeString("LFront:");
//		writeInteger(leftFrontExists,10);
//		writeString(" RFront:");
//		writeInteger(rightFrontExists,10);
//		writeChar('\n');
//		writeString("LBack:");
//		writeInteger(leftBackExists,10);
//		writeString(" RBack:");
//		writeInteger(rightBackExists,10);
//		writeChar('\n');

//		writeString("LFront:");
//		writeInteger(leftFront,10);
//		writeString(" RFront:");
//		writeInteger(rightFront,10);
//		writeChar('\n');
//		writeString("LBack:");
//		writeInteger(leftBack,10);
//		writeString(" RBack:");
//		writeInteger(rightBack,10);
//		writeChar('\n');

		if (leftFrontExists && leftBackExists)
		{
			leftDirError = (leftFront - leftBack) / dirErrorFactor;
			if (rightFrontExists) leftDistError = (leftFront - rightFront) / distErrorFactor;
			else
			if (rightBackExists) leftDistError = (leftBack - rightBack) / distErrorFactor;
			else
				leftDistError = (leftFront - conditionedWallDistance) / distErrorFactor;
		}
		else
		{
			leftDirError=0;
		}

		if (rightFrontExists && rightBackExists)
		{
			rightDirError = (rightFront - rightBack) / dirErrorFactor;
			if (leftFrontExists) rightDistError = (rightFront - leftFront) / distErrorFactor;
			else
			if (leftBackExists) rightDistError = (rightBack - leftBack) / distErrorFactor;
			else
				rightDistError = (rightFront - conditionedWallDistance) / distErrorFactor;
		}
		else
		{
			rightDirError = 0;
		}

//		writeString("LDir:");
//		writeInteger(leftDirError,10);
//		writeString(" RDir:");
//		writeInteger(rightDirError,10);
//		writeChar('\n');
//
//		writeString("LDist:");
//		writeInteger(leftDistError,10);
//		writeString(" RDist:");
//		writeInteger(rightDistError,10);
//		writeChar('\n');

		if (rightFrontExists && rightBackExists && leftFrontExists && leftBackExists)
		{
			conditionedWallDistance = ((rightFront+leftFront)/2 + (rightBack+leftBack)/2)/2;
		}

		if (!rightFrontExists && !leftFrontExists && rightBackExists && leftBackExists) //if entering no-wall zone, do a last error fix.
		{
			leftDistError = (leftBack - rightBack) / distErrorFactor;
			rightDistError = (rightBack - leftBack) / distErrorFactor;
		}

//		writeString("CWD:");
//		writeInteger(conditionedWallDistance,10);
//		writeChar('\n');

		//change individual motor speed according to Sharp data.
		moveAtSpeed(cruiseSpeed+leftDirError+leftDistError,cruiseSpeed+rightDirError+rightDistError);

		//if obstacle detected, stop (This is handled in the ACS event handler)


		task_checkINT0();
		task_I2CTWI();
		//mSleep(20);
		checkProcessEndedAbruptly();
	}
	stop();
	//set state to stationary.
	kState = stationary;
}

void turnLeft(void)
{
	//set state to turning
	kState = turning;

	//start turning left
	rotate(cruiseSpeed,LEFT,dist360/25,true);

	//set state to stationary
	kState = stationary;
}
void turnRight(void)
{

	//set state to turning
	kState = turning;

	//start turning right
	rotate(cruiseSpeed,RIGHT,dist360/25,true);

	//set state to stationary
	kState = stationary;
}
void turn180(void)
{

	//set state to turning
	kState = turning;

	//start turning right
	rotate(cruiseSpeed,LEFT,2*dist360/25,true);

	//set state to stationary
	kState = stationary;
}
