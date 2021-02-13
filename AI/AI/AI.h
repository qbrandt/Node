#pragma once
#include<string>
#include"Tile.h"
#include"Status.h"

using std::string;

class AI
{
private:
	Tile tiles[5][5] = {};
	Status status[11][11] = {};
	int move;
	bool isSmart;
	bool goesFirst;

public:
	AI();
	~AI();
	void GameSetup(string board, bool aiGoesFirst, bool aiIsSmart);
	string GetMove(string move);

private:
	void ResetBoard();
	void SetGameboard(string board);
	string GetRandomMove(string move);
	string GetSmartMove(string move);
};