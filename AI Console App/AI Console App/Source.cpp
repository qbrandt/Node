#include<iostream>
#include<string>
#include"AI.h"

using namespace std;

int main()
{
	AI myAI;
	cout << "AI Created" << endl;
	string board = "R1Y2B2G1G3XXY3G2B1Y1B3R2R3";
	myAI.GameSetup(board, true, false);
	cout << "Game SetUp - " << board << endl;
	string move ;
	cin >> move;
	while (move != "Q")
	{
		cout << "Person: " << move << endl;
		cout << "AI: " << myAI.GetMove(move) << endl;
		cout << endl;
		myAI.PrintAI();
		cout << endl;
		cin >> move;
	}
	cout << endl;
	cout << "-------------------------------------" << endl;
	cout << endl;
	myAI.GameSetup(board, false, false);
	cin >> move;
	while (move != "Q")
	{
		cout << "Person: " << move << endl;
		cout << "AI: " << myAI.GetMove(move) << endl;
		cout << endl;
		myAI.PrintAI();
		cout << endl;
		cin >> move;
	}

	return 0;
}