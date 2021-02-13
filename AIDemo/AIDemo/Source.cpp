#include <iostream>
//header file that includes the enumerated type player
#include "Tile.h"
#include "NodeBranch.h"
#include "Player.h"
using namespace std;

void findNodeCoordinates(int coordinates[2], NodeBranch nodeBranchBoard[11][11], int id) {
	coordinates[0] = 12;
	coordinates[1] = 12;

	for (int i = 0; i < 11; i++) {
		for (int j = 0; j < 11; j++) {
			if (nodeBranchBoard[i][j].getNodeBranchType() == node && nodeBranchBoard[i][j].getNodeBranchID == id) {
				coordinates[0] = i;
				coordinates[1] = j;
			}
		}
	}
}

void findBranchCoordinates(int coordinates[2], NodeBranch nodeBranchBoard[11][11], int id) {
	coordinates[0] = 12;
	coordinates[1] = 12;

	for (int i = 0; i < 11; i++) {
		for (int j = 0; j < 11; j++) {
			if (nodeBranchBoard[i][j].getNodeBranchType() == branch && nodeBranchBoard[i][j].getNodeBranchID == id) {
				coordinates[0] = i;
				coordinates[1] = j;
			}
		}
	}
}

void addResources(Player* player, playerName p, NodeBranch nodeBranchBoard[11][11], Tile tileBoard[11][11]) {
	playerName opponent = none;
	if (p == p1) {
		opponent == p2;
	}
	else {
		opponent == p1;
	}

	for (int i = 0; i < 11; i++) {
		for (int j = 0; j < 11; j++) {
			if (nodeBranchBoard[i][j].getNodeBranchType() == node && nodeBranchBoard[i][j].getNodeBranchOwner() == p) {
				if (i - 1 >= 0 && j - 1 >= 0 && tileBoard[i - 1][j - 1].getTileColor() != outOfRange && tileBoard[i - 1][j - 1].getTileOwner() != black && tileBoard[i - 1][j - 1].getTileOwner() != opponent) {
					player->incrementResource(tileBoard[i - 1][j - 1].getTileColor());
				}

				if (i - 1 >= 0 && j + 1 <= 10 && tileBoard[i - 1][j + 1].getTileColor() != outOfRange && tileBoard[i - 1][j + 1].getTileOwner() != black && tileBoard[i - 1][j + 1].getTileOwner() != opponent) {
					player->incrementResource(tileBoard[i - 1][j + 1].getTileColor());
				}

				if (i + 1 <= 10 && j - 1 >= 0 && tileBoard[i + 1][j - 1].getTileColor() != outOfRange && tileBoard[i + 1][j - 1].getTileOwner() != black && tileBoard[i + 1][j - 1].getTileOwner() != opponent) {
					player->incrementResource(tileBoard[i + 1][j - 1].getTileColor());
				}

				if (i + 1 <= 10 && j + 1 <= 10 && tileBoard[i + 1][j + 1].getTileColor() != outOfRange && tileBoard[i + 1][j + 1].getTileOwner() != black && tileBoard[i + 1][j + 1].getTileOwner() != opponent) {
					player->incrementResource(tileBoard[i + 1][j + 1].getTileColor());
				}
			}
		}
	}
}

bool won(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], Player* player) {
	//empty
	//determines whether a move results in a win
	//complete after update resources and board
}

bool isLegalTrade(string move, Player* player) {
	bool result = false;
	int redRequested = 0;
	int blueRequested = 0;
	int greenRequested = 0;
	int yellowRequested = 0;

	for (int i = 1; i < 4; i++) {
		switch (move[i]) {
		case 'R':
			redRequested++;
			break;
		case 'B':
			blueRequested++;
			break;
		case 'G':
			greenRequested++;
			break;
		case 'Y':
			yellowRequested++;
			break;
		}
	}

	if (redRequested <= player->getRedResourceNumber() && blueRequested <= player->getBlueResourceNumber() && greenRequested <= player->getGreenResourceNumber() && yellowRequested <= player->getYellowResourceNumber()) {
		result = true;
	}
	return result;
}

