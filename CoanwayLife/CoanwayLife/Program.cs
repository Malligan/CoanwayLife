namespace CoanwayLife
{
    using static System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var lm = new LifeManager(500);

            lm.Start();



            ReadLine();
        }
    }
}
