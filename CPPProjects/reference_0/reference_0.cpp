// reference_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <iostream>

#define ENABLE_TRIGGER_CRASH 1

class Giant
{
public:
	Giant();

	class Fur
	{
	public:
		typedef void (Giant::*Callback)();
		Callback functionPtr;
	};

	void FuncVoid1();
	void FuncVoid2();

	void GoNext(Fur&_Set);
	void MainRun();

private:
	Fur& refVar;

	static Fur staticArray[];
};

Giant::Fur Giant::staticArray[] =
{
	{ nullptr  },
	{ &Giant::FuncVoid1 },
	{ &Giant::FuncVoid2 },
};

Giant::Giant()
	: refVar(staticArray[1])
{

}

void Giant::FuncVoid1()
{
	printf("Giant :: FuncVoid1()");
}

void Giant::FuncVoid2()
{
	printf("Giant :: FuncVoid2()");
}

void Giant::MainRun()
{

#if 1 == ENABLE_TRIGGER_CRASH
	(this->*refVar.functionPtr)();
#endif

	GoNext(staticArray[2]);

	GoNext(staticArray[0]);

}


void Giant::GoNext(Fur&_Set)
{
	refVar = _Set;
}


int main()
{
	Giant* obj1 = new Giant();
	obj1->MainRun();
	delete obj1;

	printf("-----------------------------------------\n");
	printf("-----------------------------------------\n");

	Giant* obj2 = new Giant();
	obj2->MainRun();
	delete obj2;

	int finishInteger = 0;
	std::cin >> finishInteger;
    return 0;
}

