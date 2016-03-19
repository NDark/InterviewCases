// ForLoop_1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>

int main()
{
	std::vector<int> resourceVec;
	
	int loopNum = 100;
	int count = 0;

	for (; loopNum >= 0; --loopNum)
	{
		resourceVec.push_back(0);
		std::cout << ++count << std::endl ;
	}



	int finishInteger;
	std::cin >> finishInteger;

    return 0;
}

