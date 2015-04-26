#pragma once

#include <iostream>
#include <vector>
#include <string>
#include <map>

#include "Operator.h"
#include "Envelope.h"

typedef struct param_struct
{
	std::string parameterId;
	double value;
} Parameter;

class Note
{
	int _key; // key from 0 (C) to 11 (B), negative if unassigned (rest)
	int _octave; // octave, no boundaries
	double _duration; // duration in seconds

	unsigned long _positionPpqn; // position in ppqn
	unsigned long _durationPpqn; // duration in ppqn
	unsigned long _releasePointPpqn; // release point in ppqn

	std::string _patchId; // patch id
	std::map<std::string, std::vector<Parameter>> _parameters; // parameters

public:
	Note(void);
	~Note(void);

	int getKey() const;
	void setKey(int key);

	int getOctave() const;
	void setOctave(int octave);
	
	double getDuration() const;
	void setDuration(double duration);

	unsigned long getPositionPpqn() const;
	void setPositionPpqn(unsigned long position);

	unsigned long getDurationPpqn() const;
	void setDurationPpqn(unsigned long duration);

	unsigned long getReleasePointPpqn() const;
	void setReleasePointPpqn(unsigned long releasePoint);

	std::string getPatchId() const;
	void setPatchId(std::string patchId);

	std::map<std::string, std::vector<Parameter>> getParameters() const;
	void addParameter(std::string path, Parameter param);

	unsigned int getSampleCountFor(unsigned long sampleRate) const;

};
