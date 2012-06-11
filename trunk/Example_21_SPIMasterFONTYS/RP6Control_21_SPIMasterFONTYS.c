/* 
 * ****************************************************************************
 * RP6 ROBOT SYSTEM - RP6 CONTROL M32 Examples
 * ****************************************************************************
 * Example: SPI Master
 * Author(s): Tommy Sools & Corné Govers
 * ****************************************************************************
 * Description:
 * Tests SPI Gyroscope Module. Max bus speed: 4 MHz.
 * CS = PB1/MEM_CS2
 * ############################################################################
 * The Robot does NOT move in this example! You can simply put it on a table
 * next to your PC and you should connect it to the PC via the USB Interface!
 * ############################################################################
 * ****************************************************************************
 */

/*****************************************************************************/
// Includes:

#include "RP6ControlLib.h" 		// The RP6 Control Library. 
								// Always needs to be included!

void RP6_SPI_Select(void)
{
	PORTB &= ~MEM_CS2;
}

void RP6_SPI_Deselect(void)
{
	PORTB |= MEM_CS2;
}

/*****************************************************************************/

unsigned short RP6_SPI_read(void)
{
	unsigned short result;
	RP6_SPI_Select();
	//sleep(10);
	result = readWordSPI();
	//sleep(10);
	RP6_SPI_Deselect();
	return result;
}


/*****************************************************************************/

void RP6_SPI_init(void)
{
/*
	void writeSPI(uint8_t data);
	uint8_t readSPI(void);
	uint16_t readWordSPI(void);
	void writeWordSPI(uint16_t data);
	void writeBufferSPI(uint8_t *buffer, uint8_t length);
	void readBufferSPI(uint8_t *buffer, uint8_t length);
*/
/*
	// SPI Master (SPI Mode 0, SCK Frequency is F_CPU/2, which means it is 8MHz 
	// on the RP6 CONTROL M32...):
	SPCR =    (0<<SPIE) 
			| (1<<SPE) 
			| (1<<MSTR) 
			| (0<<SPR0) 
			| (0<<SPR1) 
			| (0<<CPOL) 
			| (0<<CPHA);  
	SPSR = (1<<SPI2X);
*/
	unsigned short dummy;
	
	// default SPI mode: 0.
	// default SPI clock rate: 8 MHz.
	// default CPOL: 0 => leading edge = rising; trailing edge = falling
	// default CPHA: 0 => leading edge = sample; trailing edge = setup

	// lower clock rate to 4 MHz.
	//SPSR &= ~(1<<SPI2X);

	// lower clock rate to 1 MHz.
	// SPCR |= (1<<SPR0);
	// SPSR &= ~(1<<SPI2X);

	// lower clock rate to 250 kHz.
	SPCR |= (1<<SPR1);
	SPSR &= ~(1<<SPI2X);

	// lower clock rate to 125 kHz.
	// SPCR |= (1<<SPR1) | (1<<SPR0);
	// SPSR &= ~(1<<SPI2X);

	dummy = RP6_SPI_read();
}


/*****************************************************************************/

// Main function - The program starts here:

int16_t main(void)
{
	initRP6Control(); // Always call this first! The Processor will not work
					  // correctly otherwise. 

	initLCD(); // Initialize the LC-Display (LCD)
			   // Always call this before using the LCD!
			   
	writeString_P("\n\nRP6 CONTROL M32 SPI Gyroscope Module Test Program!\n"); 

	RP6_SPI_init();

	// Play two sounds:
	sound(180,80,25);
	sound(220,80,25);

	setLEDs(0b1111); // Turn all LEDs on!

	showScreenLCD("################", "################");
	mSleep(500);
	showScreenLCD("SPI-Master", "Gyroscope");
	mSleep(1000);
	// ---------------------------------------
	setLEDs(0b0000); // All LEDs off!
	
	while(true) 
	{
		sleep(100);
		// setLEDs(0b0001);
		// writeString_P("Gyroscope data: "); 
		writeInteger((RP6_SPI_read() >> 2), DEC);
		writeChar('\n');
	}
	return 0;
}
