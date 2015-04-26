#pragma once

class Envelope
{
	double attack;
	double decay;
	double sustain;
	double release;
	double releasePoint;
	int polarity; // 0 = normal, 1 = inverted
	double shape;

	double getValueAt(double time, bool checkPolarity);

public:
	Envelope(void);
	~Envelope(void);

	double getAttack() const { return attack; }
	void setAttack(double a) { attack = a; }

	double getDecay() const { return decay; }
	void setDecay(double d) { decay = d; }

	double getSustain() const { return sustain; }
	void setSustain(double s) { sustain = s; }

	double getRelease() const { return release; }
	void setRelease(double r) { release = r; }

	double getReleasePoint() const { return releasePoint; }
	void setReleasePoint(double rp) { releasePoint = rp; }

	int getPolarity() const { return polarity; }
	void setPolarity(int p) { polarity = p; }

	double getShape() const { return shape; }
	void setShape(double s) { shape = s; }

	double getValueAt(double time);
};
