using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class V2Data
{
    public V2Data(float X, float Y)
    {
        X = X;
        Y = Y;
    }

    public void Print()
    {
        System.Console.Out.WriteLine(string.Format("{0} {1}", X, Y));
    }
    
	private float X = 0 ;
    private float Y = 0;

};


namespace ConstructorArgument_0
{
    class Program
    {
        static void Main(string[] args)
        {
            V2Data obj = new V2Data(1.0f, 2.0f);
            obj.Print();

            System.Console.In.ReadLine();
        }
    }
}
