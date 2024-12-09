namespace AdventOfCode;

public partial record Day7
{

    private record Equation(params ulong[] Numbers)
    {

        public static Equation Parse(string line)
        {
            return new Equation(
                line.Split(':', ' ')
                    .Select(ulong.Parse)
                    .ToArray()
            );
        }

        public bool IsSolvable()
        {
            return true;
        }

    }

}