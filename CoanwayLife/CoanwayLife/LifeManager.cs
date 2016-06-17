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

        public LifeManager(int time, Field field)
        {
            TimeBetweenIterations = time;
            Field = field;
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

        

        

        

        
    }
}
