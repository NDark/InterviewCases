using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhileLoop_0
{
    class Program
    {
        static Random sRandom = null;

        static int GetResource()
        {
            int ret = 0 ;
            if( null != sRandom )
            {
                ret = (0 == sRandom.Next(2)) ? 1 : 2;
            }
            return ret;
        }

        static void Main(string[] args)
        {
            sRandom = new Random();

            int goal = 100;
            int resource = 0;

            while (true)
            {
                resource += GetResource();
                Console.WriteLine(resource);
                if (resource == goal)
                {
                    break;
                }
            }

            Console.WriteLine(resource);

            Console.ReadLine();
        }
    }
}
