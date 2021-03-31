#pragma once
#include<vector>
#include <time.h>
#include<exception>
#include "Board.h"
#include "Player.h"
#include "Monte Carlo.h"

using std::exception;

using std::vector;

class State
{
public:
#pragma region Constructors
	State() 
	{
		board = new Board;
		currentPlayer = new Player;
		currentPlayer->setName(Status::PLAYER1);
		currentOpponent = new Player;
		currentOpponent->setName(Status::PLAYER2);
		moveString = "";
		player_to_move = (int)Status::PLAYER1;
		moveCount = 0;
	}
	
	State(const State& state)
	{
		currentPlayer = new Player(*state.currentPlayer);
		currentOpponent = new Player(*state.currentOpponent);
		board = new Board(*state.board);
		moveString = state.moveString;
		player_to_move = (int)currentPlayer->getName();
		moveCount = state.moveCount;
	}

	~State() 
	{
		delete board;
		delete currentPlayer;
		delete currentOpponent;
	}

#pragma endregion


	typedef int Move;
	static const Move no_move = 0;


	int player_to_move;

	bool won() const {
		bool result = false;
		int points = 0;

		if (currentPlayer->getLongest() == Network::NET1 && currentOpponent->getLongest() == Network::NET1 && currentPlayer->getBranches1() > currentOpponent->getBranches1()) {
			points += 2;
		}
		else if (currentPlayer->getLongest() == Network::NET2 && currentOpponent->getLongest() == Network::NET1 && currentPlayer->getBranches2() > currentOpponent->getBranches1()) {
			points += 2;
		}
		else if (currentPlayer->getLongest() == Network::NET1 && currentOpponent->getLongest() == Network::NET2 && currentPlayer->getBranches1() > currentOpponent->getBranches2()) {
			points += 2;
		}
		else if (currentPlayer->getLongest() == Network::NET2 && currentOpponent->getLongest() == Network::NET2 && currentPlayer->getBranches2() > currentOpponent->getBranches2()) {
			points += 2;
		}
		else if (currentOpponent->getLongest() == Network::NEITHER) {
			points += 2;
		}

		points = points + currentPlayer->getNodes() + currentPlayer->getTiles();

		if (points >= 10) {
			result = true;
		}

		return result;
	}
	bool lost() const {
		bool result = false;
		int points = 0;

		if (currentOpponent->getLongest() == Network::NET1 && currentPlayer->getLongest() == Network::NET1 && currentOpponent->getBranches1() > currentPlayer->getBranches1()) {
			points = 2;
		}
		else if (currentOpponent->getLongest() == Network::NET2 && currentPlayer->getLongest() == Network::NET1 && currentOpponent->getBranches2() > currentPlayer->getBranches1()) {
			points = 2;
		}
		else if (currentOpponent->getLongest() == Network::NET1 && currentPlayer->getLongest() == Network::NET2 && currentOpponent->getBranches1() > currentPlayer->getBranches2()) {
			points = 2;
		}
		else if (currentOpponent->getLongest() == Network::NET2 && currentPlayer->getLongest() == Network::NET2 && currentOpponent->getBranches2() > currentPlayer->getBranches2()) {
			points = 2;
		}
		else if (currentPlayer->getLongest() == Network::NEITHER) {
			points = 2;
		}

		points = points + currentOpponent->getNodes() + currentOpponent->getTiles();

		if (points >= 10) {
			result = true;
		}

		return result;
	}

