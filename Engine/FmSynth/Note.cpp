#include "Note.h"
#include <cmath>

Note::Note(void)
{
	_key = 0;
	_octave = 0;
	_duration = 0.0;
	_positionPpqn = 0;
	_durationPpqn = 0;
	_releasePointPpqn = 0;
}

Note::~Note(void)
{
	
}

// key

int Note::getKey() const
{
	return _key;
}
void Note::setKey(int key)
{
	_key = key;
}

// octave

int Note::getOctave() const
{
	return _octave;
}
void Note::setOctave(int octave)
{
	_octave = octave;
}

// duration in seconds

double Note::getDuration() const
{
	return _duration;
}
void Note::setDuration(double duration)
{
	_duration = duration;
}

// position in ppqn

unsigned long Note::getPositionPpqn() const
{
	return _positionPpqn;
}
void Note::setPositionPpqn(unsigned long position)
{
	_positionPpqn = position;
}

// duration in ppqn

unsigned long Note::getDurationPpqn() const
{
	return _durationPpqn;
}
void Note::setDurationPpqn(unsigned long duration)
{
	_durationPpqn = duration;
}

// release point in ppqn

unsigned long Note::getReleasePointPpqn() const
{
	return _releasePointPpqn;
}
void Note::setReleasePointPpqn(unsigned long releasePoint)
{
	_releasePointPpqn = releasePoint;
}

// patch id

std::string Note::getPatchId() const
{
	return _patchId;
}
void Note::setPatchId(std::string patchId)
{
	_patchId = patchId;
}

// parameters

std::map<std::string, std::vector<Parameter>> Note::getParameters() const
{
	return _parameters;
}
void Note::addParameter(std::string path, Parameter param)
{
	_parameters[path].push_back(param);
}

// get sample count

unsigned int Note::getSampleCountFor(unsigned long sampleRate) const
{
	return (unsigned long)(_duration * sampleRate);
}
