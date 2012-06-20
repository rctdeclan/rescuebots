/*
 * CoreControl.c
 *
 *  Created on: May 30, 2012
 *      Author: Ron Engels, Declan Bullock, Bart Peters
 */
#include "ASI.h"
#include "MoveControl.h"
#include "CommControl.h"
#include "PositionControl.h"
#include "RP6Control_I2CMasterLib.h"

modeState mState;
int dist360 = 1900;
int cellCounter = 0;
direction dir;
bool softEndFind = true;
bool softEndReturn = true;
int totalCellsFindCourse = 0;
int totalCellsReturnCourse = 0;
int beaconDetectedOnCell;
bool found = false;


void end(void)
{
	if (mState == finding) totalCellsFindCourse = cellCounter;
	if (mState == returning) totalCellsReturnCourse = cellCounter;
	stop();
	mState = undefined;
	kState = limbo;
	sendData();
}

void checkProcessEndedAbruptly(void)
{
	if(getPressedKeyNumber())
	{
		softEndFind = (mState == finding);
		softEndReturn = (mState == returning);
		end();
	}
}


void onEnterCellHandler(void)
{
	beep(100,10);
	show_ENTERCELL();

	updateWallData(); //add additional data to last cell.

	cellCounter++; //officially entered this cell.

	//cellCounter will from this point always be higher than 0.
	mSleep(300);
	if (mState==finding)
	{
		switch(dir)
		{
			case facingNorth: cells[cellCounter].y = cells[cellCounter-1].y - 1;break;
			case facingEast: cells[cellCounter].x = cells[cellCounter-1].x + 1;break;
			case facingSouth: cells[cellCounter].y = cells[cellCounter-1].y + 1;break;
			case facingWest: cells[cellCounter].x = cells[cellCounter-1].x - 1;break;
		}
	}
	else if (mState==returning)
	{
		switch(dir)
		{
			case facingNorth: invCells[cellCounter].y = invCells[cellCounter-1].y - 1;break;
			case facingEast: invCells[cellCounter].x = invCells[cellCounter-1].x + 1;break;
			case facingSouth: invCells[cellCounter].y = invCells[cellCounter-1].y + 1;break;
			case facingWest: invCells[cellCounter].x = invCells[cellCounter-1].x - 1;break;
		}
	}


	cells[cellCounter].dir = dir;
}

void onLeaveCellHandler(void)
{
	show_LEAVECELL();
	updateWallData();

	mSleep(300);
	if (mState==finding)
		show_WHICHWALL(cells[cellCounter].north,cells[cellCounter].east,cells[cellCounter].south,cells[cellCounter].west);
	else if (mState==returning)
		show_WHICHWALL(invCells[cellCounter].north,invCells[cellCounter].east,invCells[cellCounter].south,invCells[cellCounter].west);

	mSleep(300);

	beep(40,10);
//	writeString("\nCELL:");
//	writeInteger(cellCounter,10);
//	writeChar('\n');
//	writeString("North: ");
//	if (cells[cellCounter].north) writeChar('Y');
//	writeChar('\n');
//	writeString("East: ");
//	if (cells[cellCounter].east) writeChar('Y');
//	writeChar('\n');
//	writeString("South: ");
//	if (cells[cellCounter].south) writeChar('Y');
//	writeChar('\n');
//	writeString("West: ");
//	if (cells[cellCounter].west) writeChar('Y');
//	writeChar('\n');
//	writeString("Action: ");
//	writeInteger(cells[cellCounter].a,10);
//	writeChar('\n');
//	writeString("X: ");
//	writeInteger(cells[cellCounter].x,10);
//	writeString(", Y: ");
//	writeInteger(cells[cellCounter].y,10);
//	writeChar('\n');
}

