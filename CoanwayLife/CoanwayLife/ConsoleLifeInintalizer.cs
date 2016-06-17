namespace CoanwayLife
{
    using System;
    using static System.Console;
    using System.Linq;
    using System.Text;

    internal class ConsoleLifeInintalizer
    {
        public int Time { get; set; }

        public Field Field { get; set; }

        public LifeManager InitalizeLifeManager()
        {
            SpeedSetup();
            SizeSetup();
            RandomOrCustom();

            return new LifeManager(Time, Field);
        }

        public void SpeedSetup()
        {
            WriteLine("Enter time between iterations in ms.\n" +
                      "Example: 500 (0.5 second)");

            Time = int.Parse(ReadLine());

            Clear();
        }

        public void SizeSetup()
        {
            WriteLine("Enter number of lines and columns.\n" +
                      "Example: \"10 10\" generate 10x10 field");

            //238 max - console size columns
            //63 max - console size lines
            var size = ReadLine()?.Split(' ').ToList().Select(int.Parse).ToList();

            Field = new Field(size[0], size[1], new int[size[0], size[1]]);

            

            Clear();

        }

        //custom - from input.txt
        //random - random and write start matrix in file

        public void RandomOrCustom()
        {
            WriteLine("Randomly\ngenereated\nfield?\n(y/n)");

            var random = true;
            var completedStep = 0;

            while (completedStep != 3)
            {
                switch (ReadLine())
                {
                    case "y":
                        WriteLine("Random\nchoosen.");
                        random = true;
                        completedStep = 3;
                        break;
                    case "n":
                        random = false;
                        completedStep = 3;
                        break;
                    default:
                        WriteLine("Try to enter \"y\" or \"n\"");
                        completedStep++;
                        break;
                }
            }

            if (random)
            {
                var r = new Random();

                var log = new StringBuilder();

                var t = DateTime.Now;
                var logDate = $"{t.Year}-{t.Month}-{t.Day}-{t.Hour}-{t.Minute}-{t.Second}";

                var logName = $"{Field.Lines}x{Field.Columns}-{logDate}.txt";

                for (int line0 = 0; line0 < Field.FieldList.GetLength(0); line0++)
                {
                    for (int column1 = 0; column1 < Field.FieldList.GetLength(1); column1++)
                    {
                        Field.FieldList[line0, column1] = r.NextDouble() > 0.5 ? 1 : 0;
                        log.Append(Field.FieldList[line0, column1]);
                        log.Append(" ");
                    }
                    log.Append("\n");
                }

                System.IO.File.WriteAllText(logName, log.ToString());


            }
            else
            {
                WriteLine("Input\nfrom\ninput.txt");

                string[] lines = System.IO.File.ReadAllLines(@"input.txt");

                for (int i0 = 0; i0 < Field.FieldList.GetLength(0); i0++)
                {
                    var numbers = lines[i0].Split(' ').Select(int.Parse).ToList();
                    for (int i1 = 0; i1 < Field.FieldList.GetLength(1); i1++)
                    {
                        Field.FieldList[i0, i1] = numbers[i1];
                    }
                }
            }
            Clear();
            SetWindowSize(Field.Columns + 1, Field.Lines + 1);
        }
    }
}
