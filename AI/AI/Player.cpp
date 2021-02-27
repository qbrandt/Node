#include "pch.h"
#include "Player.h"

Player::Player() {
	name = Status::EMPTY;
	redResources = 0;
	blueResources = 0;
	yellowResources = 0;
	greenResources = 0;
	nodes = 0;
	branches1 = 0;
	branches2 = 0;
	tiles = 0;
	networks = 0;
	longest = Network::NEITHER;
}

Player::Player(Player& player)
{
	name = player.name;
	redResources = player.redResources;
	blueResources = player.blueResources;
	yellowResources = player.yellowResources;
	greenResources = player.greenResources;
	nodes = player.nodes;
	branches1 = player.branches1;
	branches2 = player.branches2;
	tiles = player.tiles;
	networks = player.networks;
	longest = player.longest;
}

void Player::resetPlayer() {
	redResources = 0;
	blueResources = 0;
	yellowResources = 0;
	greenResources = 0;
	nodes = 0;
	branches1 = 0;
	branches2 = 0;
	tiles = 0;
	networks = 0;
	longest = Network::NEITHER;
}

void Player::setName(Status name) {
	this->name = name;
}

Status Player::getName() {
	return name;
}

void Player::incrementResource(Color Color) {
	if (Color == Color::RED) {
		increaseRedResources(1);
	}
	else if (Color == Color::BLUE) {
		increaseBlueResources(1);
	}
	else if (Color == Color::YELLOW) {
		increaseYellowResources(1);
	}
	else if (Color == Color::GREEN) {
		increaseGreenResources(1);
	}
}

void Player::increaseRedResources(int x) {
	redResources = redResources + x;
}

void Player::decreaseRedResources(int x) {
	redResources = redResources - x;
}

int Player::getRedResources() {
	return redResources;
}

void Player::increaseBlueResources(int x) {
	blueResources = blueResources + x;
}

void Player::decreaseBlueResources(int x) {
	blueResources = blueResources - x;
}

int Player::getBlueResources() {
	return blueResources;
}

void Player::increaseYellowResources(int x) {
	yellowResources = yellowResources + x;
}

void Player::decreaseYellowResources(int x) {
	yellowResources = yellowResources - x;
}

int Player::getYellowResources() {
	return yellowResources;
}

void Player::increaseGreenResources(int x) {
	greenResources = greenResources + x;
}

void Player::decreaseGreenResources(int x) {
	greenResources = greenResources - x;
}

int Player::getGreenResources() {
	return greenResources;
}

void Player::incrementTiles() {
	tiles++;
}

int Player::getTiles() {
	return tiles;
}

void Player::incrementNodes() {
	nodes++;
}

int Player::getNodes() {
	return nodes;
}

void Player::incrementBranches1() {
	branches1++;
}

int Player::getBranches1() {
	return branches1;
}

void Player::incrementBranches2() {
	branches2++;
}

int Player::getBranches2() {
	return branches2;
}

void Player::setNetworks(int nets) {
	networks = nets;
}

int Player::getNetworks() {
	return networks;
}

void Player::mergeNetworks() {
	branches1 = branches1 + branches2;
	networks = 1;
	longest = Network::NET1;
}

void Player::setLongest() {
	if (branches1 > branches2) {
		longest = Network::NET1;
	}
	else if (branches2 > branches1) {
		longest = Network::NET2;
	}
	else {
		longest = Network::NET1;
	}
}

Network Player::getLongest() {
	return longest;
}

bool Player::isLegalTrade(std::string move) {
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

	if (redRequested <= redResources && blueRequested <= blueResources && greenRequested <= greenResources && yellowRequested <= yellowResources) {
		result = true;
	}
	return result;
}

bool Player::tradeMade(std::string move) {
	bool confirmed = false;
	int redTraded = 0;
	int blueTraded = 0;
	int yellowTraded = 0;
	int greenTraded = 0;
	Color colorGained = Color::BLANK;

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
			colorGained = Color::RED;
		}
		else if (move[4] == 'B') {
			colorGained = Color::BLUE;
		}
		else if (move[4] == 'Y') {
			colorGained = Color::YELLOW;
		}
		else if (move[4] == 'G') {
			colorGained = Color::GREEN;
		}

		if (redTraded <= redResources && blueTraded <= blueResources && yellowTraded <= yellowResources && greenTraded <= greenResources) {
			decreaseRedResources(redTraded);
			decreaseBlueResources(blueTraded);
			decreaseYellowResources(yellowTraded);
			decreaseGreenResources(greenTraded);
			incrementResource(colorGained);
			confirmed = true;
		}
	}

	return confirmed;
}