// ForLoop_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

class Character
{
public:
	int Cost;
};
int main()
{
	Character characters[10];

	int sum = 0;

	for (int i; i < 100; ++i)
	{
		sum += characters[i].Cost;
	}

	std::cout << sum;

	int finishInteger;
	std::cin >> finishInteger;
    return 0;
}

