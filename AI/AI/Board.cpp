#include "pch.h"
#include "Board.h"
#include<iostream>
#include <cmath>
#include <exception>
#include "Tile.h"
#include "Board.h"


Tile defaultTile;
Tile Board::tiles[11][11] = { defaultTile };

Board::Board() {
	aiPossibleNodes = 0UL;
	playerPossibleNodes = 0UL;
	aiPossibleBranches = 0UL;
	playerPossibleBranches = 0UL;
}

Board::Board(Board& board)
{
	memcpy(pieces, board.pieces, 121);
	aiPossibleNodes = 0UL;
	playerPossibleNodes = 0UL;
	aiPossibleBranches = 0UL;
	playerPossibleBranches = 0UL;
}

int Board::connectingNodes(int row, int col) {
	int result = 0;

	if (row - 1 >= 0 && col - 1 >= 0 && (pieces[row - 1][col - 1].getOwner() == Status::PLAYER1 || pieces[row - 1][col - 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row - 1 >= 0 && col + 1 <= 10 && (pieces[row - 1][col + 1].getOwner() == Status::PLAYER1 || pieces[row - 1][col + 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row + 1 <= 10 && col - 1 >= 0 && (pieces[row + 1][col - 1].getOwner() == Status::PLAYER1 || pieces[row + 1][col - 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row + 1 <= 10 && col + 1 <= 10 && (pieces[row + 1][col + 1].getOwner() == Status::PLAYER1 || pieces[row + 1][col + 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	return result;
}

string Board::GetBoard()
{
	stringstream result;
	for (int i = 0; i < 11; i++)
	{
		for (int j = 0; j < 11; j++)
		{
#ifdef NDEBUG
			switch (pieces[i][j].getOwner())
			{
			case Status::INVALID:
				result << "  ";
				break;
			case Status::EMPTY:
				if (i % 2 == 0)
				{
					result << (j % 2 == 0 ? "O" : "-");
				}
				else
				{
					result << (j % 2 == 0 ? "||" : "H");
				}
				break;
			case Status::PLAYER1:
				result << "A";
				break;
			case Status::PLAYER2:
				result << "P";
				break;
			}
#else
			switch (pieces[i][j].getOwner())
			{
			case Status::INVALID:
				result << " ";
				break;
			case Status::EMPTY:
				if (i % 2 == 0)
				{
					result << (j % 2 == 0 ? "O" : "-");
				}
				else
				{
					result << (j % 2 == 0 ? '|' : (char)220);
				}
				break;
			case Status::PLAYER1:
				result << "A";
				break;
			case Status::PLAYER2:
				result << "P";
				break;
			}
#endif

			result << " ";
		}
		result << std::endl;
	}
	return result.str();
}

void Board::AddNode(int id, Status player)
{
	AddNode(Point::GetNodeCoordinate(id), player);
}

void Board::AddNode(Point loc, Status player)
{
	Piece piece = pieces[loc.Row][loc.Col];
	piece.setOwner(player);
	BIT_CLEAR(aiPossibleNodes, piece.getId());
	BIT_CLEAR(playerPossibleNodes, piece.getId());
}

void Board::AddBranch(int id, Status player)
{
	AddBranch(Point::GetBranchCoordinate(id), player);
}

void Board::AddBranch(Point loc, Status player)
{
	Piece piece = pieces[loc.Row][loc.Col];
	piece.setOwner(player);
	unsigned long& branchesYours = player == Status::PLAYER1 ? aiPossibleBranches : playerPossibleBranches;
	unsigned long& nodesYours = player == Status::PLAYER1 ? aiPossibleNodes : playerPossibleNodes;
	BIT_CLEAR(aiPossibleBranches, piece.getId());
	BIT_CLEAR(playerPossibleBranches, piece.getId());
	if (loc.Row % 2 == 0)
	{
		Point left(loc.Row, loc.Col - 1);
		Point right(loc.Row, loc.Col + 1);

		if (pieces[left.Row][left.Col].getOwner() == Status::EMPTY)
		{
			BIT_SET(nodesYours, pieces[left.Row][left.Col].getId());
		}
		if (pieces[right.Row][right.Col].getOwner() == Status::EMPTY)
		{
			BIT_SET(nodesYours, pieces[right.Row][right.Col].getId());
		}

		AddPossiblesNextToNode(left, player);
		AddPossiblesNextToNode(right, player);
	}
	else
	{
		Point up(loc.Row - 1, loc.Col);
		Point down(loc.Row + 1, loc.Col);

		if (pieces[up.Row][up.Col].getOwner() == Status::EMPTY)
		{
			BIT_SET(nodesYours, pieces[up.Row][up.Col].getId());
		}
		if (pieces[down.Row][down.Col].getOwner() == Status::EMPTY)
		{
			BIT_SET(nodesYours, pieces[down.Row][down.Col].getId());
		}

		AddPossiblesNextToNode(up, player);
		AddPossiblesNextToNode(down, player);
	}
}

void Board::AddPossiblesNextToNode(Point loc, Status player)
{
	unsigned long& branchesYours = player == Status::PLAYER1 ? aiPossibleBranches : playerPossibleBranches;
	if (loc.Row != 0 && pieces[loc.Row - 1][loc.Col].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row - 1][loc.Col].getId());
	}
	if (loc.Col != 0 && pieces[loc.Row][loc.Col - 1].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row][loc.Col - 1].getId());
	}
	if (loc.Row != 10 && pieces[loc.Row + 1][loc.Col].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row + 1][loc.Col].getId());
	}
	if (loc.Col != 10 && pieces[loc.Row][loc.Col + 1].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row][loc.Col + 1].getId());
	}
}


void Board::ResetBoard()
{
	int nodeCount = 0;
	int branchCount = 0;
	int tileCount = 0;

	for (int i = 0; i < 11; i++)
	{
		for (int j = 0; j < 11; j++)
		{
			pieces[i][j].setNet(Network::NEITHER);

			if (i % 2 == 0) {
				if (abs(5 - i) + abs(5 - j) <= 6)
				{
					pieces[i][j].setOwner(Status::EMPTY);

					if ((abs(5 - i) + abs(5 - j)) % 2 == 0) {
						pieces[i][j].setType(PieceType::NODE);
						pieces[i][j].setId(nodeCount);
						nodeCount++;
					}
					else if ((abs(5 - i) + abs(5 - j)) % 2 == 1) {
						pieces[i][j].setType(PieceType::BRANCH);
						pieces[i][j].setId(branchCount);
						branchCount++;
					}
				}
				else
				{
					pieces[i][j].setOwner(Status::INVALID);
					pieces[i][j].setType(PieceType::NONE);
				}
			}
			else {
				if (abs(5 - i) + abs(5 - j) <= 5) {
					pieces[i][j].setOwner(Status::EMPTY);

					if ((abs(5 - i) + abs(5 - j)) % 2 == 0) {
						pieces[i][j].setType(PieceType::TILE);
						pieces[i][j].setId(tileCount);
						tileCount++;
					}
					else if ((abs(5 - i) + abs(5 - j)) % 2 == 1) {
						pieces[i][j].setType(PieceType::BRANCH);
						pieces[i][j].setId(branchCount);
						branchCount++;
					}
				}
				else {
					pieces[i][j].setOwner(Status::INVALID);
					pieces[i][j].setType(PieceType::NONE);
				}
			}
		}
	}
}

void Board::SetGameboard(std::string board)
{
	if (board.size() != 26)
		throw new std::exception("Board string representation is not the correct length");

	for (int i = 0; i < 13; i++)
	{
		std::string piece = board.substr(i * 2, 2);
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
			throw new std::exception("Color not known");
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