#pragma once
#include "Piece.h"
#include "Tile.h"
#include <string>
#include <sstream>


#define BIT_SET(num, id) ((num) |= (1ULL<<(id)))
#define BIT_CLEAR(num, id) ((num) &= ~(1ULL<<(id)))
#define BIT_FLIP(num, id) ((num) ^= (1ULL<<(id)))
#define BIT_CHECK(num, id) (!!((num) & (1ULL<<(id)))) 

using std::string;
using std::stringstream;
using std::endl;

class Board
{
public:
	Board();
	Board(Board& board);
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
	void AddNode(int id, Status player);
	void AddNode(Point loc, Status player);
	void AddBranch(int id, Status player);
	void AddBranch(Point loc, Status player);
private:
	void AddPossiblesNextToNode(Point loc, Status player);
};

