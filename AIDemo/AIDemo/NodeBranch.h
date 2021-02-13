#pragma once
#include "Enums.h"
#include <vector>
#include "Tile.h"

class NodeBranch {
public:
	NodeBranch();
	void changeNodeBranchOwner(playerName p);
	void changeNodeBranchID(int num);
	void changeNodeBranchType(nodeOrBranch t);
	playerName getNodeBranchOwner();
	int getNodeBranchID();
	nodeOrBranch getNodeBranchType();

private:
	playerName owner;
	int id;
	nodeOrBranch type;
};