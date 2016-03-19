// Scope_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>

class Storage
{
public:
	std::vector<int> m_Storage;
};

class StorageCalculator
{
public :

	int GetSum(Storage _Src)
	{
		if (nullptr == summation)
		{
			Calculate(_Src);
		}
		std::cout << *summation << std::endl ;
		return *summation;
	}

	void Calculate(Storage _Src )
	{
		summation = new int;
		*summation = 0;
		for (int i = 0; i < _Src.m_Storage.size(); ++i)
		{
			*summation += _Src.m_Storage[i];
		}
	}

	int* summation = nullptr;
};



int main()
{


	Storage storages[100] ;
	for (int i = 0; i < 100; ++i)
	{
		storages[i].m_Storage.push_back(i);
	}

	int totalSum = 0;
	for (int i = 0; i < 100; ++i)
	{
		StorageCalculator* calculator = new StorageCalculator();
		totalSum += calculator->GetSum(storages[i]);
	}

	std::cout << totalSum;

	int finishInteger;
	std::cin >> finishInteger;
    return 0;
}

