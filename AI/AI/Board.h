#pragma once
#include "Piece.h"
#include "Tile.h"

class Board
{
public:
	Board();
	void copyBoard();
	int connectingNodes(int row, int col);
	Piece pieces[11][11] = {};
	Tile tiles[11][11] = {};
	//add a link to the static array of tiles, can't remember how to do that and I'm not looking it up rn
private:
	//
};

