#pragma once

#include "Enums.h"
#include "NodeBranch.h"
#include <iostream>
#include <string>
#include <vector>
using namespace std;

class Tile {
public:
	Tile();
	void changeTileColor(char colorChar);
	void changePossibleNodes(char nodeNumberChar);
	void incrementCurrentNodes();
	void changeOwner(playerName p);
	color getTileColor();
	int getPossibleNodeNumber();
	int getCurrentNodeNumber();
	playerName getTileOwner();
	void changeTileID(int ID);
	int getTileID();

private:
	color tileColor;
	int possibleNodes;
	int currentNodes;
	playerName owner;
	int tileID;
};