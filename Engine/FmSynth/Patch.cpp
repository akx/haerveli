#include "Patch.h"

using namespace std;

typedef map<string, Operator*>::iterator OperatorIterator;
typedef vector<string>::iterator ModulatorIterator;

Patch::Patch(void)
{
}


Patch::~Patch(void)
{
}


void Patch::setRootOperatorId(string id)
{
	_rootOperatorId = id; 
}

Operator* Patch::getRootOperator()
{
	return _operators[_rootOperatorId];
}

map<string, Operator*> Patch::getOperators()
{
	return _operators;
}

void Patch::addOperator(string id, Operator* op)
{
	_operators[id] = op;
}

void Patch::addModulation(string targetId, string modulatorId)
{
	_modulations[targetId].push_back(modulatorId);
}

vector<string> Patch::getModulatorIdsFor(string operatorId)
{
	return _modulations[operatorId];
}

void Patch::connectModulators()
{
	for(OperatorIterator iter = _operators.begin(); iter!=_operators.end(); ++iter)
	{
		string targetId = iter->first;
		Operator* target = iter->second;
		target->clearModulators();

		vector<string> modulatorIds = getModulatorIdsFor(targetId);

		for(ModulatorIterator iter2 = modulatorIds.begin(); iter2 != modulatorIds.end(); ++iter2)
		{
			string modulatorId = *iter2;
			target->addModulator(_operators[modulatorId]);
		}
	}
}

void Patch::setOperatorParameter(string operatorId, string parameterId, double value)
{
	Operator* target = _operators[operatorId];
	target->setParameter(parameterId, value, true);
}