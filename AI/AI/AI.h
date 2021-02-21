#pragma once
#include"Tile.h"
#include "Piece.h"
#include "State.h"

using std::string;

class AI
{
private:
	State* initialState;
	int move;
	bool isSmart;
	bool goesFirst;

public:
	AI();
	~AI();
	void GameSetup(string board, bool aiGoesFirst, bool aiIsSmart);
	string GetMove(string move);

private:
	string GetRandomMove(string move);
	string GetSmartMove(string move);
};