#pragma once
#include<vector>
#include "Board.h"
#include "Player.h"

using std::vector;

class State
{
public:
	State();
	State(const State& state);
	~State();

	bool won();
	void addResources();
	bool isLegal(std::string move);
	bool isLegalOpening(std::string move);
	void identifyCapturedTiles(int row, int col);
	bool tileVisited(int row, int col, int visited[13][2], int length);
	bool tileCaptured(int row, int col, int visited[13][2], int* length);
	bool nodeBought(std::string move, Point* coordinates);
	void buildNode(Point* coordinates);
	bool branchBought(std::string move,  Point* coordinates);
	void buildBranch(Point* coordinates);
	void updateGameBoard(std::string move, bool isOpening);
	void swapPlayerAndOpponent();
	void setBoard(std::string board);
	std::string getRandomMove();
	std::string getRandomOpeningMove();
	string GetState();
	vector<State> GenerateAllStartResources();
	vector<State> GenerateAllOpeningMoves(bool firstMoveOfPlayer);
private:
	Board* board;
	Player* currentPlayer;
	Player* currentOpponent;

};

