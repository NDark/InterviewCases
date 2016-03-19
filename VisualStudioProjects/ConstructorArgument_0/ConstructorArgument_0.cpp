// ConstructorArgument_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

class V2Data
{
public:

	V2Data(float X, float Y);
	void Print();

private :

	float X;
	float Y;

};


V2Data :: V2Data(float X, float Y)
{
	X = X;
	Y = Y;
}

void V2Data :: Print()
{
	std::cout << X << " " << Y << std::endl;
}

int main()
{
	V2Data obj(1.0f, 2.0f);
	obj.Print();

	int finishInteger;
	std::cin >> finishInteger;
    return 0;
}

