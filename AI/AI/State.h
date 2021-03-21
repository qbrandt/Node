#pragma once
#include<vector>
#include "Board.h"
#include "Player.h"
#include "Monte Carlo.h"

using std::vector;

class State
{
public:
	State();
	State(const State& state);
	~State();

	typedef int Move;
	static const Move no_move = 0;

	void do_move(Move move);
	template<typename RandomEngine>
	void do_random_move(RandomEngine* engine);
	bool has_moves() const;
	std::vector<Move> get_moves();
	double get_result(int current_player_to_move) const;

	int player_to_move;

	bool won() const;
	bool lost() const;
	void addResources();
	void mergeNetworks();
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
	vector<State> GenerateAllOpeningMoves();
	vector<State> GenerateAllBranches(long visited);
	vector<State> GenerateAllBranches();
	vector<State> GenerateAllNodes(long visited);
	vector<State> GenerateAllNodes();
	vector<State> GenerateAllMoves();
	std::string getMoveString();
	vector<State> possibleMoves = {};
	void incrementMoveCount();

private:
	Board* board;
	Player* currentPlayer;
	Player* currentOpponent;
	std::string moveString;
	int moveCount;
	void check_invariant() const;
};

