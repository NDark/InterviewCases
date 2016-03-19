// DynamicCast_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

class Parent
{
public:
	void virtual Print()
	{
		std::cout << "Parent" << std::endl;
	}
};


class Child : public Parent
{
public:
	void virtual Print()
	{
		std::cout << "Child" << std::endl;
	}
};


int main()
{
	
	Parent* pParent = new Parent();

	pParent->Print();

	Child* pChild = dynamic_cast<Child*> ( pParent );  

	pChild->Print();


	int finishInteger;
	std::cin >> finishInteger;
	return 0;
}

