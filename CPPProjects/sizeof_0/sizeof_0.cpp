// sizeof_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"

#include <cstring>
#include <iostream>
#include <string>

#define CONST_1234 "1234"
#define CONST_STR "abcdefghijklmnopqrstuvwxyz"

static std::string gDynamticStr = "";

static const char* GetStr(const char* _Append)
{
	std::string str1(CONST_STR);
	std::string str2 = _Append;
	gDynamticStr = str1 + str2;
	return gDynamticStr.c_str();
}

int main()
{

	std::cout << "size of char is " << sizeof(char) << std::endl;
	std::cout << "size of char * (pointer) is " << sizeof(char*) << std::endl;

	std::cout << "size of define char string " << CONST_1234 << " is " << sizeof(CONST_1234) << std::endl;

	std::string staticCCharString = CONST_STR"26";
	int alphaSize = sizeof(CONST_STR"26");

	std::cout << "size of define char string " << staticCCharString.c_str() << " is " << alphaSize << std::endl;

	std::string dynamicStr = GetStr("26");

	int sizeofLength = sizeof(GetStr("26"));
	int stringLength = dynamicStr.length();

	std::cout << "size of char * string " << dynamicStr.c_str() << " is " << sizeofLength << std::endl;
	std::cout << "size of string length" << dynamicStr.c_str() << " is " << stringLength << std::endl;

	int finishInteger = 0 ;
	std::cin >> finishInteger;
    return 0;
}

