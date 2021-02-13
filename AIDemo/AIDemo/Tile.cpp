#include "Tile.h"

Tile::Tile() {
	tileColor = outOfRange;
	possibleNodes = 0;
	currentNodes = 0;
	owner = none;
	tileID = 40;
}

void Tile::changeTileColor(char colorChar) {
	switch (colorChar) {
	case 'X':
		tileColor = grey;
		break;
	case 'R':
		tileColor = red;
		break;
	case 'B':
		tileColor = blue;
		break;
	case 'Y':
		tileColor = yellow;
		break;
	case 'G':
		tileColor = green;
		break;
	default:
		tileColor = grey;
	}
}

void Tile::changePossibleNodes(char nodeNumberChar) {
	switch (nodeNumberChar) {
	case 'X':
		possibleNodes = 0;
		break;
	case '1':
		possibleNodes = 1;
		break;
	case '2':
		possibleNodes = 2;
		break;
	case '3':
		possibleNodes = 3;
		break;
	default:
		possibleNodes = 0;
		break;
	}
}

void Tile::incrementCurrentNodes() {
	if (currentNodes < possibleNodes) {
		currentNodes++;
	}
}

void Tile::changeOwner(playerName p) {
	if (owner == none) {
		owner = p;
	}
}

color Tile::getTileColor() {
	return tileColor;
}

int Tile::getPossibleNodeNumber() {
	return possibleNodes;
}

int Tile::getCurrentNodeNumber() {
	return currentNodes;
}

playerName Tile::getTileOwner() {
	return owner;
}

void Tile::changeTileID(int ID) {
	tileID == ID;
}

int Tile::getTileID() {
	return tileID;
}