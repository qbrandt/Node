#include "pch.h"
#include "State.h"

void State::addResources(Status currentPlayer) {
	Status opponent = Status::EMPTY;
	Player* player;
	if (currentPlayer == Status::PLAYER1) {
		player = &player1;
		opponent == Status::PLAYER2;
	}
	else {
		player = &player2;
		opponent == Status::PLAYER1;
	}

	for (int i = 0; i < 11; i++) {
		for (int j = 0; j < 11; j++) {
			if (board.pieces[i][j].getType() == PieceType::NODE && board.pieces[i][j].getOwner() == currentPlayer) {
				if (i - 1 >= 0 && j - 1 >= 0 && board.tiles[i - 1][j - 1].getColor() != Color::BLANK && board.pieces[i - 1][j - 1].getOwner() != Status::INVALID && board.pieces[i - 1][j - 1].getOwner() != opponent) {
					player->incrementResource(board.tiles[i - 1][j - 1].getColor());
				}

				if (i - 1 >= 0 && j + 1 <= 10 && board.tiles[i - 1][j + 1].getColor() != Color::BLANK && board.pieces[i - 1][j + 1].getOwner() != Status::INVALID && board.pieces[i - 1][j + 1].getOwner() != opponent) {
					player->incrementResource(board.tiles[i - 1][j + 1].getColor());
				}

				if (i + 1 <= 10 && j - 1 >= 0 && board.tiles[i + 1][j - 1].getColor() != Color::BLANK && board.pieces[i + 1][j - 1].getOwner() != Status::INVALID && board.pieces[i + 1][j - 1].getOwner() != opponent) {
					player->incrementResource(board.tiles[i + 1][j - 1].getColor());
				}

				if (i + 1 <= 10 && j + 1 <= 10 && board.tiles[i + 1][j + 1].getColor() != Color::BLANK && board.pieces[i + 1][j + 1].getOwner() != Status::INVALID && board.pieces[i + 1][j + 1].getOwner() != opponent) {
					player->incrementResource(board.tiles[i + 1][j + 1].getColor());
				}
			}
		}
	}
}

bool won() {
	//empty
	//determines whether a move results in a win
	//complete after update resources and board
}

bool State::isLegal(std::string move, Status p, Player* player) {
	bool result = true;
	if ((move[0] == '+' && player->isLegalTrade(move)) || move[0] != '+') {
		int start = 0;
		if (move[0] == '+') {
			start = 5;
		}
		bool enoughResources = false;
		bool alreadyOwned = true;
		bool connected = false;
		std::string tempString;
		int id = 0;
		Point location;
		for (int i = start; i < move.length(); i = i + 3) {
			tempString.push_back(move[i + 1]);
			tempString.push_back(move[i + 2]);
			id = stoi(tempString);
			if (move[i] == 'N' && player->getGreenResources() >= 2 && player->getYellowResources() >= 2) {
				enoughResources = true;
			}
			else if (move[i] == 'B' && player->getBlueResources() >= 1 && player->getRedResources() >= 1) {
				enoughResources = true;
			}

			if (move[i] == 'N') {
				location = Point::GetNodeCoordinate(id);
				//should work later, add function
			}
			else {
				location = Point::GetBranchCoordinate(id);
				//should work later, add function
			}

			if (board.pieces[location.Row][location.Col].getOwner() == Status::EMPTY) {
				alreadyOwned = false;
			}

			if (move[i] == 'N' && ((location.Row - 1 >= 0 && board.pieces[location.Row - 1][location.Col].getOwner() == p)
				|| (location.Row + 1 <= 10 && board.pieces[location.Row + 1][location.Col].getOwner() == p)
				|| (location.Col - 1 >= 0 && board.pieces[location.Row][location.Col - 1].getOwner() == p)
				|| (location.Col + 1 <= 10 && board.pieces[location.Row][location.Col + 1].getOwner() == p))) {
				connected = true;
			}
			else if (move[i] == 'B' && (location.Row == 0 || location.Row == 2 || location.Row == 4 || location.Row == 6 || location.Row == 8 || location.Row == 10)
				&& ((location.Col - 2 >= 0 && board.pieces[location.Row][location.Col - 2].getOwner() == p)
					|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board.pieces[location.Row - 1][location.Col - 1].getOwner() == p)
					|| (location.Row + 1 <= 10 && location.Col - 1 >= 0 && board.pieces[location.Row + 1][location.Col - 1].getOwner() == p)
					|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board.pieces[location.Row - 1][location.Col + 1].getOwner() == p)
					|| (location.Col + 2 <= 10 && board.pieces[location.Row][location.Col + 2].getOwner() == p)
					|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board.pieces[location.Row + 1][location.Col + 1].getOwner() == p))) {
				connected = true;
			}
			else if (move[i] == 'B' && (location.Row == 1 || location.Row == 3 || location.Row == 5 || location.Row == 7 || location.Row == 9)
				&& ((location.Row + 1 <= 10 && location.Col - 1 >= 0 && board.pieces[location.Row + 1][location.Col - 1].getOwner() == p)
					|| (location.Row + 2 <= 10 && board.pieces[location.Row + 2][location.Col].getOwner() == p)
					|| (location.Row + 1 <= 10 && location.Col + 1 <= 10 && board.pieces[location.Row + 1][location.Col + 1].getOwner() == p)
					|| (location.Row - 1 >= 0 && location.Col - 1 >= 0 && board.pieces[location.Row - 1][location.Col - 1].getOwner() == p)
					|| (location.Row - 2 >= 0 && board.pieces[location.Row - 2][location.Col].getOwner() == p)
					|| (location.Row - 1 >= 0 && location.Col + 1 <= 10 && board.pieces[location.Row - 1][location.Col + 1].getOwner() == p))) {
				connected = true;
			}

			if (!enoughResources || alreadyOwned || !connected) {
				result = false;
			}
		}
	}

	return result;
}

bool State::isLegalOpening(std::string move, Player* player) {
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
			branchLocation = Point::GetBranchCoordinate(Id);

		}
		else {
			branchLocation = Point::GetBranchCoordinate(Id);
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