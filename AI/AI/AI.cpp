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