/*
 * CommControl.h
 *
 *  Created on: May 30, 2012
 *      Author: root
 */

#ifndef COMMCONTROL_H_
#define COMMCONTROL_H_


#include "CoreControl.h"
#include "IOControl.h"
#include "RP6Control_I2CMasterLib.h"

typedef enum
{
	idle,
	receiving,
	sending
} commState;

void initCommControl(void);

#endif /* COMMCONTROL_H_ */
