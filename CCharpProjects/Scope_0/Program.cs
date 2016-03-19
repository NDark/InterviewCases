using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Storage
{
    public List<int> m_Storage = new List<int>() ;
};

class StorageCalculator
{
    public int GetSum(Storage _Src)
    {
        if (0 == summation)
        {
            Calculate(_Src);
        }
        Console.Out.WriteLine(summation);
        return summation;
    }

    void Calculate(Storage _Src)
    {
        summation = 0;
        for (int i = 0; i < _Src.m_Storage.Count(); ++i)
        {
            summation += _Src.m_Storage[i];
        }
    }

    private int summation { get; set; }
};


namespace Scope_0
{
    class Program
    {
        static void Main(string[] args)
        {

            Storage [] storages = new Storage [100];
            for (int i = 0; i < 100; ++i)
            {
                storages[i] = new Storage();
                storages[i].m_Storage.Add(i);
            }

            int totalSum = 0;
            for (int i = 0; i < 100; ++i)
            {
                StorageCalculator calculator = new StorageCalculator();
                totalSum += calculator.GetSum(storages[i]);
            }

            Console.WriteLine( totalSum );

            Console.ReadLine();
        }
    }
}
