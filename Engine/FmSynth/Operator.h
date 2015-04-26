#pragma once
#include <iostream>
#include <string>
#include <map>
#include <vector>

#include "Envelope.h"

using namespace std;

class Operator
{
	vector<Operator*> modulators;

	map<string, double> originalParameterValues;

	double originalOscillatorFrequency;
	double oscillatorFrequency;
	double oscillatorLevel;
	double oscillatorPhase; // degrees

	double sharpness;
	double noiseLevel;
	double distortion;
	int resolution; // resolution in bits, 0 = float

	double panningFrequency;
	double panningLevel;
	double panningOffset;

	Envelope* envelope;
	Envelope* pitchEnvelope;

	double envelopeAmount;
	double pitchEnvelopeAmount;

	int modulatorType; // 0 = FM, 1 = PM, 2 = AM, 3 = additive, 4 = RM
	
	int keyFollow;
	int keyOffset;
	int octaveOffset;
	int baseKey;
	int baseOctave;

	double frequencyModulation[2]; // index: 0 = left, 1 = right

	double getPanning(double time);
	double getEnvelopeValueAt(double time);

	double getValueAt(double time, int channel); // channel: 0 = left, 1 = right

	bool hasFeedBack(Operator* modulationRoot);

public:
	Operator();
	~Operator();

	// modulators
	vector<Operator*> getModulators() const { return modulators; }
	void addModulator(Operator* op);
	void clearModulators();

	// oscillator
	double getOscillatorFrequency() const { return oscillatorFrequency; }
	void setOscillatorFrequency(double freq) { oscillatorFrequency = freq; originalOscillatorFrequency = freq; }
	double getOscillatorLevel() const { return oscillatorLevel; }
	void setOscillatorLevel(double level) { oscillatorLevel = level; }
	double getOscillatorPhase() const { return oscillatorPhase; }
	void setOscillatorPhase(double phase) { oscillatorPhase = phase; }

	// noise
	double getNoiseLevel() const { return noiseLevel; }
	void setNoiseLevel(double noise) { noiseLevel = noise; }

	// sharpness
	double getSharpness() const { return sharpness; }
	void setSharpness(double sharpness) { this->sharpness = sharpness; }

	// distortion
	double getDistortion() const { return distortion; }
	void setDistortion(double distortion) { this->distortion = distortion; }

	// resolution
	int getResolution() const { return resolution; }
	void setResolution(int resolution) { this->resolution = resolution; }

	// panning
	double getPanningFrequency() const { return panningFrequency; }
	void setPanningFrequency(double freq) { panningFrequency = freq; }
	double getPanningLevel() const { return panningLevel; }
	void setPanningLevel(double level) { panningLevel = level; }
	double getPanningOffset() const { return panningOffset; }
	void setPanningOffset(double offset) { panningOffset = offset; }

	// envelope
	Envelope* getEnvelope() const { return envelope; }
	void setEnvelope(Envelope* e) { envelope = e; }
	double getEnvelopeAmount() const { return envelopeAmount; }
	void setEnvelopeAmount(double amount) { envelopeAmount = amount; }

	// pitch envelope
	Envelope* getPitchEnvelope() const { return pitchEnvelope; }
	void setPitchEnvelope(Envelope* e) { pitchEnvelope = e; }
	double getPitchEnvelopeAmount() const { return pitchEnvelopeAmount; }
	void setPitchEnvelopeAmount(double amount) { pitchEnvelopeAmount = amount; }

	// get value
	double getValueLeft(double time);
	double getValueRight(double time);

	// modulator type
	int getModulatorType() const { return modulatorType; }
	void setModulatorType(int type) { modulatorType = type; }

	// key follow properties
	int getKeyFollow() const { return this->keyFollow; }
	void setKeyFollow(int keyFollow) { this->keyFollow = keyFollow; }
	int getKeyOffset() const { return this->keyOffset; }
	void setKeyOffset(int keyOffset) { this->keyOffset = keyOffset; }
	int getOctaveOffset() const { return this->octaveOffset; }
	void setOctaveOffset(int octaveOffset) { this->octaveOffset = octaveOffset; }
	int getBaseKey() const { return this->baseKey; }
	void setBaseKey(int baseKey) { this->baseKey = baseKey; }
	int getBaseOctave() const { return this->baseOctave; }
	void setBaseOctave(int baseOctave) { this->baseOctave = baseOctave; }

	// reset
	void reset();

	// calculates frequency according to key if key follow is set
	// othewise leaves frequency unchanged
	void updateFrequency();

	// set and restore parameters
	void setParameter(string parameterId, double value, bool storeOriginalValue);
	void restoreParameters();
};