bool isLegal(string move, playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11]) {
	bool result = true;
	if ((move[0] == '+' && isLegalTrade(move, player)) || move[0] != '+') {
		int start = 0;
		if (move[0] == '+') {
			start = 5;
		}
		bool enoughResources = false;
		bool alreadyOwned = true;
		bool connected = false;
		string tempString;
		int Id = 0;
		int coordinates[2];
		for (int i = start; i < move.length(); i = i + 3) {
			tempString.push_back(move[i + 1]);
			tempString.push_back(move[i + 2]);
			Id = stoi(tempString);
			if (move[i] == 'N' && player->getGreenResourceNumber() >= 2 && player->getYellowResourceNumber() >= 2) {
				enoughResources = true;
			}
			else if (move[i] == 'B' && player->getBlueResourceNumber() >= 1 && player->getRedResourceNumber() >= 1) {
				enoughResources = true;
			}

			if (move[i] == 'N') {
				findNodeCoordinates(coordinates, nodeBranchBoard, Id);
			}
			else {
				findBranchCoordinates(coordinates, nodeBranchBoard, Id);
			}

			if (coordinates[0] != 12 && nodeBranchBoard[coordinates[0]][coordinates[1]].getNodeBranchOwner == none) {
				alreadyOwned = false;
			}

			if (move[i] == 'N' && ((coordinates[0] - 1 >= 0 && nodeBranchBoard[coordinates[0] - 1][coordinates[1]].getNodeBranchOwner() == p)
				|| (coordinates[0] + 1 <= 10 && nodeBranchBoard[coordinates[0] + 1][coordinates[1]].getNodeBranchOwner() == p)
				|| (coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0]][coordinates[1] - 1].getNodeBranchOwner() == p)
				|| (coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0]][coordinates[1] + 1].getNodeBranchOwner() == p))) {
				connected = true;
			}
			else if (move[i] == 'B' && (coordinates[0] == 0 || coordinates[0] == 2 || coordinates[0] == 4 || coordinates[0] == 6 || coordinates[0] == 8 || coordinates[0] == 10)
				&& ((coordinates[1] - 2 >= 0 && nodeBranchBoard[coordinates[0]][coordinates[1] - 2].getNodeBranchOwner() == p)
					|| (coordinates[0] - 1 >= 0 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchOwner() == p)
					|| (coordinates[0] + 1 <= 10 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchOwner() == p)
					|| (coordinates[0] - 1 >= 0 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchOwner() == p)
					|| (coordinates[1] + 2 <= 10 && nodeBranchBoard[coordinates[0]][coordinates[1] + 2].getNodeBranchOwner() == p)
					|| (coordinates[0] + 1 <= 10 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchOwner() == p))) {
				connected = true;
			}
			else if (move[i] == 'B' && (coordinates[0] == 1 || coordinates[0] == 3 || coordinates[0] == 5 || coordinates[0] == 7 || coordinates[0] == 9)
				&& ((coordinates[0] + 1 <= 10 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchOwner() == p)
					|| (coordinates[0] + 2 <= 10 && nodeBranchBoard[coordinates[0] + 2][coordinates[1]].getNodeBranchOwner() == p)
					|| (coordinates[0] + 1 <= 10 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchOwner() == p)
					|| (coordinates[0] - 1 >= 0 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchOwner() == p)
					|| (coordinates[0] - 2 >= 0 && nodeBranchBoard[coordinates[0] - 2][coordinates[1]].getNodeBranchOwner() == p)
					|| (coordinates[0] - 1 >= 0 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchOwner() == p))) {
				connected = true;
			}

			if (!enoughResources || alreadyOwned || !connected) {
				result = false;
			}
		}
	}

	return result;
}

bool isLegalOpening(string move, Player* player, NodeBranch nodeBranchBoard[11][11]) {
	bool result = false;

	if (((move[0] == 'N' && move[3] == 'B') || (move[0] == 'B' && move[3] == 'N')) && move.length() == 6) {
		int nodeCoordinates[2];
		int branchCoordinates[2];
		int id;
		string idString;
		idString.push_back(move[1]);
		idString.push_back(move[2]);
		id = stoi(idString);

		if (move[0] == 'N') {
			findNodeCoordinates(nodeCoordinates, nodeBranchBoard, id);
			idString.push_back(move[4]);
			idString.push_back(move[5]);
			id = stoi(idString);
			findBranchCoordinates(branchCoordinates, nodeBranchBoard, id);
			
		}
		else {
			findBranchCoordinates(branchCoordinates, nodeBranchBoard, id);
			idString.push_back(move[4]);
			idString.push_back(move[5]);
			id = stoi(idString);
			findNodeCoordinates(nodeCoordinates, nodeBranchBoard, id);
		}

		if (nodeCoordinates[0] != 12 && branchCoordinates[0] != 12
			&& nodeBranchBoard[nodeCoordinates[0]][nodeCoordinates[1]].getNodeBranchOwner() == none
			&& nodeBranchBoard[branchCoordinates[0]][branchCoordinates[1]].getNodeBranchOwner() == none
			&& ((nodeCoordinates[0] == branchCoordinates[0] && nodeCoordinates[1] - 1 == branchCoordinates[1])
				|| (nodeCoordinates[0] - 1 == branchCoordinates[0] && nodeCoordinates[1] == branchCoordinates[1])
				|| (nodeCoordinates[0] == branchCoordinates[0] && nodeCoordinates[1] + 1 == branchCoordinates[1])
				|| (nodeCoordinates[0] + 1 == branchCoordinates[0] && nodeCoordinates[1] == branchCoordinates[1]))) {
			result = true;
		}
	}

	return result;
}

string selectMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], Player* player) {
	//empty
	//uses the monte carlo tree search and the heuristic to select a move
	//complete after monte carlo and heuristic
}

string selectOpeningMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], Player* player) {
	//empty
	//uses the heuristic to select an opening move
	//complete after monte carlo and heuristic
}

bool tradeMade(Player* player, string move) {
	bool confirmed = false;
	int redTraded = 0;
	int blueTraded = 0;
	int yellowTraded = 0;
	int greenTraded = 0;
	color colorGained = outOfRange;

	if (move[0] == '+') {
		if (move[1] == 'R') {
			redTraded++;
		}
		else if (move[1] == 'B') {
			blueTraded++;
		}
		else if (move[1] == 'Y') {
			yellowTraded++;
		}
		else if (move[1] == 'G') {
			greenTraded++;
		}

		if (move[2] == 'R') {
			redTraded++;
		}
		else if (move[2] == 'B') {
			blueTraded++;
		}
		else if (move[2] == 'Y') {
			yellowTraded++;
		}
		else if (move[2] == 'G') {
			greenTraded++;
		}

		if (move[3] == 'R') {
			redTraded++;
		}
		else if (move[3] == 'B') {
			blueTraded++;
		}
		else if (move[3] == 'Y') {
			yellowTraded++;
		}
		else if (move[3] == 'G') {
			greenTraded++;
		}

		if (move[4] == 'R') {
			colorGained = red;
		}
		else if (move[4] == 'B') {
			colorGained = blue;
		}
		else if (move[4] == 'Y') {
			colorGained = yellow;
		}
		else if (move[4] == 'G') {
			colorGained = green;
		}

		if (redTraded <= player->getRedResourceNumber() && blueTraded <= player->getBlueResourceNumber() && yellowTraded <= player->getYellowResourceNumber() && greenTraded <= player->getGreenResourceNumber()) {
			player->decreaseRedResources(redTraded);
			player->decreaseBlueResources(blueTraded);
			player->decreaseYellowResources(yellowTraded);
			player->decreaseGreenResources(greenTraded);
			player->incrementResource(colorGained);
			confirmed = true;
		}
	}

	return confirmed;
}

bool nodeBought(string move, playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], int coordinates[2]) {
	bool confirmed = false;
	if (coordinates[0] != 12) {
		if (player->getGreenResourceNumber() >= 2 && player->getYellowResourceNumber() >= 2 && nodeBranchBoard[coordinates[0]][coordinates[1]].getNodeBranchOwner() == none) {
			player->decreaseGreenResources(2);
			player->decreaseYellowResources(2);

			confirmed = true;
		}
	}

	return confirmed;
}

