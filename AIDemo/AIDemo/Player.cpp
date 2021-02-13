#include "Player.h"

Player::Player() {
	name = none;
	redResources = 0;
	blueResources = 0;
	yellowResources = 0;
	greenResources = 0;
	nodes = 0;
	branches = 0;
	tiles = 0;
}

void Player::changePlayerName(playerName p) {
	name = p;
}

playerName Player::getPlayerName() {
	return name;
}

void Player::increaseRedResources(int x) {
	redResources = redResources + x;
}

void Player::decreaseRedResources(int x) {
	redResources = redResources - x;
}

int Player::getRedResourceNumber() {
	return redResources;
}

void Player::increaseBlueResources(int x) {
	blueResources = blueResources + x;
}

void Player::decreaseBlueResources(int x) {
	blueResources = blueResources - x;
}

int Player::getBlueResourceNumber() {
	return blueResources;
}

void Player::increaseYellowResources(int x) {
	yellowResources = yellowResources + x;
}

void Player::decreaseYellowResources(int x) {
	yellowResources = yellowResources - x;
}

int Player::getYellowResourceNumber() {
	return yellowResources;
}

void Player::increaseGreenResources(int x) {
	greenResources = greenResources + x;
}

void Player::decreaseGreenResources(int x) {
	greenResources = greenResources - x;
}

int Player::getGreenResourceNumber() {
	return greenResources;
}

void Player::incrementNodeNumber() {
	nodes++;
}

int Player::getNodeNumber() {
	return nodes;
}

void Player::incrementBranchNumber() {
	branches++;
}

int Player::getBranchNumber() {
	return branches;
}

void Player::incrementTileNumber() {
	tiles++;
}

int Player::getTileNumber() {
	return tiles;
}

void Player::incrementResource(color c) {
	if (c == red) {
		increaseRedResources(1);
	}
	else if (c == blue) {
		increaseBlueResources(1);
	}
	else if (c == yellow) {
		increaseYellowResources(1);
	}
	else if (c == green) {
		increaseGreenResources(1);
	}
}