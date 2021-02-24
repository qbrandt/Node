#include<cmath>
#include<exception>

#include"Point.h"

using std::exception;

Point::Point()
{
	Col = 12;
	Row = 12;
}

Point::Point(int row, int col) : Point()
{
	Row = row;
	Col = col;
}

Point Point::GetTileCoordinate(int id)
{
	return Tiles[id];
}

Point Point::GetNodeCoordinate(int id)
{
	return Nodes[id];
}

Point Point::GetBranchCoordinate(int id)
{
	return Branches[id];
}

Point Point::Tiles[13] = 
{
	Point(1,5),

	Point(3,3),
	Point(3,5),
	Point(3,7),

	Point(5,1),
	Point(5,3),
	Point(5,5),
	Point(5,7),
	Point(5,9),

	Point(7,3),
	Point(7,5),
	Point(7,7),

	Point(9,5)
};

Point Point::Nodes[24] =
{
	Point(0,4),
	Point(0,6),

	Point(2,2),
	Point(2,4),
	Point(2,6),
	Point(2,8),

	Point(4,0),
	Point(4,2),
	Point(4,4),
	Point(4,6),
	Point(4,8),
	Point(4,10),

	Point(6,0),
	Point(6,2),
	Point(6,4),
	Point(6,6),
	Point(6,8),
	Point(6,10),

	Point(8,2),
	Point(8,4),
	Point(8,6),
	Point(8,8),

	Point(10,4),
	Point(10,6)
};

Point Point::Branches[36] =
{
	Point(0,5),

	Point(1,4),
	Point(1,6),

	Point(2,3),
	Point(2,5),
	Point(2,7),

	Point(3,2),
	Point(3,4),
	Point(3,6),
	Point(3,8),

	Point(4,1),
	Point(4,3),
	Point(4,5),
	Point(4,7),
	Point(4,9),

	Point(5,0),
	Point(5,2),
	Point(5,4),
	Point(5,6),
	Point(5,8),
	Point(5,10),

	Point(6,1),
	Point(6,3),
	Point(6,5),
	Point(6,7),
	Point(6,9),

	Point(7,2),
	Point(7,4),
	Point(7,6),
	Point(7,8),

	Point(8,3),
	Point(8,5),
	Point(8,7),

	Point(9,4),
	Point(9,6),

	Point(10,5)
};

