#include "NodeBranch.h"

NodeBranch::NodeBranch() {
	owner = none;
	id = 40;
	type = neither;
}

void NodeBranch::changeNodeBranchOwner(playerName p) {
	owner = p;
}

void NodeBranch::changeNodeBranchID(int num) {
	id = num;
}

void NodeBranch::changeNodeBranchType(nodeOrBranch t) {
	type = t;
}

playerName NodeBranch::getNodeBranchOwner() {
	return owner;
}

int NodeBranch::getNodeBranchID() {
	return id;
}

nodeOrBranch NodeBranch::getNodeBranchType() {
	return type;
}