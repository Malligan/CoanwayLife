namespace CoanwayLife
{
    using System.Collections.Generic;

    internal class Field
    {
        //lines -> columns [line][column]
        public int[,] FieldList { get; set; }

        public int Lines { get; set; }

        public int Columns { get; set; }

        public Field()
        {
            FieldList = new int[Lines, Columns];
        }

        public Field(int lines, int columns, int[,] arr)
        {
            Lines = lines;
            Columns = columns;
            FieldList = arr;
        }

        

        public IList<int> GetSibilings(int line, int column)
        {

            /*
             * 1 2 3
             * 4 x 5
             * 6 7 8
             */

            var line123 = line == 0 ? Lines - 1 : line - 1;
            var line45 = line;
            var line678 = line == Lines - 1 ? 0 : line + 1;

            var column146 = column == 0 ? Columns - 1 : column - 1;
            var column27 = column;
            var column358 = column == Columns - 1 ? 0 : column + 1;

            IList<int> result = new List<int>(8);

            result.Add(FieldList[line123,   column146]);
            result.Add(FieldList[line123,   column27]);
            result.Add(FieldList[line123,   column358]);
            result.Add(FieldList[line45,    column146]);
            result.Add(FieldList[line45,    column358]);
            result.Add(FieldList[line678,   column146]);
            result.Add(FieldList[line678,   column27]);
            result.Add(FieldList[line678,   column358]);

            return result;
        }
    }
}
