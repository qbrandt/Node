#pragma once
#include "Piece.h"
#include "Tile.h"
#include <string>

class Board
{
public:
	Board();
	void ResetBoard();
	void SetGameboard(std::string board);
	int connectingNodes(int row, int col);
	Piece pieces[11][11] = {};
	static Tile tiles[11][11];
	void PrintBoard();
private:
	//would make things private, but we're choosing to trust State and making the arrays private would be a pain
};

