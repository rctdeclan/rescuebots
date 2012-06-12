/*
 * MoveControl.h
 *
 *  Created on: May 30, 2012
 *      Author: Ron Engels, Declan Bullock, Bart Peters
 */
#include "CoreControl.h"

#ifndef MOVECONTROL_H_
#define MOVECONTROL_H_

typedef enum {
	undefined,
	moving,
	turning,
	stationary
} kineticState;

kineticState kState;

void initMoveControl(void);
void updateMoveControl(void);

void moveForward(void);
void moveBack(void);

void turnLeft(void);
void turnRight(void);
void turn180(void);

void MoveControl_onEnterCell(void (*onEnterCellHandler)(void));
void MoveControl_onLeaveCell(void (*onLeaveCellHandler)(void));

bool wallIsLeft(void);
bool wallIsRight(void);
bool wallIsFront(void);

uint8_t cruiseSpeed;
int cellDist;

#endif /* MOVECONTROL_H_ */
