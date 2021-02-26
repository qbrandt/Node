#include<iostream>
#include<string>
#include"AI_Console.h"

using namespace std;

int main()
{
	AI_Console myAI;
	cout << "AI Created" << endl;
	string board = "R1Y2B2G1G3XXY3G2B1Y1B3R2R3";
	for (int i = 0; i < 3; i++)
	{
		string move;
		char playFirst;
		cout << endl;
		cout << "-------------------------------------" << endl;
		cout << endl;
		cin >> playFirst;
		myAI.GameSetup(board, playFirst == 'T', false);
		cin >> move;
		while (move != "Q")
		{
			cout << "Person: " << move << endl;
			cout << "AI: " << myAI.GetMove(move) << endl;
			cout << endl;
			cout << myAI.GetAI();
			cout << endl;
			cin >> move;
		}
	}
	

	return 0;
}