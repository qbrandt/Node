#include "pch.h"
#include "framework.h"

#include<cmath>
#include<exception>

#include"Point.h"

using std::exception;

Point::Point()
{
	Col = 12;
	Row = 12;
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
		point.Row = 2;
		point.Col = isNegative ? 0 : 4;
	}
	else if (absDiff > 2)
	{
		int rowPos = (absDiff - 2);
		if (isNegative)
		{
			rowPos = rowPos * -1 + 4;
		}
		point.Row = rowPos;
		point.Col = isNegative ? 1 : 3;
	}
	else
	{
		point.Col = 2;
		point.Row = 2 + diff;
	}
	return point;
}

int Point::PointToTileNumber(Point point)
{
	switch (point.Col)
	{
	case 0:
		return 1;
	case 1:
		return point.Row + 1;
	case 2:
		return point.Row + 4;
	case 3:
		return point.Row + 9;
	case 4:
		return 13;
	default:
		throw new exception("Invalid coordinate");
	}
}