#pragma once
#include "Status.h"
#include "Color.h"
#include "Network.h"
#include <string>

class Player
{
public:
	Player();
	Player(Player &player);
	void resetPlayer();

	void setName(Status name);
	Status getName() const;

	void incrementResource(Color Color);
	
	void increaseRedResources(int x);
	void decreaseRedResources(int x);
	int getRedResources() const;

	void increaseBlueResources(int x);
	void decreaseBlueResources(int x);
	int getBlueResources() const;

	void increaseYellowResources(int x);
	void decreaseYellowResources(int x);
	int getYellowResources() const;

	void increaseGreenResources(int x);
	void decreaseGreenResources(int x);
	int getGreenResources() const;

	void incrementTiles();
	int getTiles() const;

	void incrementNodes();
	int getNodes() const;

	void incrementBranches1();
	void setBranches1(int x);
	int getBranches1() const;

	void incrementBranches2();
	void setBranches2(int x);
	int getBranches2() const;

	void setNetworks(int nets);
	int getNetworks() const;

	void setLongest();
	Network getLongest() const;

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

