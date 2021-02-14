#pragma once
#include "Board.h"
#include "Player.h"
class State
{
public:
	State();

	void addResources(Status currentPlayer);
	bool isLegal(std::string move, Status p, Player* player);
	bool isLegalOpening(std::string move, Player* player);
private:
	Board board;
	Player player1;
	Player player2;
};

