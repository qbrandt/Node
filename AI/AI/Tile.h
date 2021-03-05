#pragma once
#include"Color.h"

class Tile
{
public:
	Tile();

	void setColor(Color color);
	Color getColor();

	void setDots(int dots);
	int getDots();

private:
	Color color;
	unsigned char Dots;
};