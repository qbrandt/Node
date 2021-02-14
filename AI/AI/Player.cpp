#include "pch.h"
#include "Player.h"

Player::Player() {
	name = Status::EMPTY;
	redResources = 0;
	blueResources = 0;
	yellowResources = 0;
	greenResources = 0;
	nodes = 0;
	branches = 0;
	tiles = 0;
	networks = 0;
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

void Player::incrementBranches() {
	branches++;
}

int Player::getBranches() {
	return branches;
}

void Player::setNetworks(int nets) {
	networks = nets;
}

int Player::getNetworks() {
	return networks;
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