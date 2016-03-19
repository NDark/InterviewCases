// Scope_1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <time.h>

int main()
{
	time_t timeObj = time(0) ;
	struct tm * timeNow = localtime(&timeObj);

	int sum = 0;

	for (int i = 0; i < 100; ++i)
	{
		if (0 == i % 2)
		{
			if ( rand() % 10 > 3 )
			{
				if (timeNow->tm_wday != 0 )
				{
					std::cout << i << " continue" << std::endl;
					continue;
				}
			}
		}
		++sum;
	}

	std::cout << sum;

	int finishInteger;
	std::cin >> finishInteger;
	return 0;
}

