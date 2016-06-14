namespace CoanwayLife
{
    using System;
    using System.Linq;
    using static System.Console;
    using System.Threading;
    using System.Text;

    class LifeManager
    {
        public Field Field { get; set; }

        public int TimeBetweenIterations { get; set; } = 1000;

        public LifeManager()
        {

        }

        public LifeManager(int time)
        {
            TimeBetweenIterations = time;
        }

        public void DrawIteration()
        {
            for (int i0 = 0; i0 < Field.FieldList.GetLength(0); i0++)
            {
                for (int i1 = 0; i1 < Field.FieldList.GetLength(1); i1++)
                {
                    switch (Field.FieldList[i0, i1])
                    {
                        case 0:
                            Write(" ");
                            break;
                        case 1:
                            Write("█");
                            break;
                    }

                }
                WriteLine();
            }
        }

        public void Start()
        {
            GenerateField();

            while (true)
            {
                DrawIteration();
                ProcessIteration();
                Thread.Sleep(TimeBetweenIterations);
                Clear();
            }
        }

        public void ProcessIteration()
        {
            var newField = new int[Field.Lines, Field.Columns];

            for (int line = 0; line < Field.FieldList.GetLength(0); line++)
                for (int column = 0; column < Field.FieldList.GetLength(1); column++)
                {
                    var sum = Field.GetSibilings(line, column).Sum();
                    if (Field.FieldList[line, column] == 1)
                    {
                        if (sum == 2 || sum == 3)
                            newField[line, column] = 1;
                    }
                    if (Field.FieldList[line, column] == 0 && sum == 3)
                    {
                        newField[line, column] = 1;
                    }
                }

            Field = new Field(Field.Lines, Field.Columns, newField);
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
        }

        public void SizeSetup()
        {
            WriteLine("Enter number of lines and columns.\n" +
                      "Example: \"10 10\" generate 10x10 field");

            //238 max - console size columns
            //63 max - console size lines
            var size = ReadLine()?.Split(' ').ToList().Select(int.Parse).ToList();

            SetWindowSize(size[1] + 1, size[0] + 1);

            Field = new Field(size[0], size[1], new int[size[0], size[1]]);

            Clear();

        }

        public void GenerateField()
        {
            SizeSetup();

            //custom input from file input.txt
            RandomOrCustom();
        }
    }
}