void buildNode(playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], int coordinates[2]) {
	if (nodeBranchBoard[coordinates[0]][coordinates[1]].getNodeBranchOwner == none) {
		nodeBranchBoard[coordinates[0]][coordinates[1]].changeNodeBranchOwner(p);

		player->incrementNodeNumber();

		if (coordinates[0] - 1 >= 0 && coordinates[1] - 1 >= 0 && tileBoard[coordinates[0] - 1][coordinates[1] - 1].getTileColor() != outOfRange) {
			tileBoard[coordinates[0] - 1][coordinates[1] - 1].incrementCurrentNodes();
			if (tileBoard[coordinates[0] - 1][coordinates[1] - 1].getCurrentNodeNumber() > tileBoard[coordinates[0] - 1][coordinates[1] - 1].getPossibleNodeNumber()
				&& tileBoard[coordinates[0] - 1][coordinates[1] - 1].getTileOwner() != p) {
				tileBoard[coordinates[0] - 1][coordinates[1] - 1].changeOwner(black);
			}
		}

		if (coordinates[0] - 1 >= 0 && coordinates[1] + 1 <= 10 && tileBoard[coordinates[0] - 1][coordinates[1] + 1].getTileColor() != outOfRange) {
			tileBoard[coordinates[0] - 1][coordinates[1] + 1].incrementCurrentNodes();
			if (tileBoard[coordinates[0] - 1][coordinates[1] + 1].getCurrentNodeNumber() > tileBoard[coordinates[0] - 1][coordinates[1] + 1].getPossibleNodeNumber()
				&& tileBoard[coordinates[0] - 1][coordinates[1] + 1].getTileOwner() != p) {
				tileBoard[coordinates[0] - 1][coordinates[1] + 1].changeOwner(black);
			}
		}

		if (coordinates[0] + 1 <= 10 && coordinates[1] - 1 >= 0 && tileBoard[coordinates[0] + 1][coordinates[1] - 1].getTileColor() != outOfRange) {
			tileBoard[coordinates[0] + 1][coordinates[1] - 1].incrementCurrentNodes();
			if (tileBoard[coordinates[0] + 1][coordinates[1] - 1].getCurrentNodeNumber() > tileBoard[coordinates[0] + 1][coordinates[1] - 1].getPossibleNodeNumber()
				&& tileBoard[coordinates[0] + 1][coordinates[1] - 1].getTileOwner() != p) {
				tileBoard[coordinates[0] + 1][coordinates[1] - 1].changeOwner(black);
			}
		}

		if (coordinates[0] + 1 <= 10 && coordinates[1] + 1 <= 10 && tileBoard[coordinates[0] + 1][coordinates[1] + 1].getTileColor() != outOfRange) {
			tileBoard[coordinates[0] + 1][coordinates[1] + 1].incrementCurrentNodes();
			if (tileBoard[coordinates[0] + 1][coordinates[1] + 1].getCurrentNodeNumber() > tileBoard[coordinates[0] + 1][coordinates[1] + 1].getPossibleNodeNumber()
				&& tileBoard[coordinates[0] + 1][coordinates[1] + 1].getTileOwner() != p) {
				tileBoard[coordinates[0] + 1][coordinates[1] + 1].changeOwner(black);
			}
		}
	}
}

bool branchBought(string move, playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], int coordinates[2]) {
	bool confirmed = false;
	if (coordinates[0] != 12) {
		if (player->getRedResourceNumber() >= 1 && player->getBlueResourceNumber() >= 1 && nodeBranchBoard[coordinates[0]][coordinates[1]].getNodeBranchOwner() == none) {
			player->decreaseRedResources(1);
			player->decreaseBlueResources(1);

			confirmed = true;
		}
	}

	return confirmed;
}

void buildBranch(playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], int coordinates[2]) {
	//this will only identify captured tiles if they are in a direct square from the placed branch, but it should actually identify loops in the network
	//captured tile identification should be similar to longest network identification
	if (nodeBranchBoard[coordinates[0]][coordinates[1]].getNodeBranchOwner == none) {
		nodeBranchBoard[coordinates[0]][coordinates[1]].changeNodeBranchOwner(p);

		player->incrementBranchNumber();

		if (coordinates[0] == 1 || coordinates[0] == 3 || coordinates[0] == 5 || coordinates[0] == 7 || coordinates[0] == 9) {
			if (coordinates[0] - 1 >= 0 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchOwner == p
				&& coordinates[1] - 2 >= 0 && nodeBranchBoard[coordinates[0]][coordinates[1] - 2].getNodeBranchType == branch && nodeBranchBoard[coordinates[0]][coordinates[1] - 2].getNodeBranchOwner == p
				&& coordinates[0] + 1 <= 10 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchOwner == p) {
				tileBoard[coordinates[0]][coordinates[1] - 1].changeOwner(p);
				player->incrementTileNumber();
			}

			if (coordinates[0] - 1 >= 0 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchOwner == p
				&& coordinates[1] + 2 <= 10 && nodeBranchBoard[coordinates[0]][coordinates[1] + 2].getNodeBranchType == branch && nodeBranchBoard[coordinates[0]][coordinates[1] + 2].getNodeBranchOwner == p
				&& coordinates[0] + 1 <= 10 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchOwner == p) {
				tileBoard[coordinates[0]][coordinates[1] + 1].changeOwner(p);
				player->incrementTileNumber();
			}
		}
		else if (coordinates[0] == 0 || coordinates[0] == 2 || coordinates[0] == 4 || coordinates[0] == 6 || coordinates[0] == 8 || coordinates[0] == 10) {
			if (coordinates[0] - 1 >= 0 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] - 1][coordinates[1] - 1].getNodeBranchOwner == p
				&& coordinates[0] - 2 >= 0 && nodeBranchBoard[coordinates[0] - 2][coordinates[1]].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] - 2][coordinates[1]].getNodeBranchOwner == p
				&& coordinates[0] - 1 >= 0 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] - 1][coordinates[1] + 1].getNodeBranchOwner == p) {
				tileBoard[coordinates[0] - 1][coordinates[1]].changeOwner(p);
				player->incrementTileNumber();
			}

			if (coordinates[0] + 1 <= 10 && coordinates[1] - 1 >= 0 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] + 1][coordinates[1] - 1].getNodeBranchOwner == p
				&& coordinates[0] + 2 <= 10 && nodeBranchBoard[coordinates[0] + 2][coordinates[1]].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] + 2][coordinates[1]].getNodeBranchOwner == p
				&& coordinates[0] + 1 <= 10 && coordinates[1] + 1 <= 10 && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchType == branch && nodeBranchBoard[coordinates[0] + 1][coordinates[1] + 1].getNodeBranchOwner == p) {
				tileBoard[coordinates[0] + 1][coordinates[1]].changeOwner(p);
				player->incrementTileNumber();
			}
		}
	}
}

