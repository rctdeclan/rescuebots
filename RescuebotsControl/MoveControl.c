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


kineticState kState;
int cellDist = 5000;
uint8_t cruiseSpeed = 40;
uint8_t dirErrorFactor = 6;
uint8_t distErrorFactor = 6;



void initMoveControl(void)
{
	kState = stationary;
	conditionedWallDistance = (higherAvgWallBoundary+lowerAvgWallBoundary)/2;

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

void MoveControl_onEnterCell_DUMMY(void){}
static void (*MoveControl_onEnterCellHandler)(void) = MoveControl_onEnterCell_DUMMY;
void MoveControl_onEnterCell(void (*onEnterCellHandler)(void))
{
	MoveControl_onEnterCellHandler = onEnterCellHandler;
}

void MoveControl_onLeaveCell_DUMMY(void){}
static void (*MoveControl_onLeaveCellHandler)(void) = MoveControl_onLeaveCell_DUMMY;
void MoveControl_onLeaveCell(void (*onLeaveCellHandler)(void))
{
	MoveControl_onLeaveCellHandler = onLeaveCellHandler;
}

/*
 * moveForward
 *
 *      Author: Ron Engels, Declan Bullock
 */
void moveForward(void)
{
	kState = moving;

	//call onLeaveCellHandler
	MoveControl_onLeaveCellHandler();

	move(cruiseSpeed,FWD,cellDist,false);


	uint8_t leftDirError = 0;
	uint8_t rightDirError = 0;
	uint8_t leftDistError = 0;
	uint8_t rightDistError = 0;
	bool leftFrontExists = false;
	bool leftBackExists = false;
	bool rightFrontExists = false;
	bool rightBackExists = false;
	while(!drive_status.movementComplete)
	{

		uint16_t leftFront = readADC(ADC_5);
		uint16_t leftBack = readADC(ADC_2);
		uint16_t rightFront = readADC(ADC_4);
		uint16_t rightBack = readADC(ADC_3);

		leftFrontExists = (leftFront>=lowerAvgWallBoundary && leftFront<=higherAvgWallBoundary);
		leftBackExists = (leftBack>=lowerAvgWallBoundary && leftBack<=higherAvgWallBoundary);
		rightFrontExists = (rightFront>=lowerAvgWallBoundary && rightFront<=higherAvgWallBoundary);
		rightBackExists = (rightBack>=lowerAvgWallBoundary && rightBack<=higherAvgWallBoundary);

		if (leftFrontExists && leftBackExists)
		{
			leftDirError = (leftFront - leftBack) / dirErrorFactor;
			if (rightFrontExists) leftDistError = (leftFront - rightFront) / distErrorFactor;
			else
			if (rightBackExists) leftDistError = (leftBack - rightBack) / distErrorFactor;
			else
				leftDistError = (leftFront - conditionedWallDistance);
		}

		if (rightFrontExists && rightBackExists)
		{
			rightDirError = (rightFront - rightBack) / dirErrorFactor;
			if (leftFrontExists) rightDistError = (rightFront - leftFront) / distErrorFactor;
			else
			if (leftBackExists) rightDistError = (rightBack - leftBack) / distErrorFactor;
			else
				rightDistError = (rightFront - conditionedWallDistance);
		}

		if (rightFrontExists && rightBackExists && leftFrontExists && leftBackExists)
		{
			conditionedWallDistance = ((rightFront+leftFront)/2 + (rightBack+leftBack)/2)/2;
		}


		//change individual motor speed according to Sharp data.
		moveAtSpeed(cruiseSpeed+leftDirError+leftDistError,cruiseSpeed+rightDirError+rightDistError);

		//if obstacle detected, stop (This is handled in the ACS event handler)


		task_checkINT0();
		task_I2CTWI();
	}

	//set state to stationary.
	kState = stationary;

	//call onEnterCellHandler
	MoveControl_onEnterCellHandler();
}

void turnLeft(void)
{
	//set state to turning
	kState = turning;

	//start turning left
	rotate(cruiseSpeed,LEFT,dist360/4,true);

	//set state to stationary
	kState = stationary;
}
void turnRight(void)
{

	//set state to turning
	kState = turning;

	//start turning right
	rotate(cruiseSpeed,RIGHT,dist360/4,true);

	//set state to stationary
	kState = stationary;
}
