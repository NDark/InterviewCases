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
        static void InitStorage(Storage[] _Storages )
        {
            for (int i = 0; i < _Storages.Length; ++i)
            {
                if (null == _Storages[i])
                {
                    _Storages[i] = new Storage();
                }
                else
                {
                    _Storages[i].m_Storage.Clear();
                }
                
                _Storages[i].m_Storage.Add(i);// fill the storages with [0] [1] ... [length-1]
            }
        }

        static void Main(string[] args)
        {
            Storage[] storages = new Storage[100];
            InitStorage(storages);// fill the storages with [0] [1] ... [length-1]

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
