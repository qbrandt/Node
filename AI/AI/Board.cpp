#include "pch.h"
#include "Board.h"
#include<iostream>
#include <cmath>
#include <exception>
#include "Tile.h"
#include "Board.h"
#include <bitset>


Tile defaultTile;
Tile Board::tiles[11][11] = { defaultTile };

Board::Board() {
	aiPossibleNodes = 0ULL;
	playerPossibleNodes = 0ULL;
	aiPossibleBranches = 0ULL;
	playerPossibleBranches = 0ULL;
}

Board::Board(Board& board)
{
	for (int i = 0; i < 11; i++)
	{
		for (int j = 0; j < 11; j++)
		{
			pieces[i][j] = board.pieces[i][j];
		}
	}
	aiPossibleNodes = board.aiPossibleNodes;
	playerPossibleNodes = board.playerPossibleNodes;
	aiPossibleBranches = board.aiPossibleBranches;
	playerPossibleBranches = board.playerPossibleBranches;
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
					if (j % 2 == 0)
					{
						auto ai = BIT_CHECK(aiPossibleNodes, pieces[i][j].getId());
						auto player = BIT_CHECK(playerPossibleNodes, pieces[i][j].getId());
						if (ai && player)
							result << "n";
						else if (ai || player)
							result << "0";
						else
							result << "O";
					}
					else
					{
						auto ai = BIT_CHECK(aiPossibleBranches, pieces[i][j].getId());
						auto player = BIT_CHECK(playerPossibleBranches, pieces[i][j].getId());
						if (ai && player)
							result << "b";
						else if (ai || player)
							result << "~";
						else
							result << "-";
					}
					//result << (j % 2 == 0 ? "O" : "-");
				}
				else
				{
					if (j % 2 == 0)
					{
						auto ai = BIT_CHECK(aiPossibleBranches, pieces[i][j].getId());
						auto player = BIT_CHECK(playerPossibleBranches, pieces[i][j].getId());
						if (ai && player)
							result << "b";
						else if (ai || player)
							result << "s";
						else
							result << "|";
					}
					else
					{
						result << (char)220;
					}
					//result << (j % 2 == 0 ? '|' : (char)220);
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
	//NEVER add a check here to make sure that the piece isn't already occupied, that would make this conflict with buildBranch in State
	//also make sure that this function is always called after buildBranch
	Piece piece = pieces[loc.Row][loc.Col];
	piece.setOwner(player);
	unsigned long long& branchesYours = player == Status::PLAYER1 ? aiPossibleBranches : playerPossibleBranches;
	unsigned long long& nodesYours = player == Status::PLAYER1 ? aiPossibleNodes : playerPossibleNodes;
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
	//debugging output
	/*std::bitset<36> bitLong(nodesYours);
	cout << "Nodes " << bitLong << endl;*/
}

void Board::AddPossiblesNextToNode(Point loc, Status player)
{
	unsigned long long& branchesYours = player == Status::PLAYER1 ? aiPossibleBranches : playerPossibleBranches;
	if (loc.Row != 0 && pieces[loc.Row - 1][loc.Col].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row - 1][loc.Col].getId());

		/*std::bitset<36> bitLong(branchesYours);

		if (BIT_CHECK(branchesYours, pieces[loc.Row - 1][loc.Col].getId())) {
			std::cout << "First branch added." << endl;
		}
		else {
			std::cout << "First branch failed." << endl;
		}

		cout << bitLong << endl;*/
	}
	if (loc.Col != 0 && pieces[loc.Row][loc.Col - 1].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row][loc.Col - 1].getId());

		/*std::bitset<36> bitLong(branchesYours);

		if (BIT_CHECK(branchesYours, pieces[loc.Row][loc.Col - 1].getId())) {
			std::cout << "Second branch added." << endl;
		}
		else {
			std::cout << "Second branch failed." << bitLong << endl;
		}

		cout << bitLong << endl;*/
	}
	if (loc.Row != 10 && pieces[loc.Row + 1][loc.Col].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row + 1][loc.Col].getId());

		/*std::bitset<36> bitLong(branchesYours);

		if (BIT_CHECK(branchesYours, pieces[loc.Row + 1][loc.Col].getId())) {
			std::cout << "Third branch added." << endl;
		}
		else {
			std::cout << "Third branch failed." << bitLong<< endl;
		}

		cout << bitLong << endl;*/

	}
	if (loc.Col != 10 && pieces[loc.Row][loc.Col + 1].getOwner() == Status::EMPTY)
	{
		BIT_SET(branchesYours, pieces[loc.Row][loc.Col + 1].getId());

		/*std::bitset<36> bitLong(branchesYours);

		if (BIT_CHECK(branchesYours, pieces[loc.Row][loc.Col + 1].getId())) {
			std::cout << "Fourth branch added." << endl;
		}
		else {
			std::cout << "Fourth branch failed." << bitLong << endl;
		}

		cout << bitLong << endl;*/
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