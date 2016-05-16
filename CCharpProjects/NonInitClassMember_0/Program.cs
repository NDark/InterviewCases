
namespace ClassMember
{
    class Tooth
    {
        public int Age { get; set; }
    }

    class Character
    {
    	public void Print()
        {
            System.Console.Out.WriteLine(m_Tooth0.Age);
            System.Console.Out.WriteLine(m_Tooth1.Age);
        }

	    private Tooth m_Tooth0 { get; set; }
        private Tooth m_Tooth1;
    };

    class Program
    {
        static void Main(string[] args)
        {
            Character obj = new Character();
            obj.Print();

            System.Console.In.ReadLine();
        }
    }
}
