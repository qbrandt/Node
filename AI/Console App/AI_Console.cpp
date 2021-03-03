#include "AI_Console.h"

AI_Console::AI_Console()
{
	AI = Internal_CreateAI();
}

AI_Console::~AI_Console()
{
	Internal_DestroyAI(AI);
}

void AI_Console::GameSetup(string board, bool isSmart, bool goesFirst)
{
	Internal_AI_GameSetup(AI, board.c_str(), isSmart,goesFirst);
}

string AI_Console::GetMove(string move)
{
	return Internal_AI_GetMove(AI, move.c_str());
}

string AI_Console::GetAI()
{
	return Internal_AI_View(AI);
}