void initCoreControl(void)
{
	initLCD();
	initASI();
	initMoveControl();
	initPositionControl();


	initCommControl();

	show_PLACEMEATPOSITION(cells[0].x,cells[0].y,dir);
	//PRESS ANY KEY TO CONTINUE
	while(getPressedKeyNumber()!=1) {}

//	cells[0].north = true;
//	cells[0].east = true;
//	cells[0].south = false;
//	cells[0].west = false;
//	cells[0].x = 0;
//	cells[0].y = 0;
//	cells[0].a = tLeft;
//	cells[1].north = false;
//	cells[1].east = true;
//	cells[1].south = true;
//	cells[1].west = false;
//	cells[1].x = 1;
//	cells[1].y = 0;
//	cells[1].a = t180;
//	cells[2].north = false;
//	cells[2].east = false;
//	cells[2].south = false;
//	cells[2].west = true;
//	cells[2].x = 2;
//	cells[2].y = 0;
//	cells[2].a = tRight;
//	cells[3].north = false;
//	cells[3].east = true;
//	cells[3].south = false;
//	cells[3].west = false;
//	cells[3].x = 2;
//	cells[3].y = 1;
//	cells[3].a = tRight;
//	cells[4].north = false;
//	cells[4].east = true;
//	cells[4].south = true;
//	cells[4].west = false;
//	cells[4].x = 4;
//	cells[4].y = 5;
//	cells[4].a = mForward;
//
//	softEndFind=false;
//	totalCellsFindCourse = 5;
//
//	sendData();
	//mState = calibrating;
	mState = finding;

}


/*
 * costFunction & calibrate
 *
 *      Author: Declan Bullock
 */
int costFunction(int a[], int b[])
{
	int beta = 0;
	if (sizeof(a)!=sizeof(b)) return 16384;
	int length = sizeof(a)/sizeof(int);
	for (int j = 0; j < length; ++j)
	{
		beta = pow(a[j]-b[j],2);
	}
	return beta / (2*length);
}

void calibrate(void)
{
	//get current sensor values. save them.
	//save the sensor values, oh no!!!!
	int origin[4] = {readAvgADC(ADC_4,5),readAvgADC(ADC_3,5),readAvgADC(ADC_5,5),readAvgADC(ADC_2,5)};

	//turn a guess value for 360 degrees.
	move(40,LEFT,dist360,true);

	//on approach of end, read values again. Use alternative to gradient descent to optimize position.
	int num_iters = 100;
	int delta = 2;
	int totaldelta = 0;
	int cost;
	int lastCost = 9000; //initialize
	int alpha = 80;//learning rate
	for(int i=0;i<num_iters;i++)
	{

		int input[4] = {readAvgADC(ADC_4,5),readAvgADC(ADC_3,5),readAvgADC(ADC_5,5),readAvgADC(ADC_2,5)};
		cost = costFunction(input,origin);

		writeString_P("cost:");
		writeInteger(cost,10);
		writeString("\n");

		move(40,3 - (delta>0),abs(delta),true);
		totaldelta += delta;
		writeString_P("totaldelta:");
		writeInteger(totaldelta,10);
		writeString("\n");

		if (i==0) {lastCost = cost;continue;}
		delta = (abs(lastCost-cost)/alpha) * ((lastCost-cost>=0) ? 1 : -1);

		writeString_P("delta:");
		writeInteger(delta,10);
		writeString("\n");

		lastCost = cost;

		if (cost < 800/alpha) break;
	}
	totaldelta+=delta; //add last delta.
	dist360+=totaldelta;
	writeString_P("dist360:");
	writeInteger(dist360,10);
	writeString("\n");
}

void searchForBeacon(void)
{
	show_LOOKINGFORBEACON();
	detecting = true;
	mSleep(1500);
	detected = (getPressedKeyNumber() == 2);
	if (detected)
	{
		found = true;
		show_BEHOLDTHEBEACONOFLIGHT();
	}
	else
	{
		show_NOBEACONFOUND();
	}
	detecting = false;
	mSleep(500);
}


/*
 * find
 *
 *      Author: Ron Engels, Bart Peters
 */
