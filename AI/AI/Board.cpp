#include "pch.h"
#include "Board.h"

int Board::connectingNodes(int row, int col) {
	int result;

	if (row - 1 >= 0 && col - 1 >= 0 && (pieces[row - 1][col - 1].getOwner() == Status::PLAYER1 || pieces[row - 1][col - 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row - 1 >= 0 && col + 1 <= 10 && (pieces[row - 1][col + 1].getOwner() == Status::PLAYER1 || pieces[row - 1][col + 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row + 1 <= 10 && col - 1 >= 0 && (pieces[row + 1][col - 1].getOwner() == Status::PLAYER1 || pieces[row + 1][col - 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	if (row + 1 <= 10 && col + 1 <= 10 && (pieces[row + 1][col + 1].getOwner() == Status::PLAYER1 || pieces[row + 1][col + 1].getOwner() == Status::PLAYER2)) {
		result++;
	}

	return result;
}