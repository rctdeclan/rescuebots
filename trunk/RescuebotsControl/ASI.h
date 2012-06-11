/*
 * ASI.h
 *
 *  Created on: May 30, 2012
 *      Author: root
 */
#include "CoreControl.h"
#include "stdint.h"

#ifndef ASI_H_
#define ASI_H_

void initASI(void);
void setPortMode(char port, uint8_t pin, bool value);
void setPortVal(char port, uint8_t pin, bool value);

#endif /* ASI_H_ */
