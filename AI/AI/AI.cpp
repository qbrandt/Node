#include "pch.h"
#include "framework.h"

#include <cmath>
#include <exception>

#include "AI.h"
#include "Point.h"

using std::exception;

AI::AI()
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

void AI::GetBoard(string board)
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
		tiles[location.X][location.Y] = tile;
	}
}

string AI::RandomMove(string move)
{
	return "DUMB";
}

string AI::SmartMove(string move)
{
	return "SMART";
}