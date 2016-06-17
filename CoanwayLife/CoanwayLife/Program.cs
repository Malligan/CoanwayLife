namespace CoanwayLife
{
    using static System.Console;

    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLifeInintalizer consoleLifeInintalizer = new ConsoleLifeInintalizer();
            var lm = consoleLifeInintalizer.InitalizeLifeManager();

            lm.Start();



            ReadLine();
        }
    }
}
