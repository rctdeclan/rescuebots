/*
 * PositionControl.h
 *
 *  Created on: Jun 6, 2012
 *      Author: Declan Bullock, Bart Peters
 */

#ifndef POSITIONCONTROL_H_
#define POSITIONCONTROL_H_

uint16_t lowerAvgWallBoundary;
uint16_t higherAvgWallBoundary;
uint16_t conditionedWallDistance;
bool wallIsLeft(void);
bool wallIsFront(void);
bool wallIsRight(void);
void initPositionControl(void);
void updatePositionControl(void);
uint16_t readAvgADC(uint8_t channel, uint8_t samples);
bool detecting;
bool detected;
#endif /* POSITIONCONTROL_H_ */
