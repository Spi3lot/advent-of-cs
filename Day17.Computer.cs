namespace AdventOfCode;

public partial record Day17
{

    private sealed class Computer(int registerCount)
    {

        private readonly ICollection<byte> _outputs = [];

        private readonly long[] _registers = new long[registerCount];

        private int _instructionPointer;

        public long this[char register]
        {
            get => _registers[register - 'A'];

            set => _registers[register - 'A'] = value;
        }

        public ICollection<byte> Execute(params Instruction[] instructions)
        {
            _outputs.Clear();

            for (_instructionPointer = 0; _instructionPointer < instructions.Length; _instructionPointer++)
            {
                instructions[_instructionPointer].Execute(this);
            }

            return [.._outputs];
        }

        public long Combo(byte operand)
        {
            return (operand < 4) ? operand : _registers[operand - 4];
        }

        public void Jnz(int to)
        {
            if (this['A'] != 0) _instructionPointer = to / 2 - 1;
        }

        public void Out(byte value)
        {
            _outputs.Add(value);
        }

        public override string ToString()
        {
            return string.Join(',', _outputs);
        }

    }

}