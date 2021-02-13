#include "pch.h"
#include "framework.h"

#include <cmath>
#include <exception>

#include "AI.h"
#include "Point.h"

using std::exception;

AI::AI()
{

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
{
	if (board.size() != 26)
		throw new exception("Board string representation is not the correct length");

	for (int i = 0; i < 13; i++)
	{
		string piece = board.substr(i, 2);
		Tile tile;
		switch (piece.at(0))
		{
		case 'R':
			tile.Color = Color::RED;
			break;
		case 'B':
			tile.Color = Color::BLUE;
			break;
		case 'G':
			tile.Color = Color::GREEN;
			break;
		case 'Y':
			tile.Color = Color::YELLOW;
			break;
		case 'X':
			tile.Color = Color::BLANK;
			break;
		default:
			throw new exception("Color not known");
		}

		if (piece.at(1) == 'X')
		{
			tile.Dots = 0;
		}
		else
		{
			tile.Dots = piece.at(1) - '0';
		}

		Point location = Point::TileNumberToPoint(i);
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
				status[i][j] = Status::EMPTY;
			}
			else
			{
				status[i][j] = Status::INVALID;
			}
		}
	}
}