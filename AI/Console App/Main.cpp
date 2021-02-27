#include<iostream>
#include<string>
#include"AI_Console.h"

using namespace std;

int main()
{
	AI_Console myAI;
	AI_Console theirAI;
	cout << "AI Created" << endl;
	string board = "XXG3Y1G1B2R2G2Y2B3Y3B1R1R3";
	//cout << "Enter board:" << endl;
	//cin >> board;
	for (int i = 0; i < 3; i++)
	{
		cout << endl;
		cout << "-------------------------------------" << endl;
		cout << endl;
		myAI.GameSetup(board, true, false);
		theirAI.GameSetup(board, false, false);
		string move = "X00";
		for (int j = 0; j < 20; j++)
		{
			move = myAI.GetMove(move);
			cout << "Player 1: " << move << endl;
			cout << myAI.GetAI() << endl;
		
			move = theirAI.GetMove(move);
			cout << "Player 2: " << move << endl;
			cout << theirAI.GetAI() << endl;
		}
		/*string move;
		char playFirst;
		cout << endl;
		cout << "-------------------------------------" << endl;
		cout << endl;
		cin >> playFirst;
		myAI.GameSetup(board, playFirst == 'T', false);
		cin >> move;
		bool win = false;
		bool loss = false;
		while (move != "Q" && !win && !loss)
		{
			cout << "Person: " << move << endl;
			cout << "AI: " << myAI.GetMove(move) << endl;
			cout << endl;
			cout << myAI.GetAI();
			cout << endl;
			win = myAI.winner();
			loss = myAI.loser();
			if (win){
				cout << "AI won" << endl;
			}
			else if (loss) {
				cout << "AI lost" << endl;
			}
			cin >> move;

		}*/
	}
	

	return 0;
}