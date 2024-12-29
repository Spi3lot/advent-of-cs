namespace AdventOfCode;

public partial record Day17 : AdventDay<Day17>
{

    private readonly Computer _computer;

    private readonly Instruction[] _instructions;

    private readonly byte[] _program;

    public Day17()
    {
        string[] parts = Input.Split("\n\n");
        string[] registers = parts[0].Split('\n');
        _computer = new Computer(registers.Length);
        _program = parts[1].Split(": ")[1].TrimEnd().Split(',').Select(byte.Parse).ToArray();
        _instructions = new Instruction[_program.Length / 2];
        byte[] opcodes = _program.Where((_, index) => index % 2 == 0).ToArray();
        byte[] operands = _program.Where((_, index) => index % 2 == 1).ToArray();

        foreach (string line in registers)
        {
            string[] register = line.Split(": ");
            _computer[register[0][^1]] = int.Parse(register[1]);
        }

        foreach (var (index, (opcode, operand)) in opcodes.Zip(operands).Index())
        {
            _instructions[index] = new Instruction(opcode, operand);
        }
    }

    public override void SolvePart1()
    {
        Console.WriteLine(string.Join(',', _computer.Execute(_instructions)));
    }

    /// <summary>
    /// Hard coded for given input but hey, it works
    /// </summary>
    public override void SolvePart2()
    {
        long a = 0;

        for (int i = _program.Length - 1; i >= 0; i--)
        {
            a <<= 3;

            while (!_program[i..].SequenceEqual(_computer.Execute(_instructions)))
            {
                _computer['A'] = ++a;
            }
        }

        Console.WriteLine(a);
    }

}