namespace AdventOfCode;

public partial record Day17
{

    private sealed class Instruction(byte opcode, byte operand)
    {

        private static readonly Action<Computer, byte>[] Actions =
        [
            (computer, operand) => computer['A'] >>= Convert.ToInt32(computer.Combo(operand)),
            (computer, operand) => computer['B'] ^= operand,
            (computer, operand) => computer['B'] = computer.Combo(operand) & 7,
            (computer, operand) => computer.Jnz(operand),
            (computer, operand) => computer['B'] ^= computer['C'],
            (computer, operand) => computer.Out((byte) (computer.Combo(operand) & 7)),
            (computer, operand) => computer['B'] = computer['A'] >> Convert.ToInt32(computer.Combo(operand)),
            (computer, operand) => computer['C'] = computer['A'] >> Convert.ToInt32(computer.Combo(operand)),
        ];

        private readonly Action<Computer> _action = computer => Actions[opcode](computer, operand);

        public void Execute(Computer computer) => _action(computer);

    }

}