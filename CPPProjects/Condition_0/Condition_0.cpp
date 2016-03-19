// Condition_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

int GetCurrentCharacterID()
{
	return rand() % 50;
}

int main()
{

	int sum = 0;

	for (int i = 0; i < 100; ++i)
	{
		if ( i = GetCurrentCharacterID() )
		{
			std::cout << i << " continue" << std::endl;
			continue;
		}
		else
		{
			++sum;
		}
		
	}

	std::cout << sum;

	int finishInteger;
	std::cin >> finishInteger;
	return 0;
}

