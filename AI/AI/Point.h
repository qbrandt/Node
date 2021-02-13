#pragma once
class Point
{
public:
	int Row;
	int Col;
	
	Point();

	static Point TileNumberToPoint(int number);
	static int PointToTileNumber(Point point);
};