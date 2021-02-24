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
	move = 0;
	initialState->setBoard(board);
	goesFirst = aiGoesFirst;
	isSmart = aiIsSmart;
}

string AI::GetMove(string move)
{
	initialState->swapPlayerAndOpponent();
	if (this->move >= 4) {
		initialState->addResources();
	}
	if (this->move < 4 && move != "X00") {
		
		initialState->updateGameBoard(move, true);
		
		this->move++;
	}
	else if (move != "X00") {
		initialState->updateGameBoard(move, false);
		this->move++;
	}
	else if (this->move >= 4) {
		this->move++;
	}
	initialState->swapPlayerAndOpponent();


	if (this->move >= 4) {
		initialState->addResources();
	}
	string response = isSmart ? GetSmartMove(move) : GetRandomMove(move);
	if (response != "X00")
	{
		initialState->updateGameBoard(response, this->move < 4);
	}
	if (this->move < 4 && response != "X00")
	{
		this->move++;
	}
	return response;
}

void AI::PrintAI()
{
	initialState->PrintState();
}

string AI::GetRandomMove(string move)
{
	string result = "";
	if (goesFirst == true && this->move == 2) {
		result = "X00";
	}
	else if (this->move < 4) {
		result = initialState->getRandomOpeningMove();
	}
	else {
		result = initialState->getRandomMove();
	}

	return result;
}

string AI::GetSmartMove(string move)
{
	return "SMART";
}