void updateGameBoard(string move, playerName p, Player* player, Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], bool isOpening) {
	//updates the game board with the most recent move
	bool resourcesUpdated = false;
	string tempMove = "";
	string tempId = "";
	int Id = 40;
	int coordinates[2];
	bool trade = false;
	int start = 0;

	if (move[0] == '+') {
		trade = tradeMade(player, move);
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
			Id = stoi(tempId);

			if (move[i] = 'N') {
				findNodeCoordinates(coordinates, nodeBranchBoard, Id);
				if (coordinates[0] != 12) {
					if (isOpening) {
						resourcesUpdated = true;
					}
					else if (nodeBought(tempMove, p, player, tileBoard, nodeBranchBoard, coordinates)) {
						resourcesUpdated = true;
					}

					if (resourcesUpdated == true) {
						buildNode(p, player, tileBoard, nodeBranchBoard, coordinates);
					}
				}
			}
			else if (move[i] = 'B') {
				findBranchCoordinates(coordinates, nodeBranchBoard, Id);
				if (coordinates[0] != 12) {
					if (isOpening) {
						resourcesUpdated = true;
					}
					else if (branchBought(tempMove, p, player, tileBoard, nodeBranchBoard, coordinates)) {
						resourcesUpdated = true;
					}

					if (resourcesUpdated == true) {
						buildBranch(p, player, tileBoard, nodeBranchBoard, coordinates);
					}
				}
			}
		}
	}

}

void makeOpeningOpponentMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* player, string move) {
	//add a function that connects this to the game core
	//makes an opening move

	if (isLegalOpening(move, player, nodeBranchBoard)) {
		updateGameBoard(move, p, player, tileBoard, nodeBranchBoard, true);
		//check that this makes the changes necessary
	}
}

string makeOpeningPlayerMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* player) {
	//uses the AI to make an opening move
	//possibly alter to interface with the game core? more likely will add another function that acts as a door to this one
	//not sure how I'm going to interface with the game core when I need the boards to be initialized in main?
	string move = "";
	move = selectOpeningMove(tileBoard, nodeBranchBoard, player);
	updateGameBoard(move, p, player, tileBoard, nodeBranchBoard, true);

	return move;
}

void makeOpponentMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* opponent, string move) {
	//convert move entry to integrate with game core
	//enters the opponent's move and updates the game board. returns the opponent if there is a win.
	addResources(opponent, p, nodeBranchBoard, tileBoard);

	if (isLegal(move, p, opponent, tileBoard, nodeBranchBoard)) {
		updateGameBoard(move, p, opponent, tileBoard, nodeBranchBoard, false);
		//also check that this makes all necessary changes
	}
}

string makePlayerMove(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName p, Player* player) {
	//complete
	//calls the function to choose a move using the AI and returns a string containing the move
	addResources(player, p, nodeBranchBoard, tileBoard);
	string move = "";
	move = selectMove(tileBoard, nodeBranchBoard, player);
	updateGameBoard(move, p, player, tileBoard, nodeBranchBoard, false);
	//check to see that none of the other functions involved in making a move need to be called

	return move;
}