void find(void)
{
	show_STARTEDFINDING();
	while (!found)
	{
		checkProcessEndedAbruptly();
		bool left = wallIsLeft();
		bool front = wallIsFront();
		bool right = wallIsRight();

		if (!left)
		{
			cells[cellCounter].a = tLeft;
			onLeaveCellHandler();
			show_ACTION(tLeft);
			turnLeft();
			decEnum(dir,3);
			moveForward();
		}
		else
		{
			if(!front)
			{
				cells[cellCounter].a = mForward;
				onLeaveCellHandler();
				show_ACTION(mForward);
				moveForward();
			}
			else
			{
				if (!right)
				{
					cells[cellCounter].a = tRight;
					onLeaveCellHandler();
					show_ACTION(tRight);
					turnRight();
					incEnum(dir,3);
					moveForward();
				}
				else
				{
					cells[cellCounter].a = t180;
					onLeaveCellHandler();
					show_ACTION(t180);
					turn180();
					decEnum(dir,3);
					decEnum(dir,3);
					moveForward();
				}
			}
		}
		onEnterCellHandler();
		searchForBeacon();
		//TODO:check if victim found (flag in corecontrol. set by eventhandler receiveRC5Data)
	}
	totalCellsFindCourse = cellCounter;
}

void removeEmptyElements(action *array, int * totalActions)
{
	int index = 0;
	for (int j = 0; j<*totalActions;j++)
	{
		if (array[j] == EMPTY)
		{
			*totalActions = *totalActions-1;
			index = j;
			for (int i = index; i < sizeof(array); i++)
			{
				array[i] = array[i+1];
			}
			array[sizeof(array)-1] = EMPTY;
		}
		if(array[j] == EMPTY) {j--;}
	}
}

/*
 * returnToStart
 *
 *      Author: Ron Engels
 */
