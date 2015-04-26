#include "Envelope.h"
#include <iostream>
#include <cmath>

Envelope::Envelope(void)
{
	attack = 0.0;
	decay = 0.0;
	sustain = 0.0;
	release = 0.0;
	releasePoint = 0.0;
	polarity = 0;
	shape = 3.0;
}

Envelope::~Envelope(void)
{
}

double Envelope::getValueAt(double time, bool checkPolarity)
{
	double result = 0.0;

	if(time <= releasePoint)
	{
		if(time <= attack && attack > 0)
		{
			result = time/attack;
		}
		else if(time < (attack + decay) && decay > 0)
		{
			double sustainTop = 1.0 - sustain;
			double timeAfterAttack = time - attack;
			result = 1.0 - (timeAfterAttack / decay * sustainTop);
		}
		else if(time >= (attack + decay))
		{
			result = sustain;
		}
	}

	if(release > 0 && time > releasePoint)
	{
		double rpvalue = getValueAt(releasePoint, false);
		result = rpvalue * (1.0 - ((time - releasePoint) / release));
		if(result < 0) result = 0.0;
	}


	if(checkPolarity && polarity == 1)
	{
		result = 1.0 - result;
	}

	return result;
}

double Envelope::getValueAt(double time)
{
	return pow(getValueAt(time, true), shape);
}