void initializeTileBoard(Tile tileBoard[11][11], string boardString) {
	//initializes the tile color and number of possible nodes at the beginning of the game
	tileBoard[1][5].changeTileColor(boardString[0]);
	tileBoard[1][5].changePossibleNodes(boardString[1]);

	tileBoard[3][3].changeTileColor(boardString[2]);
	tileBoard[3][3].changePossibleNodes(boardString[3]);

	tileBoard[3][5].changeTileColor(boardString[4]);
	tileBoard[3][5].changePossibleNodes(boardString[5]);

	tileBoard[3][7].changeTileColor(boardString[6]);
	tileBoard[3][7].changePossibleNodes(boardString[7]);

	tileBoard[5][1].changeTileColor(boardString[8]);
	tileBoard[5][1].changePossibleNodes(boardString[9]);

	tileBoard[5][3].changeTileColor(boardString[10]);
	tileBoard[5][3].changePossibleNodes(boardString[11]);

	tileBoard[5][5].changeTileColor(boardString[12]);
	tileBoard[5][5].changePossibleNodes(boardString[13]);

	tileBoard[5][7].changeTileColor(boardString[14]);
	tileBoard[5][7].changePossibleNodes(boardString[15]);

	tileBoard[5][9].changeTileColor(boardString[16]);
	tileBoard[5][9].changePossibleNodes(boardString[17]);

	tileBoard[7][3].changeTileColor(boardString[18]);
	tileBoard[7][3].changePossibleNodes(boardString[19]);

	tileBoard[7][5].changeTileColor(boardString[20]);
	tileBoard[7][5].changePossibleNodes(boardString[21]);

	tileBoard[7][7].changeTileColor(boardString[22]);
	tileBoard[7][7].changePossibleNodes(boardString[23]);

	tileBoard[9][5].changeTileColor(boardString[24]);
	tileBoard[9][5].changePossibleNodes(boardString[25]);
}

