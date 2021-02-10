#include "pch.h"
#include "framework.h"

#include<cmath>
#include<exception>

#include"Point.h"

using std::exception;

Point::Point()
{

}

Point Point::TileNumberToPoint(int number)
{
	Point point;
	int diff = 7 - number;
	bool isNegative = diff < 0;
	int absDiff = abs(diff);
	if (absDiff > 6)
	{
		throw new exception("Invalid tile number");
	}
	else if (absDiff == 6)
	{
		point.X = 2;
		point.Y = isNegative ? 0 : 4;
	}
	else if (absDiff > 2)
	{
		int rowPos = (absDiff - 2);
		if (isNegative)
		{
			rowPos = rowPos * -1 + 4;
		}
		point.X = rowPos;
		point.Y = isNegative ? 1 : 3;
	}
	else
	{
		point.Y = 2;
		point.X = 2 + diff;
	}
	return point;
}

int Point::PointToTileNumber(Point point)
{
	switch (point.Y)
	{
	case 0:
		return 1;
	case 1:
		return point.X + 1;
	case 2:
		return point.X + 4;
	case 3:
		return point.X + 9;
	case 4:
		return 13;
	default:
		throw new exception("Invalid coordinate");
	}
}