#define _USE_MATH_DEFINES
#include <cmath>

#include "Operator.h"

using namespace std;

typedef map<string, double>::iterator ParameterIterator;

Operator::Operator()
{
	oscillatorFrequency = 0.0;
	oscillatorLevel = 0.0;
	sharpness = 0.0;
	noiseLevel = 0.0;
	distortion = 1.0;
	resolution = 0;
	panningFrequency = 0.0;
	panningLevel = 0.0;
	panningOffset = 0.0;
	envelope = new Envelope();
	envelopeAmount = 1.0;
	pitchEnvelope = new Envelope();
	pitchEnvelopeAmount = 0.0;
	modulatorType = 0;
	keyFollow = 0;
	keyOffset = 0;
	octaveOffset = 0;
	baseKey = 0;
	baseOctave = 0;
	oscillatorPhase = 0.0;
	frequencyModulation[0] = 0.0;
	frequencyModulation[1] = 0.0;
}

Operator::~Operator()
{
	envelope = NULL;
	pitchEnvelope = NULL;
	modulators.clear();
}


// private methods

// Left [-1...1] Right
double Operator::getPanning(double time)
{
	double result;
	result = sin(2 * M_PI * time * panningFrequency) * panningLevel + panningOffset;
	if(result < -1.0) result = -1.0;
	else if(result > 1.0) result = 1.0;
	return result;
}

double Operator::getEnvelopeValueAt(double time)
{
	return envelope->getValueAt(time);
}

double Operator::getValueAt(double time, int channel)
{
	int modulatorsCount = modulators.size();
	double level = oscillatorLevel;
	double phaseModulation = 0.0;
	double additive = 0.0;
	double ringModulation = 1.0;
	double amplitudeModulation;
	double value;
	double modLevel;
	int modType;
	double initialPhase;
	double osc;
	double squareWave;
	double envelopeValue;
	double panning;

	if(modulatorsCount>0 && !hasFeedBack(this))
	{
		for(int i=0; i<modulatorsCount; ++i)
		{
			value = modulators[i]->getValueAt(time, channel);
			modLevel = modulators[i]->getOscillatorLevel();
			modType = modulators[i]->getModulatorType();
			
			switch(modType)
			{
			case 0:
				frequencyModulation[channel] += value;
				break;
			case 1:
				phaseModulation += value;
				break;
			case 2:
				amplitudeModulation = (value + modLevel) / 2.0;
				amplitudeModulation += (1.0 - modLevel);
				level *= amplitudeModulation;
				break;
			case 3:
				additive += value;
				break;
			case 4:
				ringModulation *= value;
				break;
			}
		}
	}

	// add pitch envelope
	frequencyModulation[channel] += pitchEnvelope->getValueAt(time) * pitchEnvelopeAmount;
	
	// convert phase to radians
	initialPhase = oscillatorPhase / 360.0 * 2 * M_PI;

	// calculate FM + PM
	osc = sin(2 * M_PI * time * oscillatorFrequency + initialPhase + phaseModulation + frequencyModulation[channel]);

	// the extreme square wave value
	squareWave = 0.0;
	if(osc < 0.0) squareWave = -1.0;
	else if(osc > 0.0) squareWave = 1.0;

	// random number for noise
	int rn = rand() % RAND_MAX;
	double noise = (double)rn / RAND_MAX * 2.0 - 1.0;

	// fade between sine and square wave
	osc = (squareWave * sharpness) + (osc * (1.0 - sharpness));

	// mix noise
	osc = (noise * noiseLevel) + (osc * (1.0 - noiseLevel));

	// add volume envelope
	envelopeValue = getEnvelopeValueAt(time);
	envelopeValue = 1.0 - envelopeAmount + (envelopeValue * envelopeAmount);
	osc *= envelopeValue;

	// add panning
	panning = getPanning(time);
	switch(channel)
	{
	case 0:
		if(panning > 0.0) osc *= (1.0 - panning); // left channel, panning to the right
		break;
	case 1:
		if(panning < 0.0) osc *= (1.0 + panning); // right channel, panning to the left
		break;
	}

	// add distortion
	if(osc < 0.0)
		osc = -pow(-osc, distortion);
	else
		osc = pow(osc, distortion);

	// quantize to resolution
	if(resolution > 0)
	{
		double depth = pow(2.0, resolution);
		double percentage = (osc + 1.0) / 2;
		double quantized = floor(percentage * depth + 0.5);
		osc = quantized / depth * 2.0 - 1.0;
	}

	// set level
	osc *= level;

	// additive synthesis
	osc += additive;

	// ring modulation
	osc *= ringModulation;

	return osc;
}


bool Operator::hasFeedBack(Operator* modulationRoot)
{
	int modulatorsCount = modulators.size();
	if(modulatorsCount == 0) return false;

	for(int i=0; i<modulatorsCount; ++i)
	{
		if(modulators[i] == modulationRoot) return true;
		if(modulators[i]->hasFeedBack(modulationRoot)) return true;
	}

	return false;
}


// public methods

void Operator::addModulator(Operator* op)
{
	this->modulators.push_back(op);
}

void Operator::clearModulators()
{
	this->modulators.clear();
}

