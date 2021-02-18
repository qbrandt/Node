#include "pch.h"
#include "State.h"

State::State() {

}

bool State::won() {
	bool result = false;
	int points = 0;

	if (currentPlayer.getLongest() == Network::NET1 && currentOpponent.getLongest() == Network::NET1 && currentPlayer.getBranches1() > currentOpponent.getBranches1()) {
		points = 2;
	}
	else if (currentPlayer.getLongest() == Network::NET2 && currentOpponent.getLongest() == Network::NET1 && currentPlayer.getBranches2() > currentOpponent.getBranches1()) {
		points = 2;
	}
	else if (currentPlayer.getLongest() == Network::NET1 && currentOpponent.getLongest() == Network::NET2 && currentPlayer.getBranches1() > currentOpponent.getBranches2()) {
		points = 2;
	}
	else if (currentPlayer.getLongest() == Network::NET2 && currentOpponent.getLongest() == Network::NET2 && currentPlayer.getBranches2() > currentOpponent.getBranches2()) {
		points = 2;
	}

	points = points + currentPlayer.getNodes() + currentPlayer.getTiles();

	if (points >= 10) {
		result = true;
	}

	return result;
}

void State::addResources() {
	for (int i = 0; i < 11; i++) {
		for (int j = 0; j < 11; j++) {
			if (board.pieces[i][j].getType() == PieceType::NODE && board.pieces[i][j].getOwner() == currentPlayer.getName()) {
				if (i - 1 >= 0 && j - 1 >= 0 && board.tiles[i - 1][j - 1].getColor() != Color::BLANK && board.pieces[i - 1][j - 1].getOwner() != Status::INVALID && board.pieces[i - 1][j - 1].getOwner() != currentOpponent.getName()) {
					currentPlayer.incrementResource(board.tiles[i - 1][j - 1].getColor());
				}

				if (i - 1 >= 0 && j + 1 <= 10 && board.tiles[i - 1][j + 1].getColor() != Color::BLANK && board.pieces[i - 1][j + 1].getOwner() != Status::INVALID && board.pieces[i - 1][j + 1].getOwner() != currentOpponent.getName()) {
					currentPlayer.incrementResource(board.tiles[i - 1][j + 1].getColor());
				}

				if (i + 1 <= 10 && j - 1 >= 0 && board.tiles[i + 1][j - 1].getColor() != Color::BLANK && board.pieces[i + 1][j - 1].getOwner() != Status::INVALID && board.pieces[i + 1][j - 1].getOwner() != currentOpponent.getName()) {
					currentPlayer.incrementResource(board.tiles[i + 1][j - 1].getColor());
				}

				if (i + 1 <= 10 && j + 1 <= 10 && board.tiles[i + 1][j + 1].getColor() != Color::BLANK && board.pieces[i + 1][j + 1].getOwner() != Status::INVALID && board.pieces[i + 1][j + 1].getOwner() != currentOpponent.getName()) {
					currentPlayer.incrementResource(board.tiles[i + 1][j + 1].getColor());
				}
			}
		}
	}
}