	void addResources() {
		for (int i = 0; i < 11; i++) {
			for (int j = 0; j < 11; j++) {
				if (board->pieces[i][j].getType() == PieceType::NODE && board->pieces[i][j].getOwner() == currentPlayer->getName()) {
					if (i - 1 >= 0 && j - 1 >= 0 && board->tiles[i - 1][j - 1].getColor() != Color::BLANK && board->pieces[i - 1][j - 1].getOwner() != Status::INVALID && board->pieces[i - 1][j - 1].getOwner() != currentOpponent->getName()) {
						currentPlayer->incrementResource(board->tiles[i - 1][j - 1].getColor());
					}

					if (i - 1 >= 0 && j + 1 <= 10 && board->tiles[i - 1][j + 1].getColor() != Color::BLANK && board->pieces[i - 1][j + 1].getOwner() != Status::INVALID && board->pieces[i - 1][j + 1].getOwner() != currentOpponent->getName()) {
						currentPlayer->incrementResource(board->tiles[i - 1][j + 1].getColor());
					}

					if (i + 1 <= 10 && j - 1 >= 0 && board->tiles[i + 1][j - 1].getColor() != Color::BLANK && board->pieces[i + 1][j - 1].getOwner() != Status::INVALID && board->pieces[i + 1][j - 1].getOwner() != currentOpponent->getName()) {
						currentPlayer->incrementResource(board->tiles[i + 1][j - 1].getColor());
					}

					if (i + 1 <= 10 && j + 1 <= 10 && board->tiles[i + 1][j + 1].getColor() != Color::BLANK && board->pieces[i + 1][j + 1].getOwner() != Status::INVALID && board->pieces[i + 1][j + 1].getOwner() != currentOpponent->getName()) {
						currentPlayer->incrementResource(board->tiles[i + 1][j + 1].getColor());
					}
				}
			}
		}
	}
	void mergeNetworks() {
		//TODO: make this more efficient by only checking the columns that could be branches
		currentPlayer->setBranches1(currentPlayer->getBranches1() + currentPlayer->getBranches2());
		currentPlayer->setBranches2(0);
		currentPlayer->setNetworks(1);
		currentPlayer->setLongest();

		for (int i = 0; i < 11; i++) {
			for (int j = i % 2; j < 11; j++) {
				if (board->pieces[i][j].getType() == PieceType::BRANCH && board->pieces[i][j].getOwner() == currentPlayer->getName()) {
					board->pieces[i][j].setNet(Network::NET1);
				}
			}
		}
	}
	bool isLegal(std::string move) {
		bool result = true;
		if (move == "") {
			result = false;
		}
		else if ((move[0] == '+' && currentPlayer->isLegalTrade(move)) || move[0] != '+') {
			int start = 0;
			int red = currentPlayer->getRedResources();
			int blue = currentPlayer->getBlueResources();
			int yellow = currentPlayer->getYellowResources();
			int green = currentPlayer->getGreenResources();
			if (move[0] == '+') {
				start = 5;

				for (int i = 1; i < 4; i++) {
					switch (move[i]) {
					case 'R':
						red--;
						break;
					case 'B':
						blue--;
						break;
					case 'G':
						green--;
						break;
					case 'Y':
						yellow--;
						break;
					}
				}

				switch (move[4]) {
				case 'R':
					red++;
					break;
				case 'B':
					blue++;
					break;
				case 'G':
					green++;
					break;
				case 'Y':
					yellow++;
					break;
				}
			}

			Point location;
			for (int i = start; i < move.length() && result == true; i = i + 3) {
				bool enoughResources = false;
				bool alreadyOwned = true;
				bool connected = false;
				bool capturedByO = false;
				int id = stoi(move.substr(i + 1, 2));
				if (move[i] == 'N' && green >= 2 && yellow >= 2) {
					enoughResources = true;
					green = green - 2;
					yellow = yellow - 2;
				}
				else if (move[i] == 'B' && blue >= 1 && red >= 1) {
					enoughResources = true;
					blue--;
					red--;
				}

				if (enoughResources) {
					if (move[i] == 'N') {
						location = Point::GetNodeCoordinate(id);
					}
					else {
						location = Point::GetBranchCoordinate(id);
					}

					if (board->pieces[location.Row][location.Col].getOwner() == Status::EMPTY) {
						alreadyOwned = false;
					}

					if (!alreadyOwned) {
						if (move[i] == 'N' && ((location.Row - 1 >= 0 && board->pieces[location.Row - 1][location.Col].getOwner() == currentPlayer->getName())
							|| (location.Row + 1 <= 10 && board->pieces[location.Row + 1][location.Col].getOwner() == currentPlayer->getName())
							|| (location.Col - 1 >= 0 && board->pieces[location.Row][location.Col - 1].getOwner() == currentPlayer->getName())
							|| (location.Col + 1 <= 10 && board->pieces[location.Row][location.Col + 1].getOwner() == currentPlayer->getName()))) {
							connected = true;
							if ((location.Row - 1 >= 0 && location.Col - 1 >= 0 && board->pieces[location.Row - 1][location.Col - 1].getOwner() == currentOpponent->getName())
								|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board->pieces[location.Row - 1][location.Col + 1].getOwner() == currentOpponent->getName())
								|| (location.Row + 1 <= 10 && location.Col - 1 >= 0 && board->pieces[location.Row + 1][location.Col - 1].getOwner() == currentOpponent->getName())
								|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board->pieces[location.Row + 1][location.Col + 1].getOwner() == currentOpponent->getName())) {
								capturedByO = true;
							}
						}
						else if (move[i] == 'B' && (location.Row == 0 || location.Row == 2 || location.Row == 4 || location.Row == 6 || location.Row == 8 || location.Row == 10)
							&& ((location.Col - 2 >= 0 && board->pieces[location.Row][location.Col - 2].getOwner() == currentPlayer->getName())
								|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board->pieces[location.Row - 1][location.Col - 1].getOwner() == currentPlayer->getName())
								|| (location.Row + 1 <= 10 && location.Col - 1 >= 0 && board->pieces[location.Row + 1][location.Col - 1].getOwner() == currentPlayer->getName())
								|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board->pieces[location.Row - 1][location.Col + 1].getOwner() == currentPlayer->getName())
								|| (location.Col + 2 <= 10 && board->pieces[location.Row][location.Col + 2].getOwner() == currentPlayer->getName())
								|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board->pieces[location.Row + 1][location.Col + 1].getOwner() == currentPlayer->getName()))) {
							connected = true;
							if ((location.Row - 1 >= 0 && board->pieces[location.Row - 1][location.Col].getOwner() == currentOpponent->getName())
								|| (location.Row + 1 <= 10 && board->pieces[location.Row + 1][location.Col].getOwner() == currentOpponent->getName())) {
								capturedByO = true;
							}
						}
						else if (move[i] == 'B' && (location.Row == 1 || location.Row == 3 || location.Row == 5 || location.Row == 7 || location.Row == 9)
							&& ((location.Row + 1 <= 10 && location.Col - 1 >= 0 && board->pieces[location.Row + 1][location.Col - 1].getOwner() == currentPlayer->getName())
								|| (location.Row + 2 <= 10 && board->pieces[location.Row + 2][location.Col].getOwner() == currentPlayer->getName())
								|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board->pieces[location.Row + 1][location.Col + 1].getOwner() == currentPlayer->getName())
								|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board->pieces[location.Row - 1][location.Col - 1].getOwner() == currentPlayer->getName())
								|| (location.Row - 2 >= 0 && board->pieces[location.Row - 2][location.Col].getOwner() == currentPlayer->getName())
								|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board->pieces[location.Row - 1][location.Col + 1].getOwner() == currentPlayer->getName()))) {
							connected = true;
							if ((location.Col - 1 >= 0 && board->pieces[location.Row][location.Col - 1].getOwner() == currentOpponent->getName())
								|| (location.Col + 1 <= 10 && board->pieces[location.Row][location.Col + 1].getOwner() == currentOpponent->getName())) {
								capturedByO = true;
							}
						}
					}
				}

				if (!enoughResources || alreadyOwned || !connected || capturedByO) {
					result = false;
				}
			}
		}

		return result;
	}
	bool isLegalOpening(std::string move) {
		bool result = false;

		if (((move[0] == 'N' && move[3] == 'B') || (move[0] == 'B' && move[3] == 'N')) && move.length() == 6) {
			Point nodeLocation;
			Point branchLocation;
			int id;
			id = stoi(move.substr(1, 2));

			if (move[0] == 'N') {
				nodeLocation = Point::GetNodeCoordinate(id);
				id = stoi(move.substr(4, 2));
				branchLocation = Point::GetBranchCoordinate(id);

			}
			else {
				branchLocation = Point::GetBranchCoordinate(id);
				id = stoi(move.substr(4, 2));
				nodeLocation = Point::GetNodeCoordinate(id);
			}

			if (board->pieces[nodeLocation.Row][nodeLocation.Col].getOwner() == Status::EMPTY
				&& board->pieces[branchLocation.Row][branchLocation.Col].getOwner() == Status::EMPTY
				&& ((nodeLocation.Row == branchLocation.Row && nodeLocation.Col - 1 == branchLocation.Col)
					|| (nodeLocation.Row - 1 == branchLocation.Row && nodeLocation.Col == branchLocation.Col)
					|| (nodeLocation.Row == branchLocation.Row && nodeLocation.Col + 1 == branchLocation.Col)
					|| (nodeLocation.Row + 1 == branchLocation.Row && nodeLocation.Col == branchLocation.Col))) {
				result = true;
			}
		}

		return result;
	}
	void identifyCapturedTiles(int row, int col) {
		int visited[13][2] = {};
		int length = 0;

		if (tileCaptured(row, col, visited, &length)) {
			for (int i = 0; i < length; i++) {
				board->pieces[visited[i][0]][visited[i][1]].setOwner(currentPlayer->getName());
				currentPlayer->incrementTiles();
			}
		}
	}
	bool tileVisited(int row, int col, int visited[13][2], int length) {
		bool result = false;

		for (int i = 0; i < length && result == false; i++) {
			if (row == visited[i][0] && col == visited[i][1]) {
				result = true;
			}
		}

		return result;
	}
	bool tileCaptured(int row, int col, int visited[13][2], int* length) {
		bool result = false;
		bool side1 = false;
		bool side2 = false;
		bool side3 = false;
		bool side4 = false;

		if (row - 1 >= 0 && board->pieces[row - 1][col].getType() == PieceType::BRANCH) {
			if (board->pieces[row - 1][col].getOwner() == Status::EMPTY && row - 2 >= 0 && board->pieces[row - 2][col].getType() == PieceType::TILE) {
				if (tileVisited(row - 2, col, visited, *length)) {
					visited[*length][0] = row;
					visited[*length][1] = col;
					++* length;
					side1 = true;
				}
				else {
					visited[*length][0] = row;
					visited[*length][1] = col;
					++* length;
					side1 = tileCaptured(row - 2, col, visited, length);
				}
			}
			else if (board->pieces[row - 1][col].getOwner() == currentPlayer->getName()) {
				visited[*length][0] = row;
				visited[*length][1] = col;
				++* length;
				side1 = true;
			}
		}

		if (side1 && col + 1 <= 10 && board->pieces[row][col + 1].getType() == PieceType::BRANCH) {
			if (board->pieces[row][col + 1].getOwner() == Status::EMPTY && col + 2 <= 10 && board->pieces[row][col + 2].getType() == PieceType::TILE) {
				if (tileVisited(row, col + 2, visited, *length)) {
					side2 = true;
				}
				else {
					side2 = tileCaptured(row, col + 2, visited, length);
				}
			}
			else if (board->pieces[row][col + 1].getOwner() == currentPlayer->getName()) {
				side2 = true;
			}
		}

		if (side1 && side2 && row + 1 <= 10 && board->pieces[row + 1][col].getType() == PieceType::BRANCH) {
			if (board->pieces[row + 1][col].getOwner() == Status::EMPTY && row + 2 <= 10 && board->pieces[row + 2][col].getType() == PieceType::TILE) {
				if (tileVisited(row + 2, col, visited, *length)) {
					side3 = true;
				}
				else {
					side3 = tileCaptured(row + 2, col, visited, length);
				}
			}
			else if (board->pieces[row + 1][col].getOwner() == currentPlayer->getName()) {
				side3 = true;
			}
		}

		if (side1 && side2 && side3 && col - 1 >= 0 && board->pieces[row][col - 1].getType() == PieceType::BRANCH) {
			if (board->pieces[row][col - 1].getOwner() == Status::EMPTY && col - 2 >= 0 && board->pieces[row][col - 2].getType() == PieceType::TILE) {
				if (tileVisited(row, col - 2, visited, *length)) {
					side4 = true;
				}
				else {
					side4 = tileCaptured(row, col - 2, visited, length);
				}
			}
			else if (board->pieces[row][col - 1].getOwner() == currentPlayer->getName()) {
				side4 = true;
			}
		}

		if (side1 && side2 && side3 && side4) {
			result = true;
		}

		return result;
	}
	bool nodeBought(std::string move, Point* coordinates) {
		bool confirmed = false;
		if (coordinates->Row != 12) {
			if (currentPlayer->getGreenResources() >= 2 && currentPlayer->getYellowResources() >= 2 && board->pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
				currentPlayer->decreaseGreenResources(2);
				currentPlayer->decreaseYellowResources(2);

				confirmed = true;
			}
		}

		return confirmed;
	}
	void buildNode(Point* coordinates) {
		int nodes;
		if (board->pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
			board->pieces[coordinates->Row][coordinates->Col].setOwner(currentPlayer->getName());

			currentPlayer->incrementNodes();

			if (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board->tiles[coordinates->Row - 1][coordinates->Col - 1].getColor() != Color::BLANK) {
				nodes = board->connectingNodes(coordinates->Row - 1, coordinates->Col - 1);
				if (nodes > board->tiles[coordinates->Row - 1][coordinates->Col - 1].getDots()
					&& board->pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() != currentPlayer->getName()) {
					board->pieces[coordinates->Row - 1][coordinates->Col - 1].setOwner(Status::INVALID);
				}
			}

			if (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board->tiles[coordinates->Row - 1][coordinates->Col + 1].getColor() != Color::BLANK) {
				nodes = board->connectingNodes(coordinates->Row - 1, coordinates->Col + 1);
				if (nodes > board->tiles[coordinates->Row - 1][coordinates->Col + 1].getDots()
					&& board->pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() != currentPlayer->getName()) {
					board->pieces[coordinates->Row - 1][coordinates->Col + 1].setOwner(Status::INVALID);
				}
			}

			if (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board->tiles[coordinates->Row + 1][coordinates->Col - 1].getColor() != Color::BLANK) {
				nodes = board->connectingNodes(coordinates->Row + 1, coordinates->Col - 1);
				if (nodes > board->tiles[coordinates->Row + 1][coordinates->Col - 1].getDots()
					&& board->pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() != currentPlayer->getName()) {
					board->pieces[coordinates->Row + 1][coordinates->Col - 1].setOwner(Status::INVALID);
				}
			}

			if (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board->tiles[coordinates->Row + 1][coordinates->Col + 1].getColor() != Color::BLANK) {
				nodes = board->connectingNodes(coordinates->Row + 1, coordinates->Col + 1);
				if (nodes > board->tiles[coordinates->Row + 1][coordinates->Col + 1].getDots()
					&& board->pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() != currentPlayer->getName()) {
					board->pieces[coordinates->Row + 1][coordinates->Col + 1].setOwner(Status::INVALID);
				}
			}
		}

		board->AddNode(*coordinates, currentPlayer->getName());
	}
	bool branchBought(std::string move, Point* coordinates) {
		bool confirmed = false;
		if (coordinates->Row != 12) {
			if (currentPlayer->getRedResources() >= 1 && currentPlayer->getBlueResources() >= 1 && board->pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
				currentPlayer->decreaseRedResources(1);
				currentPlayer->decreaseBlueResources(1);

				confirmed = true;
			}
		}

		return confirmed;
	}
	void buildBranch(Point* coordinates) {

		if (board->pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
			board->pieces[coordinates->Row][coordinates->Col].setOwner(currentPlayer->getName());

			if (currentPlayer->getNetworks() == 2) {
				bool net1 = false;
				bool net2 = false;

				if (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer->getName()) {
					if (board->pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board->pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer->getName()) {
					if (board->pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board->pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer->getName()) {
					if (board->pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board->pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer->getName()) {
					if (board->pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board->pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10) {
					if (coordinates->Col - 2 >= 0 && board->pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer->getName()) {
						if (board->pieces[coordinates->Row][coordinates->Col - 2].getNet() == Network::NET1) {
							net1 = true;
						}
						else if (board->pieces[coordinates->Row][coordinates->Col - 2].getNet() == Network::NET2) {
							net2 = true;
						}
					}

					if (coordinates->Col + 2 <= 10 && board->pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer->getName()) {
						if (board->pieces[coordinates->Row][coordinates->Col + 2].getNet() == Network::NET1) {
							net1 = true;
						}
						else if (board->pieces[coordinates->Row][coordinates->Col + 2].getNet() == Network::NET2) {
							net2 = true;
						}
					}
				}
				else {
					if (coordinates->Row - 2 >= 0 && board->pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer->getName()) {
						if (board->pieces[coordinates->Row - 2][coordinates->Col].getNet() == Network::NET1) {
							net1 = true;
						}
						else if (board->pieces[coordinates->Row - 2][coordinates->Col].getNet() == Network::NET2) {
							net2 = true;
						}
					}

					if (coordinates->Row + 2 <= 10 && board->pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer->getName()) {
						if (board->pieces[coordinates->Row + 2][coordinates->Col].getNet() == Network::NET1) {
							net1 = true;
						}
						else if (board->pieces[coordinates->Row + 2][coordinates->Col].getNet() == Network::NET2) {
							net2 = true;
						}
					}
				}

				if (net1 == true && net2 == true) {
					mergeNetworks();
					currentPlayer->incrementBranches1();
					currentPlayer->setLongest();
					board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
				}
				else if (net1 == true) {
					currentPlayer->incrementBranches1();
					currentPlayer->setLongest();
					board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
				}
				else if (net2 == true) {
					currentPlayer->incrementBranches2();
					currentPlayer->setLongest();
					board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET2);
				}
			}
			else if (currentPlayer->getNetworks() == 1) {
				bool connected = false;
				if ((coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer->getName())
					|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer->getName())
					|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer->getName())
					|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer->getName())) {
					connected = true;
				}
				else if ((coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10)
					&& ((coordinates->Col - 2 >= 0 && board->pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer->getName())
						|| (coordinates->Col + 2 <= 10 && board->pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer->getName()))) {
					connected = true;
				}
				else if ((coordinates->Row - 2 >= 0 && board->pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer->getName())
					|| (coordinates->Row + 2 <= 10 && board->pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer->getName())) {
					connected = true;
				}

				if (connected == true) {
					currentPlayer->incrementBranches1();
					currentPlayer->setLongest();
					board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
				}
				else {
					currentPlayer->incrementBranches2();
					currentPlayer->setNetworks(2);
					currentPlayer->setLongest();
					board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET2);
				}
			}
			else {
				currentPlayer->setNetworks(1);
				currentPlayer->incrementBranches1();
				currentPlayer->setLongest();
				board->pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
			}

			//if each end touches the same network as the placed branch (which if there were two different networks touching this branch anyway then they should have been merged earlier)
			if (coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10) {
				if ((coordinates->Col - 2 >= 0 && board->pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row][coordinates->Col - 2].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
					|| (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
					|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())) {
					if ((coordinates->Col + 2 <= 10 && board->pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board->pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row][coordinates->Col + 2].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
						|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
						|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())) {
						if (coordinates->Row - 1 >= 0 && board->pieces[coordinates->Row - 1][coordinates->Col].getType() == PieceType::TILE) {
							identifyCapturedTiles(coordinates->Row - 1, coordinates->Col);
						}

						if (coordinates->Row + 1 <= 10 && board->pieces[coordinates->Row + 1][coordinates->Col].getType() == PieceType::TILE) {
							identifyCapturedTiles(coordinates->Row + 1, coordinates->Col);
						}
					}
				}
			}
			else if (coordinates->Row == 1 || coordinates->Row == 3 || coordinates->Row == 5 || coordinates->Row == 7 || coordinates->Row == 9) {
				if ((coordinates->Row - 2 >= 0 && board->pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row - 2][coordinates->Col].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
					|| (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
					|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())) {
					if ((coordinates->Row + 2 <= 10 && board->pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row + 2][coordinates->Col].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
						|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())
						|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer->getName() && board->pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == board->pieces[coordinates->Row][coordinates->Col].getNet())) {
						if (coordinates->Col - 1 >= 0 && board->pieces[coordinates->Row][coordinates->Col - 1].getType() == PieceType::TILE) {
							identifyCapturedTiles(coordinates->Row, coordinates->Col - 1);
						}

						if (coordinates->Col + 1 <= 10 && board->pieces[coordinates->Row][coordinates->Col + 1].getType() == PieceType::TILE) {
							identifyCapturedTiles(coordinates->Row, coordinates->Col + 1);
						}
					}
				}
			}
		}

		board->AddBranch(*coordinates, currentPlayer->getName());
	}
	void updateGameBoard(std::string move, bool isOpening) {
		//updates the game board with the most recent move
		bool resourcesUpdated = false;
		std::string tempMove = "";
		std::string tempId = "";
		int id = 40;
		Point coordinates;
		bool trade = false;
		int start = 0;

		if (move[0] == '+') {
			trade = currentPlayer->tradeMade(move);
		}

		if (trade) {
			start = 5;
		}

		for (int i = start; i < move.length(); i = i + 3) {
			tempMove = "";
			tempId = "";
			tempMove.push_back(move[i]);
			tempMove.push_back(move[i + 1]);
			tempMove.push_back(move[i + 2]);
			tempId.push_back(tempMove[1]);
			tempId.push_back(tempMove[2]);
			id = stoi(tempId);

			if (move[i] == 'N') {
				coordinates = Point::GetNodeCoordinate(id);
				if (coordinates.Row != 12) {
					if (isOpening) {
						resourcesUpdated = true;
					}
					else if (nodeBought(tempMove, &coordinates)) {
						resourcesUpdated = true;
					}

					if (resourcesUpdated == true) {
						buildNode(&coordinates);
					}
				}
			}
			else if (move[i] == 'B') {
				coordinates = Point::GetBranchCoordinate(id);
				if (coordinates.Row != 12) {
					if (isOpening) {
						resourcesUpdated = true;
					}
					else if (branchBought(tempMove, &coordinates)) {
						resourcesUpdated = true;
					}

					if (resourcesUpdated == true) {
						buildBranch(&coordinates);
					}
				}
			}
		}
	}
	void swapPlayerAndOpponent() {
		Player* temp;
		temp = currentPlayer;
		currentPlayer = currentOpponent;
		currentOpponent = temp;
	}
	void setBoard(std::string board) {
		currentPlayer->resetPlayer();
		currentOpponent->resetPlayer();
		this->board->ResetBoard();
		this->board->SetGameboard(board);
	}
	std::string getRandomMove() {
		srand(time(NULL));
		int red = currentPlayer->getRedResources();
		int blue = currentPlayer->getBlueResources();
		int yellow = currentPlayer->getYellowResources();
		int green = currentPlayer->getGreenResources();
		int branches = 0;
		int nodes = 0;
		std::string move = "";


		while (red > 0 && blue > 0) {
			branches++;
			red--;
			blue--;
		}

		while (yellow > 1 && green > 1) {
			nodes++;
			yellow = yellow - 2;
			green = green - 2;
		}

		if (red > 0 && yellow + green >= 3) {
			if (yellow >= green) {
				if (yellow >= 3) {
					move = "+YYYB";
				}
				else {
					move = "+YYGB";
				}
			}
			else {
				if (green >= 3) {
					move = "+GGGB";
				}
				else {
					move = "+GGYB";
				}
			}
			branches++;
		}
		else if (blue > 0 && yellow + green >= 3) {
			if (yellow >= green) {
				if (yellow >= 3) {
					move = "+YYYR";
				}
				else {
					move = "+YYGR";
				}
			}
			else {
				if (green >= 3) {
					move = "+GGGR";
				}
				else {
					move = "+GGYR";
				}
			}
			branches++;
		}
		else if (yellow > 1 && green == 1 && red + blue >= 3) {
			if (red >= blue) {
				if (red >= 3) {
					move = "+RRRG";
				}
				else {
					move = "+RRBG";
				}
			}
			else {
				if (blue >= 3) {
					move = "+BBBG";
				}
				else {
					move = "+BBRG";
				}
			}
			nodes++;
		}
		else if (green > 1 && yellow == 1 && red + blue >= 3) {
			if (red >= blue) {
				if (red >= 3) {
					move = "+RRRY";
				}
				else {
					move = "+RRBY";
				}
			}
			else {
				if (blue >= 3) {
					move = "+BBBY";
				}
				else {
					move = "+BBRY";
				}
			}
			nodes++;
		}
		else if (red > 3 && blue == 0) {
			move = "+RRRB";
			branches++;
		}
		else if (blue > 3 && red == 0) {
			move = "+BBBR";
			branches++;
		}
		else if (red > 3 && (yellow == 0 || green == 0)) {
			if (yellow == 0) {
				move = "+RRRY";
			}
			else if (green == 0) {
				move = "+RRRG";
			}
		}
		else if (blue > 3 && (yellow == 0 || green == 0)) {
			if (yellow == 0) {
				move = "+BBBY";
			}
			else if (green == 0) {
				move = "+BBBG";
			}
		}
		else if (yellow > 3 && (red == 0 || blue == 0 || green == 0)) {
			if (red == 0) {
				move = "+YYYR";
			}
			else if (blue == 0) {
				move = "+YYYB";
			}
			else if (green == 0) {
				move = "+YYYG";
			}
		}
		else if (green > 3 && (red == 0 || blue == 0 || yellow == 0)) {
			if (red == 0) {
				move = "+GGGR";
			}
			else if (blue == 0) {
				move = "+GGGB";
			}
			else if (yellow == 0) {
				move = "+GGGY";
			}
		}

		if (nodes == 0 && branches == 0 && move == "") {
			move = "X00";
		}
		else if (nodes != 0 || branches != 0) {
			int id = 40;
			std::string potentialMove = move;
			int tens = 0;
			int ones = 0;
			char aChar = '0';
			bool selectedBranch[36];
			int branchesSelected = 0;
			for (int i = 0; i < 36; i++) {
				selectedBranch[i] = false;
			}
			bool selectedNode[24];
			int nodesSelected = 0;
			for (int i = 0; i < 24; i++) {
				selectedNode[i] = false;
			}

			for (int i = 0; i < branches; i++) {
				std::string potentialBranch = "";
				bool legal = false;
				while (!legal && branchesSelected < 36) {
					potentialBranch = "";
					potentialMove = move;
					id = rand() % 36;
					if (!selectedBranch[id]) {
						potentialBranch.push_back('B');
						tens = id / 10;
						ones = id % 10;
						aChar = '0' + tens;
						potentialBranch.push_back(aChar);
						aChar = '0' + ones;
						potentialBranch.push_back(aChar);
						potentialMove += potentialBranch;
						legal = isLegal(potentialMove);
						selectedBranch[id] = true;
						branchesSelected++;
					}
				}
				move.append(potentialBranch);
			}

			for (int i = 0; i < nodes; i++) {
				std::string potentialNode = "";
				bool legal = false;
				while (!legal && nodesSelected < 24) {
					potentialNode = "";
					potentialMove = move;
					id = rand() % 24;
					if (!selectedNode[id]) {
						potentialNode.push_back('N');
						tens = id / 10;
						ones = id % 10;
						aChar = '0' + tens;
						potentialNode.push_back(aChar);
						aChar = '0' + ones;
						potentialNode.push_back(aChar);
						potentialMove += potentialNode;
						legal = isLegal(potentialMove);
						selectedNode[id] = true;
						nodesSelected++;
					}
				}
				move.append(potentialNode);
			}
		}

		return move;
	}
	std::string getRandomOpeningMove() {
		srand(time(NULL));
		std::string result = "";
		int nodeId = 40;
		int branchId = 40;
		int direction = 0;
		int tens = 0;
		int ones = 0;
		char aChar = '0';
		std::string potentialMove = "";
		Point coordinates;

		bool legal = false;
		bool validNode = false;
		bool validBranch = false;
		while (!legal) {
			potentialMove = "";
			while (!validNode) {
				nodeId = rand() % 24;
				coordinates = Point::GetNodeCoordinate(nodeId);
				if (board->pieces[coordinates.Row][coordinates.Col].getOwner() == Status::EMPTY) {
					validNode = true;
					while (!validBranch) {
						direction = rand() % 4;
						switch (direction) {
						case 0:
							if (coordinates.Row - 1 >= 0 && board->pieces[coordinates.Row - 1][coordinates.Col].getType() == PieceType::BRANCH
								&& board->pieces[coordinates.Row - 1][coordinates.Col].getOwner() == Status::EMPTY) {
								branchId = board->pieces[coordinates.Row - 1][coordinates.Col].getId();
								validBranch = true;
							}
							break;
						case 1:
							if (coordinates.Col + 1 <= 10 && board->pieces[coordinates.Row][coordinates.Col + 1].getType() == PieceType::BRANCH
								&& board->pieces[coordinates.Row][coordinates.Col + 1].getOwner() == Status::EMPTY) {
								branchId = board->pieces[coordinates.Row][coordinates.Col + 1].getId();
								validBranch = true;
							}
						case 2:
							if (coordinates.Row + 1 <= 10 && board->pieces[coordinates.Row + 1][coordinates.Col].getType() == PieceType::BRANCH
								&& board->pieces[coordinates.Row + 1][coordinates.Col].getOwner() == Status::EMPTY) {
								branchId = board->pieces[coordinates.Row + 1][coordinates.Col].getId();
								validBranch = true;
							}
						case 3:
							if (coordinates.Col - 1 >= 0 && board->pieces[coordinates.Row][coordinates.Col - 1].getType() == PieceType::BRANCH
								&& board->pieces[coordinates.Row][coordinates.Col - 1].getOwner() == Status::EMPTY) {
								branchId = board->pieces[coordinates.Row][coordinates.Col - 1].getId();
								validBranch = true;
							}
						}
					}
				}
			}

			potentialMove.push_back('N');
			tens = nodeId / 10;
			ones = nodeId % 10;
			aChar = '0' + tens;
			potentialMove.push_back(aChar);
			aChar = '0' + ones;
			potentialMove.push_back(aChar);
			potentialMove.push_back('B');
			tens = branchId / 10;
			ones = branchId % 10;
			aChar = '0' + tens;
			potentialMove.push_back(aChar);
			aChar = '0' + ones;
			potentialMove.push_back(aChar);
			legal = isLegalOpening(potentialMove);
		}

		result = potentialMove;
		return result;
	}

