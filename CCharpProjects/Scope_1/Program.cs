using System;

namespace Scope_1
{
    class Program
    {
        // assume the function fetch one user's variable time.
        static DateTime FetchUsersTime()
        {
            DateTime time = new DateTime();
            return time;
        }

        static void Main(string[] args)
        {
        
            int sum = 0;
            Random random = new Random();
            for (int i = 0; i< 100; ++i)
	        {
		        if (0 == i % 2)
		        {
			        if (random.Next(10) > 3 )
			        {
				        if ( FetchUsersTime().DayOfWeek !=  DayOfWeek.Sunday )
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
