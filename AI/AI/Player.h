#pragma once
#include "Status.h"
#include "Color.h"
#include <string>

class Player
{
public:
	Player();

	void setName(Status name);
	Status getName();

	void incrementResource(Color Color);
	
	void increaseRedResources(int x);
	void decreaseRedResources(int x);
	int getRedResources();

	void increaseBlueResources(int x);
	void decreaseBlueResources(int x);
	int getBlueResources();

	void increaseYellowResources(int x);
	void decreaseYellowResources(int x);
	int getYellowResources();

	void increaseGreenResources(int x);
	void decreaseGreenResources(int x);
	int getGreenResources();

	void incrementTiles();
	int getTiles();

	void incrementNodes();
	int getNodes();

	void incrementBranches();
	int getBranches();

	void setNetworks(int nets);
	int getNetworks();

	bool isLegalTrade(std::string move);

private:
	Status name;

	int redResources;
	int blueResources;
	int yellowResources;
	int greenResources;

	int tiles;
	int nodes;
	int branches;
	int networks;
};

