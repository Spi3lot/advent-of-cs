namespace AdventOfCode;

public partial record Day25
{

    private sealed class Schematic
    {

        public int[] Heights { get; }

        public int Space { get; }

        public bool IsKey { get; }

        public Schematic(string[] schematic)
        {
            string firstLine = schematic[0];
            Heights = new int[firstLine.Length];
            Space = schematic.Length;
            IsKey = firstLine[0] == '.';

            foreach (string line in schematic)
            {
                foreach ((int index, char @char) in line.Index())
                {
                    // TODO
                }
            }
        }



    }

}