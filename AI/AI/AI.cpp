#include "pch.h"
#include "framework.h"

#include <cmath>
#include <exception>

#include "AI.h"
#include "Point.h"

using std::exception;

AI::AI()
{
	isSmart = false;
	goesFirst = false;
	move = 0;
}

AI::~AI()
{
}

void AI::GameSetup(string board, bool aiGoesFirst, bool aiIsSmart)
{
	SetGameboard(board);
	move = 0;
	goesFirst = aiGoesFirst;
	isSmart = aiIsSmart;
}

string AI::GetMove(string move)
{
	return isSmart ? GetSmartMove(move) : GetRandomMove(move);
}

void AI::SetGameboard(string board)
{//check that this works with the new gameboard setup
	if (board.size() != 26)
		throw new exception("Board string representation is not the correct length");

	for (int i = 0; i < 13; i++)
	{
		string piece = board.substr(i, 2);
		Tile tile;
		switch (piece.at(0))
		{
		case 'R':
			tile.setColor(Color::RED);
			break;
		case 'B':
			tile.setColor(Color::BLUE);
			break;
		case 'G':
			tile.setColor(Color::GREEN);
			break;
		case 'Y':
			tile.setColor(Color::YELLOW);
			break;
		case 'X':
			tile.setColor(Color::GREY);
			break;
		default:
			throw new exception("Color not known");
		}

		if (piece.at(1) == 'X')
		{
			tile.setDots(0);
		}
		else
		{
			tile.setDots(piece.at(1) - '0');
		}

		Point location = Point::GetTileCoordinate(i);
		tiles[location.Row][location.Col] = tile;
	}
}

string AI::GetRandomMove(string move)
{
	return "DUMB";
}

string AI::GetSmartMove(string move)
{
	return "SMART";
}

void AI::ResetBoard()
{
	for (int i = 0; i < 11; i++)
	{
		for (int j = 0; j < 11; j++)
		{
			if (abs(5 - i) + abs(5 - j) <= 6)
			{
				pieces[i][j].setOwner(Status::EMPTY);
			}
			else
			{
				pieces[i][j].setOwner(Status::INVALID);
			}
		}
	}
}

string selectMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], Player* player) {
	//empty
	//uses the monte carlo tree search and the heuristic to select a move
	//complete after monte carlo and heuristic
}

string selectOpeningMove(State* state, Status p) {
	//empty
	//uses the heuristic to select an opening move
	//complete after monte carlo and heuristic
}

void AI::makeOpeningOpponentMove(Status p, std::string move, State* state) {
	//add a function that connects this to the game core
	//makes an opening move
	Player* player = state->getPlayer(p);

	if (state->isLegalOpening(move, player)) {
		state->updateGameBoard(move, p, player, true);
		//check that this makes the changes necessary
	}
}

string makeOpeningPlayerMove(Status p, State* state) {
	//uses the AI to make an opening move
	//possibly alter to interface with the game core? more likely will add another function that acts as a door to this one
	//not sure how I'm going to interface with the game core when I need the boards to be initialized in main?
	string move = "";
	Player* player = state->getPlayer(p);
	move = selectOpeningMove(state, p);
	state->updateGameBoard(move, p, player, true);

	return move;
}

void makeOpponentMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* opponent, string move) {
	//convert move entry to integrate with game core
	//enters the opponent's move and updates the game board. returns the opponent if there is a win.
	addResources(opponent, p, nodeBranchBoard, tileBoard);

	if (isLegal(move, p, opponent, tileBoard, nodeBranchBoard)) {
		updateGameBoard(move, p, opponent, tileBoard, nodeBranchBoard, false);
		//also check that this makes all necessary changes
	}
}

string makePlayerMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* player) {
	//complete
	//calls the function to choose a move using the AI and returns a string containing the move
	addResources(player, p, nodeBranchBoard, tileBoard);
	string move = "";
	move = selectMove(tileBoard, nodeBranchBoard, player);
	updateGameBoard(move, p, player, tileBoard, nodeBranchBoard, false);
	//check to see that none of the other functions involved in making a move need to be called

	return move;
}