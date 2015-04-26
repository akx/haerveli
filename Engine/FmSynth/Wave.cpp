#include "Wave.h"
#include <cstdio>


Wave::Wave()
{
	channelCount = 0;
	bitRate = 0;
	sampleRate = 0;
	sampleCount = 0;
	data = NULL;
}

Wave::~Wave()
{
	if(data!=NULL) free(data);
}

void Wave::setSampleCount(unsigned long sampleCount)
{
	if(channelCount==0 || bitRate==0) return;

	this->sampleCount = sampleCount;
	if(data!=NULL) free(data);

	int dataSize = sampleCount * channelCount * (bitRate / 8);
	data = (unsigned char*)malloc(dataSize);
}

bool Wave::setData(unsigned char* data)
{
	if(channelCount==0 || bitRate==0 || data==NULL || sampleCount==0) return false;

	int dataSize = sampleCount * channelCount * (bitRate / 8);

	if(this->data != NULL) free(data);
	this->data = (unsigned char*)malloc(dataSize);

	memcpy(this->data, data, dataSize);

	return true;
}

unsigned char* Wave::getSample(unsigned long samplePos)
{
	unsigned long dataPos = samplePos * this->channelCount * (this->bitRate / 8);
	return data + dataPos;
}

bool Wave::setSample(unsigned char* data, unsigned long samplePos)
{
	if(samplePos >= sampleCount) return false;

	unsigned long dataPos = samplePos * channelCount * (bitRate / 8);

	switch(channelCount)
	{
		case 1:
			if(bitRate==8)
			{
				this->data[dataPos] = data[0];
				return true;
			}
			else if(bitRate==16)
			{
				this->data[dataPos] = data[0];
				this->data[dataPos+1] = data[1];
				return true;
			}
			break;

		case 2:
			if(bitRate==8)
			{
				this->data[dataPos] = data[0];
				this->data[dataPos+1] = data[1];
				return true;
			}
			else if(bitRate==16)
			{
				this->data[dataPos] = data[0];
				this->data[dataPos+1] = data[1];
				this->data[dataPos+2] = data[2];
				this->data[dataPos+3] = data[3];
				return true;
			}
			break;
	}

	return false;
}

bool Wave::setSample(double left, double right, unsigned long samplePos)
{
	unsigned char sample[4];

	switch(bitRate)
	{

		case 8:		
			sample[0] = (unsigned char)(floor((left + 1.0) / 2.0 * 255) + 0.5);
			sample[1] = (unsigned char)(floor((right + 1.0) / 2.0 * 255) + 0.5);
			sample[2] = 0;
			sample[3] = 0;
			setSample(sample, samplePos);
			return true;
			break;

		case 16:
			long leftSample = (long)floor(left * 32767 + 0.5);
			long rightSample = (long)floor(right * 32767 + 0.5);

			sample[0] = (unsigned char)(leftSample & 0x00FF);
			sample[1] = (unsigned char)((leftSample & 0xFF00) >> 8);
			sample[2] = (unsigned char)(rightSample & 0x00FF);
			sample[3] = (unsigned char)((rightSample & 0xFF00) >> 8);
			setSample(sample, samplePos);

			return true;
			break;

	}

	return false;
}


void Wave::load(string fileName){
	// todo: implement
}


bool Wave::save(string fileName)
{
	if(data==NULL) return false;

	FILE* out;
	out = fopen(fileName.c_str(), "wb");
	if(out==NULL) return false;

	unsigned long dataSize = sampleCount * channelCount * (bitRate / 8);
	unsigned long chunkSize = 36 + dataSize;
	unsigned long headerSize = 16;
	unsigned short audioFormat = 1;
	unsigned short blockAlign = channelCount * (bitRate / 8);
	unsigned long byteRate = sampleRate * blockAlign;

	// Id, size and format for a Wave file
	fwrite("RIFF", 1, 4, out);
	fwrite(&chunkSize, sizeof(chunkSize), 1, out);
	fwrite("WAVE", 1, 4, out);

	// Id, size and format for PCM header chunk
	fwrite("fmt ", 1, 4, out);
	fwrite(&headerSize, 4, 1, out);
	fwrite(&audioFormat, 2, 1, out);

	// Bitrate, samplerate etc.
	fwrite(&channelCount, 2, 1, out);
	fwrite(&sampleRate, 4, 1, out);
	fwrite(&byteRate, 4, 1, out);
	fwrite(&blockAlign, 2, 1, out);
	fwrite(&bitRate, 2, 1, out);

	// Data chunk
	fwrite("data", 1, 4, out);
	fwrite(&dataSize, 4, 1, out);
	fwrite(data, 1, dataSize, out);

	fclose(out);

	return true;
}