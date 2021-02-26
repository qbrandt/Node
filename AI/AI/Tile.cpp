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

void Tile::setDots(int dots) {
	Dots = dots;
}

int Tile::getDots() {
	return Dots;
}