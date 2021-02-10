#pragma once
#include<string>

using std::string;

class AI
{
public:
	AI();
	void GetBoard(string board);
	string RandomMove(string move);
	string SmartMove(string move);
};