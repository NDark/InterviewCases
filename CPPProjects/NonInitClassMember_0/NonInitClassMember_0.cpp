// NonInitClassMember_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

class Character
{
public:

	void Print();

private :
	float m_X;
	float m_Y;
};


void Character :: Print()
{
	std::cout << m_X << " " << m_Y << std::endl ;
}

int main()
{
	Character obj;
	obj.Print();

	int finishInteger;
	std::cin >> finishInteger;
    return 0;
}

