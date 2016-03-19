using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scope_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time = new DateTime();
            
        
            int sum = 0;
            Random random = new Random();
            for (int i = 0; i< 100; ++i)
	        {
		        if (0 == i % 2)
		        {
			        if (random.Next(10) > 3 )
			        {
				        if (time.DayOfWeek !=  DayOfWeek.Sunday )
				        {
                            Console.WriteLine( "" + i + " continue");
					        continue;
				        }
                    }
		        }
		        ++sum;
	        }

	        Console.WriteLine( sum );

            Console.ReadLine();
        }
    }
}
