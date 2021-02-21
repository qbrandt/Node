#pragma once
#include"Color.h"

class Tile
{
public:
	Tile();

	void setColor(Color color);
	Color getColor();

	void setDots(char dots);
	int getDots();

private:
	Color color;
	int Dots;
};