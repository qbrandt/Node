#pragma once
class Point
{
public:
	int Row;
	int Col;
	
	Point();
	Point(int row, int col);

	static Point GetTileCoordinate(int id);
	static Point GetNodeCoordinate(int id);
	static Point GetBranchCoordinate(int id);
private:
	static Point Tiles[];
	static Point Nodes[];
	static Point Branches[];
};