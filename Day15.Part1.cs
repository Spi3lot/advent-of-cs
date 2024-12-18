using System.Text;

namespace AdventOfCode;

public partial record Day15
{
    
    public override void SolvePart1()
    {
        Initialize();

        foreach (char movement in _movements!)
        {
            Part1.Bot.Move(movement);
        }

        Console.WriteLine(Part1.Bot.SumBoxGpsCoordinates());
    }

}