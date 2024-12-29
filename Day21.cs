namespace AdventOfCode;

public partial record Day21 : AdventDay<Day21>
{

    private static readonly KeyPad NumericKeyPad = new("789", "456", "123", " 0A");

    private static readonly KeyPad DirectionalKeyPad = new(" ^A", "<v>");

    private readonly string[] _sequences;

    public Day21()
    {
        _sequences = Input.Trim().Split('\n');
    }

    public override void SolvePart1()
    {
        int sum = 0;

        foreach (string code in _sequences)
        {
            Console.WriteLine(code);
            string sequence = NumericKeyPad.GetSequenceForPressing(code);
            Console.WriteLine(sequence);
            sequence = DirectionalKeyPad.GetSequenceForPressing(sequence);
            Console.WriteLine(sequence);
            sequence = DirectionalKeyPad.GetSequenceForPressing(sequence);
            Console.WriteLine(sequence);
            sum += sequence.Length * int.Parse(code[..^1]);
        }

        Console.WriteLine(sum);
    }

    public override void SolvePart2()
    {
        string sequence = "<v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A";
        Console.WriteLine(sequence);
        sequence = DirectionalKeyPad.Press(sequence);
        Console.WriteLine(DirectionalKeyPad.GetSequenceForPressing(sequence)); // compare this 
        Console.WriteLine(sequence); // to this, because then they are actually trying to type the same thing
        sequence = DirectionalKeyPad.Press(sequence);
        Console.WriteLine(sequence);
        sequence = NumericKeyPad.Press(sequence);
        Console.WriteLine(sequence);
    }

}