#pragma region Generate_Moves

	vector<State> GenerateAllStartResources()
	{
		short resources[4];
		resources[0] = currentPlayer->getGreenResources();
		resources[1] = currentPlayer->getYellowResources();
		resources[2] = currentPlayer->getRedResources();
		resources[3] = currentPlayer->getBlueResources();

		vector<State> states;
		auto noTrade = new State(*this);
		noTrade->moveString = "";
		states.push_back(*noTrade);

		if (resources[0] + resources[1] + resources[2] + resources[3] < 3)
		{
			return states;
		}

		//Trade One Color
		for (int i = 0; i < 4; i++)
		{
			std::string tradeString = "+";
			if (resources[i] >= 3)
			{
				State takeResources(*this);
				switch (i)
				{
				case 0:
					takeResources.currentPlayer->decreaseGreenResources(3);
					tradeString = tradeString + "GGG";
					break;
				case 1:
					takeResources.currentPlayer->decreaseYellowResources(3);
					tradeString = tradeString + "YYY";
					break;
				case 2:
					takeResources.currentPlayer->decreaseRedResources(3);
					tradeString = tradeString + "RRR";
					break;
				case 3:
					takeResources.currentPlayer->decreaseBlueResources(3);
					tradeString = tradeString + "BBB";
					break;
				}

				if (i != 0)
				{
					State color(takeResources);
					color.currentPlayer->increaseGreenResources(1);
					color.moveString = tradeString + "G";
					states.push_back(color);
				}
				if (i != 1)
				{
					State color(takeResources);
					color.currentPlayer->increaseYellowResources(1);
					color.moveString = tradeString + "Y";
					states.push_back(color);
				}
				if (i != 2)
				{
					State color(takeResources);
					color.currentPlayer->increaseRedResources(1);
					color.moveString = tradeString + "R";
					states.push_back(color);
				}
				if (i != 3)
				{
					State color(takeResources);
					color.currentPlayer->increaseBlueResources(1);
					color.moveString = tradeString + "B";
					states.push_back(color);
				}
			}
		}
		//Trade Two Colors
		for (int i = 0; i < 4; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				if (resources[i] > 1 && resources[j] > 0)
				{
					std::string tradeString = "+";
					State takeResources(*this);
					switch (i)
					{
					case 0:
						takeResources.currentPlayer->decreaseGreenResources(2);
						tradeString = tradeString + "GG";
						break;
					case 1:
						takeResources.currentPlayer->decreaseYellowResources(2);
						tradeString = tradeString + "YY";
						break;
					case 2:
						takeResources.currentPlayer->decreaseRedResources(2);
						tradeString = tradeString + "RR";
						break;
					case 3:
						takeResources.currentPlayer->decreaseBlueResources(2);
						tradeString = tradeString + "BB";
						break;
					}
					switch (j)
					{
					case 0:
						takeResources.currentPlayer->decreaseGreenResources(1);
						tradeString = tradeString + "G";
						break;
					case 1:
						takeResources.currentPlayer->decreaseYellowResources(1);
						tradeString = tradeString + "Y";
						break;
					case 2:
						takeResources.currentPlayer->decreaseRedResources(1);
						tradeString = tradeString + "R";
						break;
					case 3:
						takeResources.currentPlayer->decreaseBlueResources(1);
						tradeString = tradeString + "B";
						break;
					}
					if (i != 0 && j != 0)
					{
						State color(takeResources);
						color.currentPlayer->increaseGreenResources(1);
						color.moveString = tradeString + "G";
						states.push_back(color);
					}
					if (i != 1 && j != 1)
					{
						State color(takeResources);
						color.currentPlayer->increaseYellowResources(1);
						color.moveString = tradeString + "Y";
						states.push_back(color);
					}
					if (i != 2 && j != 2)
					{
						State color(takeResources);
						color.currentPlayer->increaseRedResources(1);
						color.moveString = tradeString + "R";
						states.push_back(color);
					}
					if (i != 3 && j != 3)
					{
						State color(takeResources);
						color.currentPlayer->increaseBlueResources(1);
						color.moveString = tradeString + "B";
						states.push_back(color);
					}
				}
				if (resources[i] > 0 && resources[j] > 1)
				{
					std::string tradeString = "+";
					State takeResources(*this);
					switch (i)
					{
					case 0:
						takeResources.currentPlayer->decreaseGreenResources(1);
						tradeString = tradeString + "G";
						break;
					case 1:
						takeResources.currentPlayer->decreaseYellowResources(1);
						tradeString = tradeString + "Y";
						break;
					case 2:
						takeResources.currentPlayer->decreaseRedResources(1);
						tradeString = tradeString + "R";
						break;
					case 3:
						takeResources.currentPlayer->decreaseBlueResources(1);
						tradeString = tradeString + "B";
						break;
					}
					switch (j)
					{
					case 0:
						takeResources.currentPlayer->decreaseGreenResources(2);
						tradeString = tradeString + "GG";
						break;
					case 1:
						takeResources.currentPlayer->decreaseYellowResources(2);
						tradeString = tradeString + "YY";
						break;
					case 2:
						takeResources.currentPlayer->decreaseRedResources(2);
						tradeString = tradeString + "RR";
						break;
					case 3:
						takeResources.currentPlayer->decreaseBlueResources(2);
						tradeString = tradeString + "BB";
						break;
					}
					if (i != 0 && j != 0)
					{
						State color(takeResources);
						color.currentPlayer->increaseGreenResources(1);
						color.moveString = tradeString + "G";
						states.push_back(color);
					}
					if (i != 1 && j != 1)
					{
						State color(takeResources);
						color.currentPlayer->increaseYellowResources(1);
						color.moveString = tradeString + "Y";
						states.push_back(color);
					}
					if (i != 2 && j != 2)
					{
						State color(takeResources);
						color.currentPlayer->increaseRedResources(1);
						color.moveString = tradeString + "R";
						states.push_back(color);
					}
					if (i != 3 && j != 3)
					{
						State color(takeResources);
						color.currentPlayer->increaseBlueResources(1);
						color.moveString = tradeString + "B";
						states.push_back(color);
					}
				}
			}
		}
		//Trade Three Colors
		for (int i = 0; i < 4; i++)
		{
			for (int j = i + 1; j < 4; j++)
			{
				for (int k = j + 1; k < 4; k++)
				{
					if (resources[i] > 0 && resources[j] > 0 && resources[k] > 0)
					{
						std::string tradeString = "+";
						std::string gainString = "";
						State takeResources(*this);
						if (i == 0)
						{
							takeResources.currentPlayer->decreaseGreenResources(1);
							tradeString = tradeString + "G";
						}
						else
						{
							takeResources.currentPlayer->increaseGreenResources(1);
							gainString = "G";
						}
						if (i == 1 || j == 1)
						{
							takeResources.currentPlayer->decreaseYellowResources(1);
							tradeString = tradeString + "Y";
						}
						else
						{
							takeResources.currentPlayer->increaseYellowResources(1);
							gainString = "Y";
						}
						if (j == 2 || k == 2)
						{
							takeResources.currentPlayer->decreaseRedResources(1);
							tradeString = tradeString + "R";
						}
						else
						{
							takeResources.currentPlayer->increaseRedResources(1);
							gainString = "R";
						}
						if (k == 3)
						{
							takeResources.currentPlayer->decreaseBlueResources(1);
							tradeString = tradeString + "B";
						}
						else
						{
							takeResources.currentPlayer->increaseBlueResources(1);
							gainString = "B";
						}
						takeResources.moveString = tradeString + gainString;
						states.push_back(takeResources);
					}
				}
			}
		}

		return states;
	}
	vector<State> GenerateAllOpeningMoves()
	{
		vector<State> states;
		Status player = currentPlayer->getName();
		for (int i = 0; i < 11; i += 2)
		{
			for (int j = 0; j < 11; j += 2)
			{
				if (board->pieces[i][j].getOwner() == Status::EMPTY && board->pieces[i][j].getType() == PieceType::NODE)
				{
					State node(*this);
					Point nodeLocation(i, j);
					node.buildNode(&nodeLocation);
					node.moveString = "N";
					if (board->pieces[i][j].getId() < 10) {
						node.moveString = node.moveString + "0";
					}
					node.moveString = node.moveString + std::to_string(board->pieces[i][j].getId());

					if (i != 0 && board->pieces[i - 1][j].getOwner() == Status::EMPTY)
					{
						State branch1(node);
						Point branch1Coords(i - 1, j);
						branch1.buildBranch(&branch1Coords);
						branch1.moveString = node.moveString + "B";
						if (board->pieces[i - 1][j].getId() < 10) {
							branch1.moveString = branch1.moveString + "0";
						}
						branch1.moveString = branch1.moveString + std::to_string(board->pieces[i - 1][j].getId());
						states.push_back(branch1);
					}

					if (i != 10 && board->pieces[i + 1][j].getOwner() == Status::EMPTY)
					{
						State branch2(node);
						Point branch2Coords(i + 1, j);
						branch2.buildBranch(&branch2Coords);
						//added for debugging purposes
						/*for (int x = 0; x < 36; x++) {
							if (BIT_CHECK(branch2.board->aiPossibleBranches, x)) {
								std::cout << "Branch " << x << std::endl;
							}
						}

						for (int x = 0; x < 24; x++) {
							if (BIT_CHECK(branch2.board->aiPossibleNodes, x)) {
								std::cout << "Node " << x << std::endl;
							}
						}*/
						//end section
						branch2.moveString = node.moveString + "B";
						if (board->pieces[i + 1][j].getId() < 10) {
							branch2.moveString = branch2.moveString + "0";
						}
						branch2.moveString = branch2.moveString + std::to_string(board->pieces[i + 1][j].getId());
						states.push_back(branch2);
					}

					if (j != 0 && board->pieces[i][j - 1].getOwner() == Status::EMPTY)
					{
						State branch3(node);
						Point branch3Coords(i, j - 1);
						branch3.buildBranch(&branch3Coords);
						branch3.moveString = node.moveString + "B";
						if (board->pieces[i][j - 1].getId() < 10) {
							branch3.moveString = branch3.moveString + "0";
						}
						branch3.moveString = branch3.moveString + std::to_string(board->pieces[i][j - 1].getId());
						states.push_back(branch3);
					}

					if (j != 10 && board->pieces[i][j + 1].getOwner() == Status::EMPTY)
					{
						State branch4(node);
						Point branch4Coords(i, j + 1);
						branch4.buildBranch(&branch4Coords);
						branch4.moveString = node.moveString + "B";
						if (board->pieces[i][j + 1].getId() < 10) {
							branch4.moveString = branch4.moveString + "0";
						}
						branch4.moveString = branch4.moveString + std::to_string(board->pieces[i][j + 1].getId());
						states.push_back(branch4);
					}
				}
			}
		}
		return states;
	}
	vector<State> GenerateAllBranches(unsigned long long visited) {
		vector<State> states;
		unsigned long long& possibleBranches = currentPlayer->getName() == Status::PLAYER1 ? board->aiPossibleBranches : board->playerPossibleBranches;
		if (currentPlayer->getRedResources() >= 1 && currentPlayer->getBlueResources() >= 1) {
			for (int i = 0; i < 36; i++) {
				Point location = Point::GetBranchCoordinate(i);
				if (board->pieces[location.Row][location.Col].getOwner() == Status::EMPTY
					&& BIT_CHECK(possibleBranches, i) == 1
					&& BIT_CHECK(visited, i) != 1) {
					//create the new state
					State newState(*this);
					//update the potential resources
					newState.currentPlayer->decreaseRedResources(1);
					newState.currentPlayer->decreaseBlueResources(1);
					//build the potential branch
					newState.buildBranch(&location);
					//add the branch to the visited branches
					BIT_SET(visited, i) = 1;
					//update the moveString of the new state
					newState.moveString = moveString + "B";
					if (i < 10) {
						newState.moveString = newState.moveString + "0";
					}
					newState.moveString = newState.moveString + std::to_string(i);
					//call the recursion
					states.push_back(newState);
					vector<State> newStates = newState.GenerateAllBranches(visited);
					for (int j = 0; j < newStates.size(); j++) {
						states.push_back(newStates[j]);
					}
				}
			}
		}

		return states;
	}
	vector<State> GenerateAllBranches() {
		unsigned long long visited = 0;
		vector<State> states = GenerateAllBranches(visited);
		return states;
	}
	vector<State> GenerateAllNodes(unsigned long long visited) {
		vector<State> states;
		const unsigned long long& possibleNodes = currentPlayer->getName() == Status::PLAYER1 ? board->aiPossibleNodes : board->playerPossibleNodes;
		unsigned long long possibleMoves = BIT_FLIP_ALL(visited) & possibleNodes;
		if (currentPlayer->getYellowResources() >= 2 && currentPlayer->getGreenResources() >= 2) {
			for (int i = 0; i < 24; i++) {
				Point location = Point::GetNodeCoordinate(i);
				if (board->pieces[location.Row][location.Col].getOwner() == Status::EMPTY
					&& BIT_CHECK(possibleMoves, i)) {
					//create the new state
					State newState(*this);
					//update the potential resources
					newState.currentPlayer->decreaseYellowResources(2);
					newState.currentPlayer->decreaseGreenResources(2);
					//build the potential branch
					newState.buildNode(&location);
					//add the branch to the visited branches
					BIT_SET(visited, i) = 1;
					//update the move string of new state
					newState.moveString = moveString + "N";
					if (i < 10) {
						newState.moveString = newState.moveString + "0";
					}
					newState.moveString = newState.moveString + std::to_string(i);
					//call the recursion
					states.push_back(newState);
					vector<State> newStates = newState.GenerateAllNodes(visited);
					for (int j = 0; j < newStates.size(); j++) {
						states.push_back(newStates[j]);
					}
				}
			}
		}

		return states;
	}
	vector<State> GenerateAllNodes() {
		unsigned long long visited = 0;
		vector<State> states = GenerateAllNodes(visited);
		return states;
	}
	vector<State> GenerateAllMoves() {
		if (possibleMoves.size() == 0) {
			vector<State> states;
			vector<State> branchStates;
			vector<State> nodeStates;
			if (moveCount == 23)
			{
				int i = 0;
			}
			states = GenerateAllStartResources();
			for (int i = 0; i < states.size(); i++) {
				branchStates.push_back(states[i]);
				vector<State> newStates;
				newStates = states[i].GenerateAllBranches();
				for (int j = 0; j < newStates.size(); j++) {
					branchStates.push_back(newStates[j]);
				}
			}

			for (int i = 0; i < branchStates.size(); i++) {
				nodeStates.push_back(branchStates[i]);
				vector<State> newStates;
				newStates = branchStates[i].GenerateAllNodes();
				for (int j = 0; j < newStates.size(); j++) {
					nodeStates.push_back(newStates[j]);
				}
			}

			possibleMoves = nodeStates;
		}


		return possibleMoves;
	}
	
