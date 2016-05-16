using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointer_0
{

    class Character
    {
        public Character(string _Type)
        {
            m_Type = _Type;
        }

        public void TryMoveTo()
        {
            if (Math.Abs(x - goal) > 0.01f)
            {
                if (goal > x)
                    x += 0.001f;
                else
                    x -= 0.001f;
            }
            else
            {
                Random r = new Random();

                goal = (float)( r.Next( 1000 ) )  / 1000.0f;
                ++goalNum;
                System.Console.Out.WriteLine(this.m_Type + " reach goal" );
            }
        }
        
	    public float x = 0.0f ;
        public float goal = 0.0f ;
        public int goalNum = 0 ;
	    private string m_Type ;
    };
    



    class Program
    {

        static List<Character> sCharacterTemplate = new List<Character>();
        static List<Character> gCurrentCharacterVec = new List<Character>() ;

        static void InitTemplate()
        {
            sCharacterTemplate.Add(new Character("IronMan"));
            sCharacterTemplate.Add(new Character("Hulk"));
            sCharacterTemplate.Add(new Character("Loki"));
            sCharacterTemplate.Add(new Character("Captain"));
            sCharacterTemplate.Add(new Character("Batman"));

        }

        static void InitCharacter(int _CharacterNum)
        {
            int index = 0;
            Random r = new Random();
            for (int i = 0; i < _CharacterNum; ++i)
            {
                index = r.Next( sCharacterTemplate.Count ) ;
                Character pChar = sCharacterTemplate[index];
                gCurrentCharacterVec.Add(pChar);
            }
        }

        static void MainLogic()
        {
            for (int time = 0; time < 100; ++time)
            {
                for (int charIndex = 0; charIndex < gCurrentCharacterVec.Count; ++charIndex)
                {
                    if (null != gCurrentCharacterVec[charIndex])
                    {
                        gCurrentCharacterVec[charIndex].TryMoveTo();
                    }
                }
            }
        }


        static void Main(string[] args)
        {
            InitTemplate();
            InitCharacter(30);

            MainLogic();

            for (int charIndex = 0; charIndex < gCurrentCharacterVec.Count; ++charIndex)
            {
                if (null != gCurrentCharacterVec[charIndex])
                {
                    System.Console.Out.WriteLine("" + charIndex + " " + gCurrentCharacterVec[charIndex].goalNum );
                }
            }

            System.Console.In.ReadLine();
        }
    }
}
