#pragma comment(linker, "/SUBSYSTEM:windows /ENTRY:mainCRTStartup")

#include <iostream>
#include <fstream>
#include <cmath>
#include <string>
#include <map>

#include "Wave.h"
#include "Operator.h"
#include "Envelope.h"
#include "Note.h"
#include "Patch.h"

using namespace std;


enum ItemType
{
	Settings,
	PatchDefinition,
	OperatorDefinition,
	NoteDefinition,
	Other
};

// settings
double bpm = 100;
unsigned short channelCount = 2;
unsigned short bitRate = 16;
unsigned long sampleRate = 44100;
int fadeOutSamples = 44;

// patches by id
std::map<std::string, Patch*> patches;
typedef std::map<std::string, Patch*>::iterator PatchIterator;
typedef std::map<std::string, Operator*>::iterator OperatorIterator;

// notes
std::vector<Note*> sequence;
typedef std::vector<Note*>::iterator SequenceIterator;

int getItemType(string line)
{
	if(line.compare("settings") == 0) return ::Settings;
	if(line.compare("patch") == 0) return ::PatchDefinition;
	if(line.compare("operator") == 0) return ::OperatorDefinition;
	if(line.compare("note") == 0) return ::NoteDefinition;
	return ::Other;
}

void writeSetting(string data)
{
	unsigned int pos = string::npos;

	if((pos = data.find("bpm")) != string::npos)
	{
		sscanf(data.c_str(), "%*s %*c %lf", &bpm);
	}
}

void writePatchData(Patch* target, string data)
{
	unsigned int pos = string::npos;

	if((pos = data.find("id")) != string::npos)
	{
		char value[128];
 		sscanf(data.c_str(), "%*s %*c %s", value);
		patches[value] = target;
	}
	
	else if((pos = data.find("root")) != string::npos)
	{
		char value[128];
		sscanf(data.c_str(), "%*s %*c %s", value);
		target->setRootOperatorId(value);
	}
	
	else if((pos = data.find("modulation")) != string::npos)
	{
		char value1[128];
		char value2[128];
		sscanf(data.c_str(), "%*s %*c %s %*s %s", value1, value2);
		target->addModulation(value1, value2);
	}
}

