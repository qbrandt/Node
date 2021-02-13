#include "pch.h"
#include "Piece.h"

Piece::Piece() {
	owner = Status::INVALID;
	id = 40;
	type = PieceType::NONE;
}

void Piece::setOwner(Status owner) {
	this->owner = owner;
}

void Piece::setId(int id) {
	this->id = id;
}

void Piece::setType(PieceType type) {
	this->type = type;
}

Status Piece::getOwner() {
	return owner;
}

int Piece::getId() {
	return id;
}

PieceType Piece::getType() {
	return type;
}