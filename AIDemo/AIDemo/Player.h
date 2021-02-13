#pragma once
#include "Enums.h"

class Player {
public:
	Player();
	void changePlayerName(playerName p);
	playerName getPlayerName();
	void increaseRedResources(int x);
	void decreaseRedResources(int x);
	int getRedResourceNumber();
	void increaseBlueResources(int x);
	void decreaseBlueResources(int x);
	int getBlueResourceNumber();
	void increaseYellowResources(int x);
	void decreaseYellowResources(int x);
	int getYellowResourceNumber();
	void increaseGreenResources(int x);
	void decreaseGreenResources(int x);
	int getGreenResourceNumber();
	void incrementNodeNumber();
	int getNodeNumber();
	void incrementBranchNumber();
	int getBranchNumber();
	void incrementTileNumber();
	int getTileNumber();
	void incrementResource(color c);

private:
	playerName name;
	int redResources;
	int blueResources;
	int yellowResources;
	int greenResources;
	int nodes;
	int branches;
	int tiles;
};