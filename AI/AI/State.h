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
	bool nodeBought(std::string move, Status p, Player* player, Point* coordinates);
	void buildNode(Status p, Player* player, Point* coordinates);
	bool branchBought(std::string move, Status p, Player* player, Point* coordinates);
	void buildBranch(Status p, Player* player, Point* coordinates);
	void updateGameBoard(std::string move, Status p, Player* player, bool isOpening);
	Player* getPlayer(Status p);
private:
	Board board;
	Player player1;
	Player player2;
};

