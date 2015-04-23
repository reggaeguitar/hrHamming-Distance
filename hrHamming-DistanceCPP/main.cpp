#include <cstdio>
#include <bitset>
#include <string>
#include <iostream>
#include <sstream>
#include <vector>
#include <stack>
#include <iterator>

using namespace std;

const int MAX_N = 50000;

int main()
{ // a = false, b = true
	int len;
	scanf("%d", &len);
	getchar();
	string str;
	getline(cin, str);
	bitset<MAX_N> bitArr;
	bitset<MAX_N> scratchArr;
	bitset<MAX_N> midArr;
	for (unsigned int index = 0; index < str.size(); index++)
	{
		auto ch = str[index];
		if (ch == 'b')
		{
			bitArr[index] = true;
		}
	}
	int numQueries;
	scanf("%d", &numQueries);
	getchar();
	while (numQueries-- > 0)
	{
		string line;
		getline(cin, line);
		istringstream iss(line);
		vector<string> args{ istream_iterator<string>{iss},
			istream_iterator<string>{} };

		stack<bool> s; // stack will have ((rr - lr) + 1) elements
		int l, r, l1, r1, sLen1, l2, r2, sLen2, dif, lr, rr, lrCopy, rw, lw;
		bool value;
		char outputChar;

		switch (args[0][0])
		{
		case 'C':
			l = stoi(args[1]) - 1;
			r = stoi(args[2]) - 1;
			value = args[3][0] == 'b';
			for (int i = l; i <= r; ++i)
			{
				bitArr[i] = value;
			}
			break;
		case 'S':
			l1 = stoi(args[1]) - 1;
			r1 = stoi(args[2]) - 1;
			sLen1 = (r1 - l1) + 1;
			l2 = stoi(args[3]) - 1;
			r2 = stoi(args[4]) - 1;
			sLen2 = (r2 - l2) + 1;
			dif = l2 - r1 - 1;
			// save first substring in scratchArr starting at index 0
			for (int i = 0; i < sLen1; i++)
			{
				scratchArr[i] = bitArr[l1 + i];
			}
			// save the middle part in midArr starting at index 0
			for (int i = r1 + 1; i < l2; i++)
			{
				midArr[i - r1 - 1] = bitArr[i];
			}
			// overwrite first substring with second substring
			for (int i = 0; i < sLen2; i++)
			{
				bitArr[l1 + i] = bitArr[l2 + i];
			}
			// overwrite the mid substring after the first substring
			for (int i = l1 + sLen2; i < dif + l1 + sLen2; i++)
			{
				bitArr[i] = midArr[i - l1 - sLen2];
			}
			// overwrite second substring with first substring saved in scratchArr starting at index 0
			for (int i = 0; i < sLen1; i++)
			{
				bitArr[l1 + sLen2 + i + dif] = scratchArr[i];
			}
			break;
		case 'R':
			lr = stoi(args[1]) - 1;
			rr = stoi(args[2]) - 1;
			lrCopy = lr;
			for (; lrCopy <= rr; ++lrCopy)
			{
				s.push(bitArr[lrCopy]);
			}
			for (; lr <= rr; ++lr)
			{
				bitArr[lr] = s.top();
				s.pop();
			}
			break;
		case 'W':
			lw = stoi(args[1]) - 1;
			rw = stoi(args[2]) - 1;
			for (; lw <= rw; ++lw)
			{
				outputChar = bitArr[lw] ? 'b' : 'a';
				cout << outputChar;
			}
			cout << endl;
			break;
		case 'H':
			int dist = 0;
			int oneStart = stoi(args[1]) - 1;
			int twoStart = stoi(args[2]) - 1;
			if (oneStart == twoStart)
			{
				cout << "0" << endl;
				break;
			}
			int lenH = stoi(args[3]);
			for (int i = 0; i < lenH; i++)
			{
				if (bitArr[oneStart + i] != bitArr[twoStart + i])
				{
					++dist;
				}
			}
			cout << dist << endl;
			break;
		}
	}
	return 0;
}