#pragma endregion

	string GetState()
	{
		std::stringstream result;

		result << "-----------------------------------------------------------" << endl;
		result << endl << moveCount << endl << endl;

		result << (currentPlayer->getName() == Status::PLAYER1 ? "AI" : "Player") << endl;
		result << endl;

		result << "Blue:\t" << currentPlayer->getBlueResources() << std::endl;
		result << "Red:\t" << currentPlayer->getRedResources() << std::endl;
		result << "Green:\t" << currentPlayer->getGreenResources() << std::endl;
		result << "Yellow:\t" << currentPlayer->getYellowResources() << std::endl;
		result << std::endl;
		result << endl;
		result << (currentOpponent->getName() == Status::PLAYER1 ? "AI" : "Player") << endl;
		result << endl;
		result << "Blue:\t" << currentOpponent->getBlueResources() << std::endl;
		result << "Red:\t" << currentOpponent->getRedResources() << std::endl;
		result << "Green:\t" << currentOpponent->getGreenResources() << std::endl;
		result << "Yellow:\t" << currentOpponent->getYellowResources() << std::endl;
		result << endl;
		result << "Move String: " << getMoveString();
		result << endl;
		result << board->GetBoard();
		return result.str();
	}

#pragma region Monte_Carlo_Interface
	
	std::string getMoveString() {
		return moveString;
	}
	void do_move(Move move) {
		attest(0 <= move && move < possibleMoves.size());

		State& moveState = possibleMoves[move];
		cout << moveCount << ". Moves allowed: " << possibleMoves.size() << endl;
		currentPlayer = new Player(*moveState.currentPlayer);
		currentOpponent = new Player(*moveState.currentOpponent);
		board = new Board(*moveState.board);
		moveString = moveState.moveString;
		player_to_move = (int)currentPlayer->getName();

		//cout << this->GetState() << endl;

		incrementMoveCount();
		possibleMoves.clear();

		if (moveCount != 2)
		{
			swapPlayerAndOpponent();
		}

		/*if (moveCount < 4 && isLegalOpening(possibleMoves[move].moveString)) {
			updateGameBoard(possibleMoves[move].moveString, true);
			moveCount++;
		}
		else if (isLegal(possibleMoves[move].moveString)) {
			updateGameBoard(possibleMoves[move].moveString, false);
			moveCount++;
		}
		else
		{
			throw new exception("Invalid move passed");
		}*/

		
	}

	template <typename RandomEngine>
	void do_random_move(RandomEngine* engine) {
		dattest(has_moves());
		check_invariant();
		if (moveCount >= 4)
		{
			addResources();
		}
		auto moveVector = get_moves();
		std::uniform_int_distribution<Move> moves(0, possibleMoves.size() - 1);

		auto move = moves(*engine);

		do_move(move);

		/*if (moveCount < 4 && isLegalOpening(possibleMoves[move].moveString)) {
			do_move(move);
		}
		else if (isLegal(possibleMoves[move].moveString)) {
			do_move(move);
		}
		else {
			throw new exception("Invalid move passed");
		}*/
	}
	bool has_moves() const {
		return !won() && !lost();
	}

	vector<Move> get_moves() {
		vector<Move> moves;
		if (has_moves()) {
			if (possibleMoves.size() == 0)
			{
				if (moveCount < 4) {
					possibleMoves = GenerateAllOpeningMoves();
				}
				else {
					possibleMoves = GenerateAllMoves();
				}
			}

			int previousSize = moves.size();
			moves.resize(possibleMoves.size());
			for (int i = previousSize; i < possibleMoves.size(); i++)
			{
				moves[i] = i;
			}
			
		}
		return moves;
	}
	double get_result(int current_player_to_move) const {
		dattest(!has_moves());

		if (won()) {
			return 0.0;
		}
		else if (lost()) {
			return 1.0;
		}
		else {
			return 0.5;
		}
	}
	void incrementMoveCount() {
		moveCount++;
	}

#pragma endregion
	
	vector<State> possibleMoves = {};
private:
	Board* board;
	Player* currentPlayer;
	Player* currentOpponent;
	std::string moveString;
	int moveCount;
	void check_invariant() const {
		attest(player_to_move == 1 || player_to_move == 2);
	}
};


