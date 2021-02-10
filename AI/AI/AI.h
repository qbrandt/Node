#pragma once
#include<string>
#include"Tile.h"
#include"Status.h"

using std::string;

class AI
{
private:
	Tile tiles[5][5];
	Status status[11][11];
public:
	AI();
	void GetBoard(string board);
	string RandomMove(string move);
	string SmartMove(string move);
};