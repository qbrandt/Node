#pragma once
#include "Status.h"
#include "PieceType.h"
#include "Network.h"
#include "Point.h"

class Piece
{
public:
	Piece();
	Piece(const Piece& oldPiece);

	void setOwner(Status owner);
	Status getOwner();
	
	void setId(int id);
	int getId();

	void setType(PieceType type);
	PieceType getType();

	void setNet(Network net);
	Network getNet();

private:
	Status owner;
	int id;
	PieceType type;
	Network net;
};