double Operator::getValueLeft(double time)
{
	return getValueAt(time, 0);
}

double Operator::getValueRight(double time)
{
	return getValueAt(time, 1);
}


void Operator::reset()
{
	unsigned int modulatorsCount = modulators.size();
	if(modulatorsCount>0 && !hasFeedBack(this))
	{
		for(unsigned int i=0; i<modulatorsCount; i++)
		{
			modulators[i]->reset();
		}
	}
	frequencyModulation[0] = 0.0;
	frequencyModulation[1] = 0.0;

	restoreParameters();
}


void Operator::updateFrequency()
{
	int noteIndex = (baseOctave + octaveOffset) * 12 + (baseKey + keyOffset);
	int indexOfA4 = 4 * 12 + 9;
	int distanceFromA4 = noteIndex - indexOfA4;
	
	// always update modulators

	if(modulators.size()>0 && !hasFeedBack(this))
	{
		for(unsigned int i=0; i<modulators.size(); i++)
		{
			modulators[i]->setBaseKey(baseKey);
			modulators[i]->setBaseOctave(baseOctave);
			modulators[i]->updateFrequency();
		}
	}
	
	// update only if keyFollow is set
	if(keyFollow)
		oscillatorFrequency = pow(2.0, ((double)distanceFromA4 / 12.0)) * 440.0;
	else
		oscillatorFrequency = originalOscillatorFrequency;
}


void Operator::setParameter(string parameterId, double value, bool storeOriginalValue)
{

	if(parameterId.compare("oscillator_frequency") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = oscillatorFrequency;
		originalOscillatorFrequency = value;
	}

	else if(parameterId.compare("oscillator_level") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = oscillatorLevel;
		oscillatorLevel = value;
	}

	else if(parameterId.compare("oscillator_offset_k") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = keyOffset;
		keyOffset = (int)value;
	}

	else if(parameterId.compare("oscillator_offset_o") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = octaveOffset;
		octaveOffset = (int)value;
	}

	else if(parameterId.compare("oscillator_follow") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = keyFollow;
		keyFollow = (int)value;
	}

	else if(parameterId.compare("oscillator_phase") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = oscillatorPhase;
		oscillatorPhase = value;
	}

	else if(parameterId.compare("oscillator_mod_type") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = modulatorType;
		modulatorType = (int)value;
	}

	else if(parameterId.compare("amp_attack") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getAttack();
		getEnvelope()->setAttack(value);
	}

	else if(parameterId.compare("amp_decay") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getDecay();
		getEnvelope()->setDecay(value);
	}

	else if(parameterId.compare("amp_sustain") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getSustain();
		getEnvelope()->setSustain(value);
	}

	else if(parameterId.compare("amp_release") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getRelease();
		getEnvelope()->setRelease(value);
	}

	else if(parameterId.compare("amp_envelope_amount") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = envelopeAmount;
		envelopeAmount = value;
	}

	else if(parameterId.compare("amp_envelope_polarity") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getPolarity();
		getEnvelope()->setPolarity((int)value);
	}

	else if(parameterId.compare("amp_envelope_shape") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getEnvelope()->getShape();
		getEnvelope()->setShape(value);
	}

	else if(parameterId.compare("pitch_attack") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getAttack();
		getPitchEnvelope()->setAttack(value);
	}

	else if(parameterId.compare("pitch_decay") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getDecay();
		getPitchEnvelope()->setDecay(value);
	}

	else if(parameterId.compare("pitch_sustain") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getSustain();
		getPitchEnvelope()->setSustain(value);
	}

	else if(parameterId.compare("pitch_release") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getRelease();
		getPitchEnvelope()->setRelease(value);
	}

	else if(parameterId.compare("pitch_envelope_amount") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = pitchEnvelopeAmount;
		pitchEnvelopeAmount = value;
	}

	else if(parameterId.compare("pitch_envelope_polarity") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getPolarity();
		getPitchEnvelope()->setPolarity((int)value);
	}

	else if(parameterId.compare("pitch_envelope_shape") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = getPitchEnvelope()->getShape();
		getPitchEnvelope()->setShape(value);
	}

	else if(parameterId.compare("sharpness") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = sharpness;
		sharpness = value;
	}

	else if(parameterId.compare("noise_level") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = noiseLevel;
		noiseLevel = value;
	}

	else if(parameterId.compare("distortion") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = distortion;
		distortion = value;
	}

	else if(parameterId.compare("resolution") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = resolution;
		resolution = (int)value;
	}

	else if(parameterId.compare("panning_frequency") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = panningFrequency;
		panningFrequency = value;
	}

	else if(parameterId.compare("panning_level") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = panningLevel;
		panningLevel = value;
	}

	else if(parameterId.compare("panning_offset") == 0)
	{
		if(storeOriginalValue) originalParameterValues[parameterId] = panningOffset;
		panningOffset = value;
	}
}

void Operator::restoreParameters()
{
	for(ParameterIterator iter = originalParameterValues.begin(); iter != originalParameterValues.end(); ++iter)
	{
		setParameter(iter->first, iter->second, false);
	}
	originalParameterValues.clear();
}