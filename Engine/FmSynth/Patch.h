#pragma once

#include <string>
#include <map>
#include <vector>

#include "Operator.h"

using namespace std;

class Patch
{
	string _rootOperatorId;
	map<string, Operator*> _operators;
	map<string, vector<string>> _modulations;

public:
	Patch(void);
	~Patch(void);

	void setRootOperatorId(string id);
	Operator* getRootOperator();

	map<string, Operator*> getOperators();
	void addOperator(string id, Operator* op);
	void addModulation(string targetId, string modulatorId);

	vector<string> getModulatorIdsFor(string operatorId);
	void connectModulators();

	void setOperatorParameter(string operatorId, string parameterId, double value);
};

