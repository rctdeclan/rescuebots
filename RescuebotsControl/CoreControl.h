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

typedef struct
{
	bool north;
	bool east;
	bool south;
	bool west;
	int x;
	int y;
} cell;

cell cells[MAX_FIELD_SIZE * MAX_FIELD_SIZE];

void initCoreControl(void);
void updateCoreControl(void);

#endif /* CORECONTROL_H_ */
