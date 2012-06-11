/*
 * PositionControl.c
 *
 *  Created on: Jun 6, 2012
 *      Author: root
 */
#include "RP6Control_I2CMasterLib.h"
#include "ASI.h"
#include "MoveControl.h"

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

void acsStateChanged(void)
{
	if(kState==moving && (obstacle_left || obstacle_right))
	{
		beep(130,100);
		stop();
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

uint16_t lowerAvgWallBoundary = 500;
uint16_t higherAvgWallBoundary = 500;
uint16_t conditionedWallDistance;

bool wallIsLeft(void)
{
	//true if any of the left sensors report a value in between the range.

	uint16_t leftFront = readADC(ADC_5);
	uint16_t leftBack = readADC(ADC_2);

	uint16_t minLeft = ((leftFront > leftBack) ? leftFront : leftBack);

	return (minLeft >= lowerAvgWallBoundary && minLeft <=higherAvgWallBoundary);
}

bool wallIsRight(void)
{
	//true if any of the right sensors report a value in between the range.

	uint16_t rightFront = readADC(ADC_4);
	uint16_t rightBack = readADC(ADC_3);

	uint16_t minRight = ((rightFront > rightBack) ? rightFront : rightBack);

	return (minRight >= lowerAvgWallBoundary && minRight <=higherAvgWallBoundary);
}

bool wallIsFront(void)
{
	return obstacle_right || obstacle_left;
}

void initPositionControl(void)
{
	ACS_setStateChangedHandler(acsStateChanged);


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
