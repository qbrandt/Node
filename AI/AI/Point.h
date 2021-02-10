#pragma once
class Point
{
public:
	int X;
	int Y;

	Point();

	static Point TileNumberToPoint(int number);
	static int PointToTileNumber(Point point);
};