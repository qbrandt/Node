#include "pch.h"
#include "Piece.h"

Piece::Piece() {
	owner = Status::EMPTY;
	id = 40;
	type = PieceType::NONE;
	net = Network::NEITHER;
}

void Piece::setOwner(Status owner) {
	this->owner = owner;
}

Status Piece::getOwner() {
	return owner;
}

void Piece::setId(int id) {
	this->id = id;
}

int Piece::getId() {
	return id;
}

void Piece::setType(PieceType type) {
	this->type = type;
}

PieceType Piece::getType() {
	return type;
}

void Piece::setNet(Network net) {
	this->net = net;
}

Network Piece::getNet() {
	return net;
}