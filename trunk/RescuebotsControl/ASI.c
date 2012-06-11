/*
 * ASI.c (Actuators and Sensors Interface)
 * Only for low-level sensor and actuator management.
 *
 *  Created on: May 30, 2012
 *      Author: root
 */
#include "RP6Control_I2CMasterLib.h"
#include "CoreControl.h"
#include "stdint.h"

uint8_t DDR[4] = { 0xB, };
uint8_t PORT[4] = {0xB, } ;

void setPortMode(char port, uint8_t pin, bool value)
{
	uint16_t chosenPort;
	switch(port)
	{
		case 'a': chosenPort = 0;break;
		case 'b': chosenPort = 1;break;
		case 'c': chosenPort = 2;break;
		case 'd': chosenPort = 3;break;
		default:return;
	}

	if (value==true)
	{//output
		DDR[chosenPort] |= pin;
	}
	else
	{//input
		DDR[chosenPort] &= ~pin;
	}
}

void setPortVal(char port, uint8_t pin, bool value)
{
	uint16_t chosenPort;
	switch(port)
	{
		case 'a': chosenPort = 0;break;
		case 'b': chosenPort = 1;break;
		case 'c': chosenPort = 2;break;
		case 'd': chosenPort = 3;break;
		default:return;
	}

	if (value==true)
	{//output
		PORT[chosenPort] |= pin;
	}
	else
	{//input
		PORT[chosenPort] &= ~pin;
	}
}

void initASI(void)
{
	DDR[0] = DDRA;
	DDR[1] = DDRB;
	DDR[2] = DDRC;
	DDR[3] = DDRD;

	PORT[0] = PORTA;
	PORT[1] = PORTB;
	PORT[2] = PORTC;
	PORT[3] = PORTD;
}
