#pragma once
#include"Tile.h"
#include "Piece.h"
#include "Monte Carlo.h"
#include "State.hpp"
#include "RedirectOutputs.h"


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
	string GetAI();
	bool winner();
	bool loser();

private:
	string GetRandomMove(string move);
	string GetSmartMove(string move);
	MCTS::ComputeOptions options;
	capture_outputs* captureMonteCarlo;
};