void initializeNodeBranchBoard(NodeBranch nodeBranchBoard[11][11]) {
	//initializes the type and ID of each node or branch at the beginning of the game
	nodeBranchBoard[0][4].changeNodeBranchType(node);
	nodeBranchBoard[0][4].changeNodeBranchID(0);

	nodeBranchBoard[0][5].changeNodeBranchType(branch);
	nodeBranchBoard[0][5].changeNodeBranchID(0);

	nodeBranchBoard[0][6].changeNodeBranchType(node);
	nodeBranchBoard[0][6].changeNodeBranchID(1);
	
	nodeBranchBoard[1][4].changeNodeBranchType(branch);
	nodeBranchBoard[1][4].changeNodeBranchID(1);

	nodeBranchBoard[1][6].changeNodeBranchType(branch);
	nodeBranchBoard[1][6].changeNodeBranchID(2);

	nodeBranchBoard[2][2].changeNodeBranchType(node);
	nodeBranchBoard[2][2].changeNodeBranchID(2);

	nodeBranchBoard[2][3].changeNodeBranchType(branch);
	nodeBranchBoard[2][3].changeNodeBranchID(3);

	nodeBranchBoard[2][4].changeNodeBranchType(node);
	nodeBranchBoard[2][4].changeNodeBranchID(3);

	nodeBranchBoard[2][5].changeNodeBranchType(branch);
	nodeBranchBoard[2][5].changeNodeBranchID(4);

	nodeBranchBoard[2][6].changeNodeBranchType(node);
	nodeBranchBoard[2][6].changeNodeBranchID(4);

	nodeBranchBoard[2][7].changeNodeBranchType(branch);
	nodeBranchBoard[2][7].changeNodeBranchID(5);

	nodeBranchBoard[2][8].changeNodeBranchType(node);
	nodeBranchBoard[2][8].changeNodeBranchID(5);

	nodeBranchBoard[3][2].changeNodeBranchType(branch);
	nodeBranchBoard[3][2].changeNodeBranchID(6);

	nodeBranchBoard[3][4].changeNodeBranchType(branch);
	nodeBranchBoard[3][4].changeNodeBranchID(7);

	nodeBranchBoard[3][6].changeNodeBranchType(branch);
	nodeBranchBoard[3][6].changeNodeBranchID(8);

	nodeBranchBoard[3][8].changeNodeBranchType(branch);
	nodeBranchBoard[3][8].changeNodeBranchID(9);

	nodeBranchBoard[4][0].changeNodeBranchType(node);
	nodeBranchBoard[4][0].changeNodeBranchID(6);

	nodeBranchBoard[4][1].changeNodeBranchType(branch);
	nodeBranchBoard[4][1].changeNodeBranchID(10);

	nodeBranchBoard[4][2].changeNodeBranchType(node);
	nodeBranchBoard[4][2].changeNodeBranchID(7);

	nodeBranchBoard[4][3].changeNodeBranchType(branch);
	nodeBranchBoard[4][3].changeNodeBranchID(11);

	nodeBranchBoard[4][4].changeNodeBranchType(node);
	nodeBranchBoard[4][4].changeNodeBranchID(8);

	nodeBranchBoard[4][5].changeNodeBranchType(branch);
	nodeBranchBoard[4][5].changeNodeBranchID(12);

	nodeBranchBoard[4][6].changeNodeBranchType(node);
	nodeBranchBoard[4][6].changeNodeBranchID(9);

	nodeBranchBoard[4][7].changeNodeBranchType(branch);
	nodeBranchBoard[4][7].changeNodeBranchID(13);

	nodeBranchBoard[4][8].changeNodeBranchType(node);
	nodeBranchBoard[4][8].changeNodeBranchID(10);

	nodeBranchBoard[4][9].changeNodeBranchType(branch);
	nodeBranchBoard[4][9].changeNodeBranchID(14);

	nodeBranchBoard[4][10].changeNodeBranchType(node);
	nodeBranchBoard[4][10].changeNodeBranchID(11);

	nodeBranchBoard[5][0].changeNodeBranchType(branch);
	nodeBranchBoard[5][0].changeNodeBranchID(15);

	nodeBranchBoard[5][2].changeNodeBranchType(branch);
	nodeBranchBoard[5][2].changeNodeBranchID(16);

	nodeBranchBoard[5][4].changeNodeBranchType(branch);
	nodeBranchBoard[5][4].changeNodeBranchID(17);

	nodeBranchBoard[5][6].changeNodeBranchType(branch);
	nodeBranchBoard[5][6].changeNodeBranchID(18);

	nodeBranchBoard[5][8].changeNodeBranchType(branch);
	nodeBranchBoard[5][8].changeNodeBranchID(19);

	nodeBranchBoard[5][10].changeNodeBranchType(branch);
	nodeBranchBoard[5][10].changeNodeBranchID(20);

	nodeBranchBoard[6][0].changeNodeBranchType(node);
	nodeBranchBoard[6][0].changeNodeBranchID(12);

	nodeBranchBoard[6][1].changeNodeBranchType(branch);
	nodeBranchBoard[6][1].changeNodeBranchID(21);

	nodeBranchBoard[6][2].changeNodeBranchType(node);
	nodeBranchBoard[6][2].changeNodeBranchID(13);

	nodeBranchBoard[6][3].changeNodeBranchType(branch);
	nodeBranchBoard[6][3].changeNodeBranchID(22);

	nodeBranchBoard[6][4].changeNodeBranchType(node);
	nodeBranchBoard[6][4].changeNodeBranchID(14);

	nodeBranchBoard[6][5].changeNodeBranchType(branch);
	nodeBranchBoard[6][5].changeNodeBranchID(23);

	nodeBranchBoard[6][6].changeNodeBranchType(node);
	nodeBranchBoard[6][6].changeNodeBranchID(15);

	nodeBranchBoard[6][7].changeNodeBranchType(branch);
	nodeBranchBoard[6][7].changeNodeBranchID(24);

	nodeBranchBoard[6][8].changeNodeBranchType(node);
	nodeBranchBoard[6][8].changeNodeBranchID(16);

	nodeBranchBoard[6][9].changeNodeBranchType(branch);
	nodeBranchBoard[6][9].changeNodeBranchID(25);

	nodeBranchBoard[6][10].changeNodeBranchType(node);
	nodeBranchBoard[6][10].changeNodeBranchID(17);

	nodeBranchBoard[7][2].changeNodeBranchType(branch);
	nodeBranchBoard[7][2].changeNodeBranchID(26);

	nodeBranchBoard[7][4].changeNodeBranchType(branch);
	nodeBranchBoard[7][4].changeNodeBranchID(27);

	nodeBranchBoard[7][6].changeNodeBranchType(branch);
	nodeBranchBoard[7][6].changeNodeBranchID(28);

	nodeBranchBoard[7][8].changeNodeBranchType(branch);
	nodeBranchBoard[7][8].changeNodeBranchID(29);

	nodeBranchBoard[8][2].changeNodeBranchType(node);
	nodeBranchBoard[8][2].changeNodeBranchID(18);

	nodeBranchBoard[8][3].changeNodeBranchType(branch);
	nodeBranchBoard[8][3].changeNodeBranchID(30);

	nodeBranchBoard[8][4].changeNodeBranchType(node);
	nodeBranchBoard[8][4].changeNodeBranchID(19);

	nodeBranchBoard[8][5].changeNodeBranchType(branch);
	nodeBranchBoard[8][5].changeNodeBranchID(31);

	nodeBranchBoard[8][6].changeNodeBranchType(node);
	nodeBranchBoard[8][6].changeNodeBranchID(20);

	nodeBranchBoard[8][7].changeNodeBranchType(branch);
	nodeBranchBoard[8][7].changeNodeBranchID(32);

	nodeBranchBoard[8][8].changeNodeBranchType(node);
	nodeBranchBoard[8][8].changeNodeBranchID(21);

	nodeBranchBoard[9][4].changeNodeBranchType(branch);
	nodeBranchBoard[9][4].changeNodeBranchID(33);

	nodeBranchBoard[9][6].changeNodeBranchType(branch);
	nodeBranchBoard[9][6].changeNodeBranchID(34);

	nodeBranchBoard[10][4].changeNodeBranchType(node);
	nodeBranchBoard[10][4].changeNodeBranchID(22);

	nodeBranchBoard[10][5].changeNodeBranchType(branch);
	nodeBranchBoard[10][5].changeNodeBranchID(35);

	nodeBranchBoard[10][6].changeNodeBranchType(node);
	nodeBranchBoard[10][6].changeNodeBranchID(23);
}

