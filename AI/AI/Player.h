#pragma once
#include "Status.h"
#include "Color.h"
#include "Network.h"
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

	void incrementBranches1();
	int getBranches1();

	void incrementBranches2();
	int getBranches2();

	void setNetworks(int nets);
	int getNetworks();
	void mergeNetworks();

	void setLongest();
	Network getLongest();

	bool isLegalTrade(std::string move);
	bool tradeMade(std::string move);

private:
	Status name;

	int redResources;
	int blueResources;
	int yellowResources;
	int greenResources;

	int tiles;
	int nodes;
	int branches1;
	int branches2;
	int networks;
	Network longest;
};

