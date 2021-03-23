#pragma once
#include<string>
#include"AI External.h"

using std::string;

class AI_Console
{
private:
	AI* AI;

public:
	AI_Console();
	~AI_Console();
	void GameSetup(string board, bool isSmart, bool goesFirst);
	string GetMove(string move);
	string GetAI();
	bool winner();
	bool loser();
};

