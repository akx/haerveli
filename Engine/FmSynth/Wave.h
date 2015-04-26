#pragma once
#include <iostream>
#include <string>

using namespace std;

class Wave
{
	unsigned short channelCount;
	unsigned short bitRate;
	unsigned long sampleRate;
	unsigned long sampleCount;
	unsigned char* data;

public:
	Wave();
	~Wave();

	unsigned short getChannelCount() const { return this->channelCount; }
	void setChannelCount(unsigned short channelCount) { this->channelCount = channelCount; }
	unsigned short getBitRate() const { return this->bitRate; }
	void setBitRate(unsigned short bitRate) { this->bitRate = bitRate; }
	unsigned long getSampleRate() const { return this->sampleRate; }
	void setSampleRate(unsigned long sampleRate) { this->sampleRate = sampleRate; }

	unsigned long getSampleCount() const { return this->sampleCount; }
	void setSampleCount(unsigned long sampleCount);
	
	unsigned char* getData() { return this->data; }
	bool setData(unsigned char* data);
	unsigned char* getSample(unsigned long samplePos);
	bool setSample(unsigned char* data, unsigned long samplePos);
	bool setSample(double left, double right, unsigned long samplePos);

	void load(string filePath);
	bool save(string filePath);
};