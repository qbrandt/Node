#pragma once
#include "Status.h"
#include "PieceType.h"
class Piece
{
public:
	Piece();

	void setOwner(Status owner);
	Status getOwner();
	
	void setId(int id);
	int getId();

	void setType(PieceType type);
	PieceType getType();

private:
	Status owner;
	int id;
	PieceType type;
};

