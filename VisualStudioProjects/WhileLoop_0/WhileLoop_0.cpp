// WhileLoop_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <time.h>


int GetResource()
{
	int ret = ( 0 == rand() % 2 ) ? 1 : 2 ;
	return ret;
}
int main()
{
	srand( time(NULL) );

	int goal = 100;
	int resource = 0;
	while ( true )
	{
		resource += GetResource();
		std::cout << resource << std::endl ;
		if( resource == goal)
		{
			break;
		}
	}

	std::cout << resource ;

	int finishInteger;
	std::cin >> finishInteger;
    return 0;
}

