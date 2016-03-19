using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForLoop_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> resourceVec = new List<int>();

            int loopNum = 100;
            int count = 0;

            for (; loopNum >= 0; --loopNum)
            {
                resourceVec.Add(0);
                System.Console.Out.WriteLine(++count);
            }

            
            System.Console.In.ReadLine();
        }
    }
}
