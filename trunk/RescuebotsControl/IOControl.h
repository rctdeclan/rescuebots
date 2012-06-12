/*
 * LCDControl.h
 *
 *  Created on: May 31, 2012
 *      Author: Bart Peters & Daniel vd Besselaar
 */

#ifndef IOCONTROL_H_
#define IOCONTROL_H_


void show_WAITINGFORBRIEFING(void);
void show_BRIEFINGRECEIVED(uint8_t, uint8_t, uint8_t);
void show_PLACEMEATPOSITION(uint8_t, uint8_t, uint8_t);

void show_SPEED(uint8_t, uint8_t);
void show_STARTEDFINDING(void);
void show_ENTERCELL(void);

void show_LEAVECELL(void);
void show_ACTION(action a);

void show_WHICHWALL(bool, bool, bool, bool);

void show_LOOKINGFORBEACON(void);
void show_BEHOLDTHEBEACONOFLIGHT(void);
void show_NOBEACONFOUND(void);
#endif /* IOCONTROL_H_ */
