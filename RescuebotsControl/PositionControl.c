/*
 * PositionControl.c
 *
 *  Created on: Jun 6, 2012
 *      Author: root
 */
#include "RP6Control_I2CMasterLib.h"
#include "ASI.h"
#include "MoveControl.h"
#include "CoreControl.h"

bool detected = false;
bool detecting = false;

void I2C_requestedDataReady(uint8_t dataRequestID)
{
	checkRP6Status(dataRequestID);
}

void I2C_transmissionError(uint8_t errorState)
{
	writeString_P("\nI2C ERROR - TWI STATE: 0x");
	writeInteger(errorState, HEX);
	writeChar('\n');
}

void receiveRC5Data(void)
{
	if (kState == stationary && detecting)
	{
		detected = true;
	}
}

void acsStateChanged(void)
{
	if(kState==moving && (obstacle_left && obstacle_right))
	{
		beep(190,100);
	}
}

uint16_t readAvgADC(uint8_t channel, uint8_t samples)
{
	uint8_t c = samples;
	uint16_t val=0;
	while(c--)
		val+=readADC(channel);
	return val/samples;
}


//average wall is 160 in distance.
uint16_t higherAvgWallBoundary = 120;
uint16_t conditionedWallDistance;

bool wallIsLeft(void)
{
	//true if any of the left sensors report a value in between the range.

	uint16_t leftFront = readADC(ADC_5);
	uint16_t leftBack = readADC(ADC_2);

	uint16_t minLeft = ((leftFront > leftBack) ? leftFront : leftBack);

	return (minLeft >=higherAvgWallBoundary);
}

bool wallIsRight(void)
{
	//true if any of the right sensors report a value in between the range.

	uint16_t rightFront = readADC(ADC_4);
	uint16_t rightBack = readADC(ADC_3);

	uint16_t minRight = ((rightFront > rightBack) ? rightFront : rightBack);

	return (minRight >=higherAvgWallBoundary);
}

bool wallIsFront(void)
{
	return obstacle_left && obstacle_right;
}

void initPositionControl(void)
{
	ACS_setStateChangedHandler(acsStateChanged);
	IRCOMM_setRC5DataReadyHandler(receiveRC5Data);

	I2CTWI_initMaster(100);
	I2CTWI_setTransmissionErrorHandler(I2C_transmissionError);
	I2CTWI_setRequestedDataReadyHandler(I2C_requestedDataReady);


	mSleep(1000);

	// Setup ACS power:
	I2CTWI_transmit3Bytes(I2C_RP6_BASE_ADR, 0, CMD_SET_ACS_POWER, ACS_PWR_MED);

	setPortMode('a',ADC5,false);
	setPortMode('a',ADC4,false);

	setPortMode('a',ADC3,false);
	setPortMode('a',ADC2,false);
}


void updatePositionControl(void)
{
	task_checkINT0();
	task_I2CTWI();
}

void updateWallData(void)
{
	bool left = wallIsLeft();
	bool front = wallIsFront();
	bool right = wallIsRight();
	if (mState == finding)
	{
		switch(dir)
		{
			case facingNorth: 	cells[cellCounter].west=left;
								cells[cellCounter].north=front;
								cells[cellCounter].east=right; break;
			case facingEast: 	cells[cellCounter].north=left;
								cells[cellCounter].east=front;
								cells[cellCounter].south=right; break;
			case facingSouth: 	cells[cellCounter].east=left;
								cells[cellCounter].south=front;
								cells[cellCounter].west=right; break;
			case facingWest: 	cells[cellCounter].south=left;
								cells[cellCounter].west=front;
								cells[cellCounter].north=right; break;
		}
	}
	else if (mState == returning)
	{
		switch(dir)
		{
			case facingNorth: 	invCells[cellCounter].west=left;
								invCells[cellCounter].north=front;
								invCells[cellCounter].east=right; break;
			case facingEast: 	invCells[cellCounter].north=left;
								invCells[cellCounter].east=front;
								invCells[cellCounter].south=right; break;
			case facingSouth: 	invCells[cellCounter].east=left;
								invCells[cellCounter].south=front;
								invCells[cellCounter].west=right; break;
			case facingWest: 	invCells[cellCounter].south=left;
								invCells[cellCounter].west=front;
								invCells[cellCounter].north=right; break;
		}
	}
}