void writeOperatorData(Operator* target, string data)
{
	unsigned int pos = string::npos;

	if((pos = data.find("id")) != string::npos)
	{
		char value[128];
		sscanf(data.c_str(), "%*s %*c %s", value);
		for(PatchIterator iter = patches.begin(); iter != patches.end(); ++iter)
		{
			Patch* patch = iter->second;
			patch->addOperator(value, target);
		}
	}

	else if((pos = data.find("key_follow")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setKeyFollow(value);
	}

	else if((pos = data.find("key_offset")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setKeyOffset(value);
	}

	else if((pos = data.find("octave_offset")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setOctaveOffset(value);
	}

	else if((pos = data.find("modulator_type")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setModulatorType(value);
	}

	else if((pos = data.find("oscillator_level")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setOscillatorLevel(value);
	}

	else if((pos = data.find("oscillator_frequency")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setOscillatorFrequency(value);
	}

	else if((pos = data.find("oscillator_phase")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setOscillatorPhase(value);
	}

	else if((pos = data.find("amp_attack")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setAttack(value);
	}

	else if((pos = data.find("amp_decay")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setDecay(value);
	}

	else if((pos = data.find("amp_sustain")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setSustain(value);
	}

	else if((pos = data.find("amp_release ")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setRelease(value);
	}

	else if((pos = data.find("amp_release_point")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setReleasePoint(value);
	}

	else if((pos = data.find("amp_envelope_amount")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setEnvelopeAmount(value);
	}

	else if((pos = data.find("amp_envelope_polarity")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->getEnvelope()->setPolarity(value);
	}

	else if((pos = data.find("amp_envelope_shape")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getEnvelope()->setShape(value);
	}

	else if((pos = data.find("pitch_attack")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setAttack(value);
	}

	else if((pos = data.find("pitch_decay")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setDecay(value);
	}

	else if((pos = data.find("pitch_sustain")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setSustain(value);
	}

	else if((pos = data.find("pitch_release ")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setRelease(value);
	}

	else if((pos = data.find("pitch_release_point")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setReleasePoint(value);
	}

	else if((pos = data.find("pitch_envelope_amount")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setPitchEnvelopeAmount(value);
	}

	else if((pos = data.find("pitch_envelope_polarity")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->getPitchEnvelope()->setPolarity(value);
	}

	else if((pos = data.find("pitch_envelope_shape")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->getPitchEnvelope()->setShape(value);
	}

	else if((pos = data.find("sharpness")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setSharpness(value);
	}

	else if((pos = data.find("noise_level")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setNoiseLevel(value);
	}

	else if((pos = data.find("distortion")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setDistortion(value);
	}

	else if((pos = data.find("resolution")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setResolution(value);
	}

	else if((pos = data.find("panning_frequency")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setPanningFrequency(value);
	}

	else if((pos = data.find("panning_level")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setPanningLevel(value);
	}

	else if((pos = data.find("panning_offset")) != string::npos)
	{
		double value;
		sscanf(data.c_str(), "%*s %*c %lf", &value);
		target->setPanningOffset(value);
	}

}

void writeNoteData(Note* target, string data)
{
	unsigned int pos = string::npos;

	if((pos = data.find("position_ppqn")) != string::npos)
	{
		unsigned long value;
		sscanf(data.c_str(), "%*s %*c %dlu", &value);
		target->setPositionPpqn(value);
	}

	else if((pos = data.find("duration_ppqn")) != string::npos)
	{
		unsigned long value;
		sscanf(data.c_str(), "%*s %*c %dlu", &value);
		target->setDurationPpqn(value);
	}

	else if((pos = data.find("release_point_ppqn")) != string::npos)
	{
		unsigned long value;
		sscanf(data.c_str(), "%*s %*c %dlu", &value);
		target->setReleasePointPpqn(value);
	}

	else if((pos = data.find("key")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setKey(value);
	}

	else if((pos = data.find("octave")) != string::npos)
	{
		int value;
		sscanf(data.c_str(), "%*s %*c %d", &value);
		target->setOctave(value);
	}

	else if((pos = data.find("patch_id")) != string::npos)
	{
		char value[128];
		sscanf(data.c_str(), "%*s %*c %s", value);
		target->setPatchId(value);
	}

	else if((pos = data.find("parameter")) != string::npos)
	{

		Parameter param;
		char operatorId[128];
		char parameterId[128];
		double parameterValue;
		sscanf(data.c_str(), "%*s %*c %s %*s %s %*c %lf", operatorId, parameterId, &parameterValue);
		
		param.parameterId = parameterId;
		param.value = parameterValue;

		target->addParameter(operatorId, param);
	}

}


unsigned long writeNote(Note* note, Wave* wave, unsigned long position, double releasePoint)
{
	unsigned long sampleCount = note->getSampleCountFor(sampleRate);

	// if key is negative, the note is considered a rest, so only silence is written
	if(note->getKey() < 0)
	{
		for(unsigned long i=position; i<(position+sampleCount); i++)
		{
			wave->setSample(0.0, 0.0, i);
		}
		return position+sampleCount;
	}

	Patch* patch = patches[note->getPatchId()];
	Operator* op = patch->getRootOperator();

	patch->connectModulators();

	op->setBaseKey(note->getKey());
	op->setBaseOctave(note->getOctave());
	op->reset();

	map<string, vector<Parameter>> allParams = note->getParameters();
	for(map<string, vector<Parameter>>::iterator iter = allParams.begin(); iter != allParams.end(); ++iter)
	{
		vector<Parameter> operatorParams = iter->second;
		for(vector<Parameter>::iterator iter2 = operatorParams.begin(); iter2 != operatorParams.end(); ++ iter2)
		{
			patch->setOperatorParameter(iter->first, (*iter2).parameterId, (*iter2).value);
		}
	}

	op->updateFrequency();
	
	map<string, Operator*> ops = patch->getOperators();

	for(OperatorIterator iter = ops.begin(); iter!= ops.end(); ++iter)
	{
		Operator* o = iter->second;
		o->getEnvelope()->setReleasePoint(releasePoint);
		o->getPitchEnvelope()->setReleasePoint(releasePoint);
	}

	for(unsigned long i=position; i<(position+sampleCount); i++)
	{
		double left = op->getValueLeft((double)(i-position) / wave->getSampleRate());
		double right = op->getValueRight((double)(i-position) / wave->getSampleRate());
		
		if(left>1.0) left = 1.0;
		if(left<-1.0) left = -1.0;
		if(right>1.0) right = 1.0;
		if(right<-1.0) right = -1.0;

		int fadeOutPoint = position + sampleCount - fadeOutSamples;
		if(i >= (unsigned int)fadeOutPoint)
		{
			double fadeOut = 1.0 - (double)(i - fadeOutPoint) / fadeOutSamples;
			left *= fadeOut;
			right *= fadeOut;
		}

		wave->setSample(left, right, i);
	}

	return position+sampleCount;
}

int main(int argc, char **argv)
{
	if(argc<2) return -1;

	std::ifstream reader;
	reader.open(argv[1]);

	if(reader==NULL) return -1;

	char buffer[128];

	int currentItemType = ::Other;

	Patch* currentPatch = NULL;
	Operator* currentOperator = NULL;
	Note* currentNote = NULL;

	reader.getline(buffer, 127, '\n');

	while(!reader.eof())
	{
		std::string line = buffer;

		if(getItemType(line) != ::Other)
		{
			currentItemType = getItemType(line);
			switch(currentItemType)
			{
			case ::Settings:
				// nada
				break;

			case ::PatchDefinition:
				currentPatch = new Patch();
				break;

			case ::OperatorDefinition:
				currentOperator = new Operator();
				break;

			case ::NoteDefinition:
				currentNote = new Note();
				sequence.push_back(currentNote);
				break;
			}
		}
		else
		{
			switch(currentItemType)
			{
			case ::Settings:
				writeSetting(line);
				break;

			case ::PatchDefinition:
				writePatchData(currentPatch, line);
				break;

			case ::OperatorDefinition:
				writeOperatorData(currentOperator, line);
				break;

			case ::NoteDefinition:
				writeNoteData(currentNote, line);
				break;
			}
		}

		reader.getline(buffer, 127, '\n');
	}

	reader.close();

	srand(303);

	double ppqnLength = 60.0 / bpm / 24;
	
	double totalDuration = 0.0;
	unsigned long totalSampleCount = 0;

	for(SequenceIterator iter = sequence.begin(); iter != sequence.end(); ++iter)
	{
		Note* note = *iter;
		if(note->getDurationPpqn() > 0)
		{
			double noteDuration = note->getDurationPpqn() * ppqnLength;
			note->setDuration(noteDuration);
			totalDuration += noteDuration;
			totalSampleCount += note->getSampleCountFor(sampleRate);
		}
	}

	Wave soundOutput;
	soundOutput.setBitRate(bitRate);
	soundOutput.setChannelCount(channelCount);
	soundOutput.setSampleRate(sampleRate);
	soundOutput.setSampleCount(totalSampleCount);

	unsigned long position = 0;

	for(SequenceIterator iter = sequence.begin(); iter != sequence.end(); ++iter)
	{
		Note* note = *iter;
		double releasePoint = note->getReleasePointPpqn() * ppqnLength;
		position = writeNote(note, &soundOutput, position, releasePoint);
	}

	if(argc>2)
		soundOutput.save(argv[2]);

	//char fileName[] = "C:\\Temp\\fm_out.wav";
	//soundOutput.save(fileName);
	//system(fileName);

	return 0;
}
