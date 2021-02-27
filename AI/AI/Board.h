#pragma once
#include "Piece.h"
#include "Tile.h"
#include <string>
#include <sstream>

/* a=target variable, b=bit number to act upon 0-n */
#define BIT_SET(id, num) ((id) |= (1ULL<<(num)))
#define BIT_CLEAR(id, num) ((id) &= ~(1ULL<<(num)))
#define BIT_FLIP(id, num) ((id) ^= (1ULL<<(num)))
#define BIT_CHECK(id,num) (!!((id) & (1ULL<<(num)))) 

using std::string;
using std::stringstream;
using std::endl;

class Board
{
public:
	Board();
	void ResetBoard();
	void SetGameboard(std::string board);
	int connectingNodes(int row, int col);
	Piece pieces[11][11] = {};
	static Tile tiles[11][11];
	string GetBoard();
	unsigned long aiPossibleNodes;
	unsigned long playerPossibleNodes;
	unsigned long aiPossibleBranches;
	unsigned long playerPossibleBranches;
private:
	//would make things private, but we're choosing to trust State and making the arrays private would be a pain
};

