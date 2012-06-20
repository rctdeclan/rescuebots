/*
 * main.c
 *
 *  Created on: Jun 13, 2012
 *      Author: Ron Engels & Declan Bullock
 *
 *      Description:
 *
 *      This code demonstrates the grammar rules used to optimize the route taken by the Rescuebot. Essentially, it takes any route and makes the shortest version of it.
 *      To see the code in action, please debug it. Running it won't trace anything to the console. Add a breakpoint at the end of the code (I marked it with a **).
 *      Scroll down to see a detailed description of how it works.
 */

typedef enum
{
	tLeft,
	tRight,
	mForward,
	t180,
	EMPTY
} action;



//This function shifts all EMPTY actions to the back of the array. It also modifies totalActions to the amount of non-EMPTY actions in the starting side of the array.
void removeEmptyElements(action *array, int * totalActions)
{
	int index = 0;
	for (int j = 0; j<*totalActions;j++)
	{
		if (array[j] == EMPTY) //if element j is empty
		{
			*totalActions = *totalActions-1; //decrease the amount of actions by 1
			index = j;
			for (int i = index; i < sizeof(array); i++)
			{
				array[i] = array[i+1]; //shift every j+1 element to position j.
			}
			array[sizeof(array)-1] = EMPTY; //set the last element to EMPTY.
			//The last few comments denote the process of shifting all element 1 position to the left, and placing the front element at the back.
		}
		if(array[j] == EMPTY) {j--;} //If we just shifted one out, It might be the case that at the same position, there is still an EMPTY action. In this case, decrease j so the routine will go over it again.
	}
}

int main(void)
{

	action invActions[8];
	int totalActions;

	//First set up some values.
	invActions[0] = tRight;
	invActions[1] = tRight;
	invActions[2] = mForward;
	invActions[3] = mForward;
	invActions[4] = tLeft;
	invActions[5] = mForward;
	invActions[6] = tRight;
	invActions[7] = t180;

	//Set the total amount of actions.
	totalActions = 8;


	//optimizations in loop is used to keep track of the amount of changes performed in one cycle. As soon as this gets to 0, you can be sure you've found the absolute shortest route.
	int optimizationsInLoop = 10;//make sure its not 0;
	while (optimizationsInLoop!=0)
	{
		optimizationsInLoop = 0;//reset

		//m <- RR | LL
		for(int b = 0; b<totalActions-1/*do not go over the last element since it has no follower.*/;b++)
		{
			//just check several elements. In this case, 2 elements, namely at position j and position j+1. We check if these are tRight and tRight, and if so, change the values with t180 and EMTPY, respectively.
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
				invActions[b] = t180;
				invActions[b+1] = EMPTY;
				b++;//skip next element since it's EMPTY.
			}
		}

		//remove empty elements. After this is called, the current state of the array will have only non-EMPTY actions in the beginning, all the way up to totalActions.
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
//**
//Add breakpoint here.
//At this point, the array will be fully optimized.
}
