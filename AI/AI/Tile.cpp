#include "pch.h"
#include "Tile.h"

Tile::Tile() {
	color = Color::BLANK;
	Dots = 0;
}

void Tile::setColor(Color color) {
	this->color = color;
}

Color Tile::getColor() {
	return color;
}

void Tile::setDots(char dots) {
	switch (dots) {
	case 'X':
		Dots = 0;
		break;
	case '1':
		Dots = 1;
		break;
	case '2':
		Dots = 2;
		break;
	case '3':
		Dots = 3;
		break;
	default:
		Dots = 0;
		break;
	}
}

int Tile::getDots() {
	return Dots;
}