//alter this since the integration will need to be different
void makeFirstMoves(Tile tileBoard[11][11], NodeBranch nodeBranchBoard[11][11], playerName currentPlayer, Player* player1, Player* player2){
	//complete
	//make the opening four moves of the game in the order player 1, player 2, player 2, player 1
	//should only be used for testing the AI
	if (currentPlayer == p1) {
		makeOpeningPlayerMove(tileBoard, nodeBranchBoard, p1, player1);
		makeOpeningOpponentMove(tileBoard, nodeBranchBoard, p2, player2);
		makeOpeningOpponentMove(tileBoard, nodeBranchBoard, p2, player2);
		makeOpeningPlayerMove(tileBoard, nodeBranchBoard, p1, player1);
	}
	else {
		makeOpeningOpponentMove(tileBoard, nodeBranchBoard, p1, player1);
		makeOpeningPlayerMove(tileBoard, nodeBranchBoard, p2, player2);
		makeOpeningPlayerMove(tileBoard, nodeBranchBoard, p2, player2);
		makeOpeningOpponentMove(tileBoard, nodeBranchBoard, p1, player1);
	}
}

//this should call the other functions in the way that the game core will call them
int main(){
	string boardString = "";
	//initialize the game board
	Tile tileBoard[11][11];
	NodeBranch nodeBranchBoard[11][11];
	initializeNodeBranchBoard(nodeBranchBoard);
	Player player1;
	player1.changePlayerName(p1);
	Player player2;
	player2.changePlayerName(p2);
	playerName winner = none;
	playerName currentPlayer = none;
	int number = 0; //this should probably be deleted during integration or be used to integrate

	//this information should all come from the game core
	cout << "Enter game board" << endl;
	cin >> boardString;
	cout << "Enter player" << endl;
	cin >> number;
	if (number == 1) { currentPlayer = p1; }
	else if (number == 2) { currentPlayer = p2; }
	else { currentPlayer = p1; }
	//Change from manual input during integration
	initializeTileBoard(tileBoard, boardString);

	makeFirstMoves(tileBoard, nodeBranchBoard, currentPlayer, &player1, &player2);
	while (winner == none) {
		if (currentPlayer == p2) {
			winner = makePlayerMove(tileBoard, nodeBranchBoard, p2, &player2);
		}
		else {
			winner = makeOpponentMove(tileBoard, nodeBranchBoard, p2, &player2);
		}

		if (winner == none) {
			if (currentPlayer == p1) {
				winner == makePlayerMove(tileBoard, nodeBranchBoard, p1, &player1);
			}
			else {
				winner == makeOpponentMove(tileBoard, nodeBranchBoard, p1, &player1);
			}
		}
	}
	//somewhere in these functions I should output the AI's move

	if (winner == p1) {
		cout << "Player 1 wins" << endl;
	}
	else if (winner == p2) {
		cout << "Player 2 wins" << endl;
	}
	else {
		cout << "Game ended prematurely" << endl;
	}

	return 0;
}