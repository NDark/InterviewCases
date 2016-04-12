// Condition_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

int GetCurrentPlayerID()
{
	return rand() % 50;
}

int main()
{

	int notPlayerTimes = 0;

	for (int i = 0; i < 100; ++i)
	{
		if ( i = GetCurrentPlayerID() )
		{
			std::cout << i << " skip" << std::endl;
		}
		else
		{
			++notPlayerTimes;
		}
		
	}

	std::cout << notPlayerTimes;

	int finishInteger;
	std::cin >> finishInteger;
	return 0;
}

