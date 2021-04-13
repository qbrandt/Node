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
	MCTS::ComputeOptions options;
	captureMonteCarlo = new capture_outputs(std::cerr);
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
		initialState->incrementMoveCount();
		this->move++;
	}
	else if (move != "X00") {
		initialState->updateGameBoard(move, false);
		initialState->incrementMoveCount();
		this->move++;
	}
	else if (this->move >= 4) {
		initialState->incrementMoveCount();
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
		initialState->incrementMoveCount();
		this->move++;
	}
	else if (this->move >= 4) {
		initialState->incrementMoveCount();
		this->move++;
	}
	return response;
}

string AI::GetAI()
{
	string monteCarlo  = captureMonteCarlo->contents();

	//this is an ugly hack... a really ugly hack, sorry
	delete captureMonteCarlo;
	captureMonteCarlo = new capture_outputs(std::cerr);

	stringstream result;
	result << "Move\t" << this->move << std::endl;
	result << std::endl;
	result << initialState->GetState();
	result << endl;
	result << "------------------------------------------" << endl;
	result << monteCarlo;
	result << "------------------------------------------" << endl;
	result << endl;
	return result.str();
}


bool AI::winner() {
	bool result = false;
	
	if (goesFirst && initialState->won()) {
		result = true;
	}
	else if (!goesFirst && !initialState->lost() && initialState->won()) {
		result = true;
	}

	return result;
}

bool AI::loser() {
	bool result = false;

	if (!goesFirst && initialState->lost()) {
		result = true;
	}
	else if (goesFirst && !initialState->won() && initialState->lost()) {
		result = true;
	}

	return result;
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
	string result = "";

	if (!goesFirst || this->move != 2) 
	{
		result = MCTS::compute_move(*initialState, options);;
	}

	if (result == "") {
		result = "X00";
	}

	return result;
}