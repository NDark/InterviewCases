// Pointer_0.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <vector>
#include <string>

class Character
{
public :
	Character(const std::string &_Type );

	void TryMoveTo();

public :
	
	float x;
	float goal;
	int goalNum;
private :
	std::string m_Type;
};

Character::Character( const std::string &_Type ) 
	: x( 0.0f ) 
	, goal(0.0f)
	, goalNum( 0 )
{
	m_Type = _Type;
}

void Character::TryMoveTo()
{
	if (fabs(x - goal) > 0.01f)
	{
		if( goal > x )
			x += 0.001;
		else
			x -= 0.001;
	}
	else
	{
		goal = (rand() % 1000) / 1000; // get another goal
		++goalNum;
		std::cout << this->m_Type << " reach goal" << std::endl;
	}

}

static std::vector<Character*> sCharacterTemplate;
std::vector<Character*> gCurrentCharacterVec;

void InitTemplate()
{
	sCharacterTemplate.push_back(new Character("IronMan"));
	sCharacterTemplate.push_back(new Character("Hulk"));
	sCharacterTemplate.push_back(new Character("Loki"));
	sCharacterTemplate.push_back(new Character("Captain"));
	sCharacterTemplate.push_back(new Character("Batman"));

}

void InitCharacter( int _CharacterNum)
{
	int index = 0;
	for (int i = 0; i < _CharacterNum; ++i)
	{
		index = rand() % sCharacterTemplate.size();
		Character* pChar = sCharacterTemplate[index];
		gCurrentCharacterVec.push_back(pChar);
	}
}

void MainLogic()
{
	for (int time = 0; time < 100; ++time)
	{
		for (int charIndex = 0; charIndex < gCurrentCharacterVec.size(); ++charIndex)
		{
			if (nullptr != gCurrentCharacterVec[charIndex])
			{
				gCurrentCharacterVec[charIndex]->TryMoveTo();
			}
		}
	}
}


int main()
{
	InitTemplate();
	InitCharacter( 30 );

	MainLogic();

	for (int charIndex = 0; charIndex < gCurrentCharacterVec.size(); ++charIndex)
	{
		if (nullptr != gCurrentCharacterVec[charIndex])
		{
			std::cout << charIndex << " " << gCurrentCharacterVec[charIndex]->goalNum << std::endl ;
		}
	}

	int finishInteger;
	std::cin >> finishInteger;
	return 0;
}