void returnToStart(void)
{
//	//first move 180, to turn around fully;
//	show_ACTION(t180);
//	turn180();
//	decEnum(did,3);
//	decEnum(dir,3);
//
//
	action invActions[MAX_FIELD_SIZE*MAX_FIELD_SIZE];
	int totalActions;
	int i = 0;

	{
		action actions[MAX_FIELD_SIZE*MAX_FIELD_SIZE];
		//make a seperate array of commands and extract data from the cells array into it.
		int offset = 0;
		for(i = 0; i<totalCellsFindCourse+offset;i++)
		{
			actions[i] = cells[i-offset].a;
			if (actions[i]==tLeft || actions[i]==tRight || actions[i]==t180)
			{
				if (actions[i]==tLeft) actions[i]=tRight;
				if (actions[i]==tRight) actions[i]=tLeft;
				i++;

				//add a mForward after every tLeft, tRight and t180.
				actions[i] = mForward;
				offset++;
			}
		}
		//reverse actions
		totalActions = i+1;
		for (int j=0;j<totalCellsFindCourse;j++)
		{
			invActions[j] = actions[totalActions-i-1];
		}
	}


	//apply grammar rules dependent on their priority.

	int optimizationsInLoop = 10;//make sure its not 0;
	while (optimizationsInLoop!=0)
	{
		optimizationsInLoop = 0;//reset

		//m <- RR | LL
		for(int b = 0; b<totalActions-1/*do not go over the last element since it has no follower.*/;b++)
		{
			if (invActions[b]==tRight && invActions[b+1]==tRight)
			{
				optimizationsInLoop++;
				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==tLeft && invActions[b+1]==tLeft)
			{
				optimizationsInLoop++;
				void checkProcessEndedAbruptly(void)
				{
					if(getPressedKeyNumber())
					{
						softEndFind = (mState == finding);
						softEndReturn = (mState == returning);
						end();
					}
				}

				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
		}

		//remove empty elements
		removeEmptyElements(invActions,&totalActions);

		//m <- LmR | RmL | FmF
		for(int b = 0; b<totalActions-2/*do not go over the last two elements since the 2nd last one has no followers.*/;b++)
		{
			if (invActions[b]==tLeft && invActions[b+1]==t180 && invActions[b+2]==tRight )
			{
				optimizationsInLoop++;
				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				invActions[b+2] = EMPTY;
				b+=2;//skip next two elements since they're EMPTY.
			}
			else
			if (invActions[b]==tRight && invActions[b+1]==t180 && invActions[b+2]==tLeft )
			{
				optimizationsInLoop++;
				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				invActions[b+2] = EMPTY;
				b+=2;//skip next two elements since they're EMPTY.
			}
			else
			if (invActions[b]==mForward && invActions[b+1]==t180 && invActions[b+2]==mForward )
			{
				optimizationsInLoop++;
				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				invActions[b+2] = EMPTY;
				b+=2;//skip next two elements since they're EMPTY.
			}
		}

		//remove empty elements
		removeEmptyElements(invActions,&totalActions);

		//R <- Lm || mL
		//L <- Rm || mR
		for(int b = 0; b<totalActions-1/*do not go over the last element since it has no follower.*/;b++)
		{
			if (invActions[b]==tLeft && invActions[b+1]==t180)
			{
				optimizationsInLoop++;
				invActions[b] = tRight;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==t180 && invActions[b+1]==tLeft)
			{
				optimizationsInLoop++;
				invActions[b] = tRight;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==tRight && invActions[b+1]==t180)
			{
				optimizationsInLoop++;
				invActions[b] = tLeft;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==t180 && invActions[b+1]==tRight)
			{
				optimizationsInLoop++;
				invActions[b] = tLeft;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
		}


		//remove empty elements
		removeEmptyElements(invActions,&totalActions);

		//e <- RL | LR | mm
		for(int b = 0; b<totalActions-1/*do not go over the last element since it has no follower.*/;b++)
		{
			if (invActions[b]==tRight && invActions[b+1]==tLeft)
			{
				optimizationsInLoop++;
				invActions[b] = EMPTY;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==tLeft && invActions[b+1]==tRight)
			{
				optimizationsInLoop++;
				invActions[b] = EMPTY;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
			else
			if (invActions[b]==t180 && invActions[b+1]==t180)
			{
				optimizationsInLoop++;
				invActions[b] = EMPTY;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
		}

		//remove empty elements
		removeEmptyElements(invActions,&totalActions);


		//m <- LmL | RmR
		for(int b = 0; b<totalActions-2/*do not go over the last two elements since the 2nd last one has no followers.*/;b++)
		{
			if (invActions[b]==tLeft && invActions[b+1]==t180 && invActions[b+2]==tLeft )
			{
				optimizationsInLoop++;
				invActions[b] = EMPTY;
				invActions[b+1] = EMPTY;
				invActions[b+2] = EMPTY;
				b+=2;//skip next two elements since they're EMPTY.
			}
			else
			if (invActions[b]==tRight && invActions[b+1]==t180 && invActions[b+2]==tRight )
			{
				optimizationsInLoop++;
				invActions[b] = EMPTY;
				invActions[b+1] = EMPTY;
				invActions[b+2] = EMPTY;
				b+=2;//skip next two elements since they're EMPTY.
			}
		}

		//remove empty elements
		removeEmptyElements(invActions,&totalActions);
	}

	//execute
	cellCounter = 0;
	//set every returnscells[i] while moving through the course, dependent on the actions array.
	int k = 0;
	int q = 0;
	while(k<totalActions)
	{
		checkProcessEndedAbruptly();
		action a = invActions[k];
		invCells[q].a = a;
		onLeaveCellHandler();
		switch (a)
		{
			case tLeft: show_ACTION(tLeft);turnLeft();decEnum(dir,3);k++;moveForward();break;
			case tRight: show_ACTION(tRight);turnRight();incEnum(dir,3);k++;moveForward();break;
			case mForward: show_ACTION(mForward);moveForward();break;
		}

		onEnterCellHandler();
		k++;
		q++;
	}
}



void updateCoreControl(void)
{
	switch (mState)
	{
		case calibrating: calibrate(); mState = finding;break;
		case finding: find(); mState = returning;break;
		case returning: returnToStart(); mState = undefined; end(); break;
	}
	updatePositionControl();
	updateMoveControl();
}