bool State::isLegal(std::string move) {
	bool result = true;
	if ((move[0] == '+' && currentPlayer.isLegalTrade(move)) || move[0] != '+') {
		int start = 0;
		if (move[0] == '+') {
			start = 5;
		}
		bool enoughResources = false;
		bool alreadyOwned = true;
		bool connected = false;
		bool capturedByO = false;
		std::string tempString;
		int id = 0;
		Point location;
		for (int i = start; i < move.length() && result == true; i = i + 3) {
			tempString.push_back(move[i + 1]);
			tempString.push_back(move[i + 2]);
			id = stoi(tempString);
			if (move[i] == 'N' && currentPlayer.getGreenResources() >= 2 && currentPlayer.getYellowResources() >= 2) {
				enoughResources = true;
			}
			else if (move[i] == 'B' && currentPlayer.getBlueResources() >= 1 && currentPlayer.getRedResources() >= 1) {
				enoughResources = true;
			}

			if (enoughResources) {
				if (move[i] == 'N') {
					location = Point::GetNodeCoordinate(id);
				}
				else {
					location = Point::GetBranchCoordinate(id);
				}

				if (board.pieces[location.Row][location.Col].getOwner() == Status::EMPTY) {
					alreadyOwned = false;
				}

				if (!alreadyOwned) {
					if (move[i] == 'N' && ((location.Row - 1 >= 0 && board.pieces[location.Row - 1][location.Col].getOwner() == currentPlayer.getName())
						|| (location.Row + 1 <= 10 && board.pieces[location.Row + 1][location.Col].getOwner() == currentPlayer.getName())
						|| (location.Col - 1 >= 0 && board.pieces[location.Row][location.Col - 1].getOwner() == currentPlayer.getName())
						|| (location.Col + 1 <= 10 && board.pieces[location.Row][location.Col + 1].getOwner() == currentPlayer.getName()))) {
						connected = true;
						if ((location.Row - 1 >= 0 && location.Col - 1 >= 0 && board.pieces[location.Row - 1][location.Col - 1].getOwner() == currentOpponent.getName())
							|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board.pieces[location.Row - 1][location.Col + 1].getOwner() == currentOpponent.getName())
							|| (location.Row + 1 <= 10 && location.Col - 1 >= 0 && board.pieces[location.Row + 1][location.Col - 1].getOwner() == currentOpponent.getName())
							|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board.pieces[location.Row + 1][location.Col + 1].getOwner() == currentOpponent.getName())) {
							capturedByO = true;
						}
					}
					else if (move[i] == 'B' && (location.Row == 0 || location.Row == 2 || location.Row == 4 || location.Row == 6 || location.Row == 8 || location.Row == 10)
						&& ((location.Col - 2 >= 0 && board.pieces[location.Row][location.Col - 2].getOwner() == currentPlayer.getName())
							|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board.pieces[location.Row - 1][location.Col - 1].getOwner() == currentPlayer.getName())
							|| (location.Row + 1 <= 10 && location.Col - 1 >= 0 && board.pieces[location.Row + 1][location.Col - 1].getOwner() == currentPlayer.getName())
							|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board.pieces[location.Row - 1][location.Col + 1].getOwner() == currentPlayer.getName())
							|| (location.Col + 2 <= 10 && board.pieces[location.Row][location.Col + 2].getOwner() == currentPlayer.getName())
							|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board.pieces[location.Row + 1][location.Col + 1].getOwner() == currentPlayer.getName()))) {
						connected = true;
						if ((location.Row - 1 >= 0 && board.pieces[location.Row - 1][location.Col].getOwner() == currentOpponent.getName())
							|| (location.Row + 1 <= 10 && board.pieces[location.Row + 1][location.Col].getOwner() == currentOpponent.getName())) {
							capturedByO = true;
						}
					}
					else if (move[i] == 'B' && (location.Row == 1 || location.Row == 3 || location.Row == 5 || location.Row == 7 || location.Row == 9)
						&& ((location.Row + 1 <= 10 && location.Col - 1 >= 0 && board.pieces[location.Row + 1][location.Col - 1].getOwner() == currentPlayer.getName())
							|| (location.Row + 2 <= 10 && board.pieces[location.Row + 2][location.Col].getOwner() == currentPlayer.getName())
							|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board.pieces[location.Row + 1][location.Col + 1].getOwner() == currentPlayer.getName())
							|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board.pieces[location.Row - 1][location.Col - 1].getOwner() == currentPlayer.getName())
							|| (location.Row - 2 >= 0 && board.pieces[location.Row - 2][location.Col].getOwner() == currentPlayer.getName())
							|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board.pieces[location.Row - 1][location.Col + 1].getOwner() == currentPlayer.getName()))) {
						connected = true;
						if ((location.Col - 1 >= 0 && board.pieces[location.Row][location.Col - 1].getOwner() == currentOpponent.getName())
							|| (location.Col + 1 <= 10 && board.pieces[location.Row][location.Col + 1].getOwner() == currentOpponent.getName())) {
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

bool State::isLegalOpening(std::string move) {
	bool result = false;

	if (((move[0] == 'N' && move[3] == 'B') || (move[0] == 'B' && move[3] == 'N')) && move.length() == 6) {
		Point nodeLocation;
		Point branchLocation;
		int id;
		std::string idString;
		idString.push_back(move[1]);
		idString.push_back(move[2]);
		id = stoi(idString);

		if (move[0] == 'N') {
			nodeLocation = Point::GetNodeCoordinate(id);
			idString.push_back(move[4]);
			idString.push_back(move[5]);
			id = stoi(idString);
			branchLocation = Point::GetBranchCoordinate(id);

		}
		else {
			branchLocation = Point::GetBranchCoordinate(id);
			idString.push_back(move[4]);
			idString.push_back(move[5]);
			id = stoi(idString);
			nodeLocation = Point::GetNodeCoordinate(id);
		}

		if (board.pieces[nodeLocation.Row][nodeLocation.Col].getOwner() == Status::EMPTY
			&& board.pieces[branchLocation.Row][branchLocation.Col].getOwner() == Status::EMPTY
			&& ((nodeLocation.Row == branchLocation.Row && nodeLocation.Col - 1 == branchLocation.Col)
				|| (nodeLocation.Row - 1 == branchLocation.Row && nodeLocation.Col == branchLocation.Col)
				|| (nodeLocation.Row == branchLocation.Row && nodeLocation.Col + 1 == branchLocation.Col)
				|| (nodeLocation.Row + 1 == branchLocation.Row && nodeLocation.Col == branchLocation.Col))) {
			result = true;
		}
	}

	return result;
}

void State::identifyCapturedTiles(int row, int col) {
	int visited[13][2] = {};
	int length = 0;

	if (tileCaptured(row, col, visited, &length)) {
		for (int i = 0; i < length; i++) {
			board.pieces[visited[i][0]][visited[i][1]].setOwner(currentPlayer.getName());
		}
	}
}

bool State::tileVisited(int row, int col, int visited[13][2], int length) {
	bool result = false;

	for (int i = 0; i < length && result == false; i++) {
		if (row == visited[i][0] && col == visited[i][1]) {
			result = true;
		}
	}

	return result;
}

bool State::tileCaptured(int row, int col, int visited[13][2], int* length) {
	bool result = false;
	bool side1 = false;
	bool side2 = false;
	bool side3 = false;
	bool side4 = false;

	if (row - 1 >= 0 && board.pieces[row - 1][col].getType() == PieceType::BRANCH) {
		if (board.pieces[row - 1][col].getOwner() == Status::EMPTY && row - 2 >= 0 && board.pieces[row - 2][col].getType() == PieceType::TILE) {
			if (tileVisited(row - 2, col, visited, *length)) {
				side1 = true;
			}
			else {
				visited[*length][0] = row;
				visited[*length][1] = col;
				++* length;
				side1 = tileCaptured(row - 2, col, visited, length);
			}
		}
		else if (board.pieces[row - 1][col].getOwner() == currentPlayer.getName()) {
			visited[*length][0] = row;
			visited[*length][1] = col;
			++*length;
			side1 = true;
		}
	}

	if (side1 && col + 1 <= 10 && board.pieces[row][col + 1].getType() == PieceType::BRANCH) {
		if (board.pieces[row][col + 1].getOwner() == Status::EMPTY && col + 2 <= 10 && board.pieces[row][col + 2].getType() == PieceType::TILE) {
			if (tileVisited(row, col + 2, visited, *length)) {
				side2 = true;
			}
			else {
				side2 = tileCaptured(row, col + 2, visited, length);
			}
		}
		else if (board.pieces[row][col + 1].getOwner() == currentPlayer.getName()) {
			side2 = true;
		}
	}

	if (side1 && side2 && row + 1 <= 10 && board.pieces[row + 1][col].getType() == PieceType::BRANCH) {
		if (board.pieces[row + 1][col].getOwner() == Status::EMPTY && row + 2 <= 10 && board.pieces[row + 2][col].getType() == PieceType::TILE) {
			if (tileVisited(row + 2, col, visited, *length)) {
				side3 = true;
			}
			else {
				side3 = tileCaptured(row + 2, col, visited, length);
			}
		}
		else if (board.pieces[row + 1][col].getOwner() == currentPlayer.getName()) {
			side3 = true;
		}
	}

	if (side1 && side2 && side3 && col - 1 >= 0 && board.pieces[row][col - 1].getType() == PieceType::BRANCH) {
		if (board.pieces[row][col - 1].getOwner() == Status::EMPTY && col - 2 >= 0 && board.pieces[row][col - 2].getType() == PieceType::TILE) {
			if (tileVisited(row, col - 2, visited, *length)) {
				side4 = true;
			}
			else {
				side4 = tileCaptured(row, col - 2, visited, length);
			}
		}
		else if (board.pieces[row][col - 1].getOwner() == currentPlayer.getName()) {
			side4 = true;
		}
	}

	if (side1 && side2 && side3 && side4) {
		result = true;
	}

	return result;
}

bool State::nodeBought(std::string move, Point* coordinates) {
	bool confirmed = false;
	if (coordinates->Row != 12) {
		if (currentPlayer.getGreenResources() >= 2 && currentPlayer.getYellowResources() >= 2 && board.pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
			currentPlayer.decreaseGreenResources(2);
			currentPlayer.decreaseYellowResources(2);

			confirmed = true;
		}
	}

	return confirmed;
}

void State::buildNode(Point* coordinates) {
	int nodes;
	if (board.pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
		board.pieces[coordinates->Row][coordinates->Col].setOwner(currentPlayer.getName());

		currentPlayer.incrementNodes();

		if (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board.tiles[coordinates->Row - 1][coordinates->Col - 1].getColor() != Color::BLANK) {
			nodes = board.connectingNodes(coordinates->Row - 1, coordinates->Col - 1) + 1;
			if (nodes > board.tiles[coordinates->Row - 1][coordinates->Col - 1].getDots()
				&& board.pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() != currentPlayer.getName()) {
				board.pieces[coordinates->Row - 1][coordinates->Col - 1].setOwner(Status::INVALID);
			}
		}

		if (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board.tiles[coordinates->Row - 1][coordinates->Col + 1].getColor() != Color::BLANK) {
			nodes = board.connectingNodes(coordinates->Row - 1, coordinates->Col + 1) + 1;
			if (nodes > board.tiles[coordinates->Row - 1][coordinates->Col + 1].getDots()
				&& board.pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() != currentPlayer.getName()) {
				board.pieces[coordinates->Row - 1][coordinates->Col + 1].setOwner(Status::INVALID);
			}
		}

		if (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board.tiles[coordinates->Row + 1][coordinates->Col - 1].getColor() != Color::BLANK) {
			nodes = board.connectingNodes(coordinates->Row + 1, coordinates->Col - 1) + 1;
			if (nodes > board.tiles[coordinates->Row + 1][coordinates->Col - 1].getDots()
				&& board.pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() != currentPlayer.getName()) {
				board.pieces[coordinates->Row + 1][coordinates->Col - 1].setOwner(Status::INVALID);
			}
		}

		if (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board.tiles[coordinates->Row + 1][coordinates->Col + 1].getColor() != Color::BLANK) {
			nodes = board.connectingNodes(coordinates->Row + 1, coordinates->Col + 1) + 1;
			if (nodes > board.tiles[coordinates->Row + 1][coordinates->Col + 1].getDots()
				&& board.pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() != currentPlayer.getName()) {
				board.pieces[coordinates->Row + 1][coordinates->Col + 1].setOwner(Status::INVALID);
			}
		}
	}
}

bool State::branchBought(std::string move, Point* coordinates) {
	bool confirmed = false;
	if (coordinates->Row != 12) {
		if (currentPlayer.getRedResources() >= 1 && currentPlayer.getBlueResources() >= 1 && board.pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
			currentPlayer.decreaseRedResources(1);
			currentPlayer.decreaseBlueResources(1);

			confirmed = true;
		}
	}

	return confirmed;
}

void State::buildBranch(Point* coordinates) {
	//fix captured tile identification
	if (board.pieces[coordinates->Row][coordinates->Col].getOwner() == Status::EMPTY) {
		board.pieces[coordinates->Row][coordinates->Col].setOwner(currentPlayer.getName());

		if (currentPlayer.getNetworks() == 2) {
			bool net1 = false;
			bool net2 = false;

			if (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer.getName()) {
				if (board.pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == Network::NET1) {
					net1 = true;
				}
				else if (board.pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == Network::NET2) {
					net2 = true;
				}
			}

			if (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer.getName()) {
				if (board.pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == Network::NET1) {
					net1 = true;
				}
				else if (board.pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == Network::NET2) {
					net2 = true;
				}
			}

			if (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer.getName()) {
				if (board.pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == Network::NET1) {
					net1 = true;
				}
				else if (board.pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == Network::NET2) {
					net2 = true;
				}
			}

			if (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer.getName()) {
				if (board.pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == Network::NET1) {
					net1 = true;
				}
				else if (board.pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == Network::NET2) {
					net2 = true;
				}
			}

			if (coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10) {
				if (coordinates->Col - 2 >= 0 && board.pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer.getName()) {
					if (board.pieces[coordinates->Row][coordinates->Col - 2].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board.pieces[coordinates->Row][coordinates->Col - 2].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Col + 2 <= 10 && board.pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer.getName()) {
					if (board.pieces[coordinates->Row][coordinates->Col + 2].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board.pieces[coordinates->Row][coordinates->Col + 2].getNet() == Network::NET2) {
						net2 = true;
					}
				}
			}
			else {
				if (coordinates->Row - 2 >= 0 && board.pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer.getName()) {
					if (board.pieces[coordinates->Row - 2][coordinates->Col].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board.pieces[coordinates->Row - 2][coordinates->Col].getNet() == Network::NET2) {
						net2 = true;
					}
				}

				if (coordinates->Row + 2 <= 10 && board.pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer.getName()) {
					if (board.pieces[coordinates->Row + 2][coordinates->Col].getNet() == Network::NET1) {
						net1 = true;
					}
					else if (board.pieces[coordinates->Row + 2][coordinates->Col].getNet() == Network::NET2) {
						net2 = true;
					}
				}
			}

			if (net1 == true && net2 == true) {
				currentPlayer.mergeNetworks();
				currentPlayer.incrementBranches1();
				board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
			}
			else if (net1 == true) {
				currentPlayer.incrementBranches1();
				currentPlayer.setLongest();
				board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
			}
			else if (net2 == true) {
				currentPlayer.incrementBranches2();
				currentPlayer.setLongest();
				board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET2);
			}
		}
		else if (currentPlayer.getNetworks() == 1) {
			bool connected = false;
			if ((coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer.getName())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer.getName())
				|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer.getName())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer.getName())) {
				if ((coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10)
					&& ((coordinates->Col - 2 >= 0 && board.pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer.getName())
						|| (coordinates->Col + 2 <= 10 && board.pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer.getName()))) {
					connected = true;
				}
				else if ((coordinates->Row - 2 >= 0 && board.pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer.getName())
					|| (coordinates->Row + 2 <= 10 && board.pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer.getName())) {
					connected = true;
				}
			}

			if (connected == true) {
				currentPlayer.incrementBranches1();
				board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
			}
			else {
				currentPlayer.incrementBranches2();
				currentPlayer.setNetworks(2);
				currentPlayer.setLongest();
				board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET2);
			}
		}
		else {
			currentPlayer.setNetworks(1);
			currentPlayer.incrementBranches1();
			board.pieces[coordinates->Row][coordinates->Col].setNet(Network::NET1);
		}

		//if each end touches the same network as the placed branch (which if there were two different networks touching this branch anyway then they should have been merged earlier)
		if ((coordinates->Row == 0 || coordinates->Row == 2 || coordinates->Row == 4 || coordinates->Row == 6 || coordinates->Row == 8 || coordinates->Row == 10)
			&& ((coordinates->Col - 2 >= 0 && board.pieces[coordinates->Row][coordinates->Col - 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col - 2].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row][coordinates->Col - 2].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet()))
			&& ((coordinates->Col + 2 <= 10 && board.pieces[coordinates->Row][coordinates->Col + 2].getType() == PieceType::BRANCH && board.pieces[coordinates->Row][coordinates->Col + 2].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row][coordinates->Col + 2].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet()))) {
			if (coordinates->Row - 1 >= 0 && board.pieces[coordinates->Row - 1][coordinates->Col].getType() == PieceType::TILE) {
				identifyCapturedTiles(coordinates->Row - 1, coordinates->Col);
			}

			if (coordinates->Row + 1 <= 10 && board.pieces[coordinates->Row + 1][coordinates->Col].getType() == PieceType::TILE) {
				identifyCapturedTiles(coordinates->Row + 1, coordinates->Col);
			}
		}
		else if (((coordinates->Row - 2 >= 0 && board.pieces[coordinates->Row - 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 2][coordinates->Col].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row - 2][coordinates->Col].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row - 1 >= 0 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row - 1][coordinates->Col - 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row - 1 >= 0 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row - 1][coordinates->Col + 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet()))
			&& ((coordinates->Row + 2 <= 10 && board.pieces[coordinates->Row + 2][coordinates->Col].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 2][coordinates->Col].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row + 2][coordinates->Col].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row + 1][coordinates->Col - 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet())
				|| (coordinates->Row + 1 <= 10 && coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getType() == PieceType::BRANCH && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getOwner() == currentPlayer.getName() && board.pieces[coordinates->Row + 1][coordinates->Col + 1].getNet() == board.pieces[coordinates->Row][coordinates->Col].getNet()))) {
			if (coordinates->Col - 1 >= 0 && board.pieces[coordinates->Row][coordinates->Col - 1].getType() == PieceType::TILE) {
				identifyCapturedTiles(coordinates->Row, coordinates->Col - 1);
			}
			else if (coordinates->Col + 1 <= 10 && board.pieces[coordinates->Row][coordinates->Col + 1].getType() == PieceType::TILE) {
				identifyCapturedTiles(coordinates->Row, coordinates->Col + 1);
			}
		}
	}
}

void State::updateGameBoard(std::string move, bool isOpening) {
	//updates the game board with the most recent move
	bool resourcesUpdated = false;
	std::string tempMove = "";
	std::string tempId = "";
	int id = 40;
	Point coordinates;
	bool trade = false;
	int start = 0;

	if (move[0] == '+') {
		trade = currentPlayer.tradeMade(move);
	}

	if (trade) {
		start = 5;
	}
	else {
		for (int i = start; i < move.length(); i = i + 3) {
			tempMove = "";
			tempId = "";
			tempMove.push_back(move[i]);
			tempMove.push_back(move[i + 1]);
			tempMove.push_back(move[i + 2]);
			tempId.push_back(tempMove[1]);
			tempId.push_back(tempMove[2]);
			id = stoi(tempId);

			if (move[i] = 'N') {
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
			else if (move[i] = 'B') {
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

}