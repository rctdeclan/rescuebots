/*
 * CoreControl.h
 *
 *  Created on: May 30, 2012
 *      Author: Declan Bullock
 */

#ifndef CORECONTROL_H_
#define CORECONTROL_H_

#define MAX_FIELD_SIZE 10
int dist360;

typedef enum
{
	limbo,
	calibrating,
	finding,
	returning,
} modeState;

#define true 1
#define false 0
typedef char bool;

typedef enum
{
	tLeft,
	tRight,
	mForward,
	t180
} action;

typedef struct
{
	bool north;
	bool east;
	bool south;
	bool west;
	int x;
	int y;
	action a;
} cell;

typedef enum
{
	facingNorth,
	facingEast,
	facingSouth,
	facingWest
} direction;


#define incEnum(e,max) if(e==max) e=0; else e++;
#define decEnum(e,max) if(e==0) e=max; else e--;

cell cells[MAX_FIELD_SIZE * MAX_FIELD_SIZE];
direction dir;
int cellCounter;

void initCoreControl(void);
void updateCoreControl(void);

#endif /* CORECONTROL_H_ */