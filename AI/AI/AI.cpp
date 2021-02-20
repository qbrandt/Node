#include "pch.h"
#include "framework.h"

#include "AI.h"
#include "Point.h"

using std::exception;

AI::AI()
{
	isSmart = false;
	goesFirst = false;
	move = 0;
	initialState = new State;
}

AI::~AI()
{
	delete initialState;
}

void AI::GameSetup(string board, bool aiGoesFirst, bool aiIsSmart)
{
	initialState->setBoard(board);
	goesFirst = aiGoesFirst;
	isSmart = aiIsSmart;
}

string AI::GetMove(string move)
{
	if (this->move < 4 && move != "X00") {
		initialState->swapPlayerAndOpponent();
		initialState->updateGameBoard(move, true);
		initialState->swapPlayerAndOpponent();
		this->move++;
	}
	else if (move != "X00") {
		initialState->swapPlayerAndOpponent();
		initialState->updateGameBoard(move, false);
		initialState->swapPlayerAndOpponent();
		this->move++;
	}
	else if (this->move > 4) {
		this->move++;
	}

	return isSmart ? GetSmartMove(move) : GetRandomMove(move);
}

string AI::GetRandomMove(string move)
{
	string result = "";
	if (goesFirst == true && this->move == 2) {
		result = "X00";
	}
	else if (this->move < 4) {
		result = initialState->getRandomOpeningMove();
		this->move++;
	}
	else {
		result = initialState->getRandomMove();
		this->move++;
	}

	return result;
}

string AI::GetSmartMove(string move)
{
	return "SMART";
}