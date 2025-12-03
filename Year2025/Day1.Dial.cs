namespace AdventOfCode.Year2025;

public class Dial
{

    private int _point = 50;

    public int Rotate(string amount)
    {
        return Rotate(ConvertStringAmountToInt(amount));
    }

    public int Rotate(int amount)
    {
        _point = (_point + amount) % 100;
        return _point;
    }

    public IEnumerable<int> RotateIncrementally(string amount)
    {
        return RotateIncrementally(ConvertStringAmountToInt(amount));
    }

    public IEnumerable<int> RotateIncrementally(int amount)
    {
        int direction = Math.Sign(amount);

        for (int i = 0; i < Math.Abs(amount); i++)
        {
            _point = (_point + direction) % 100;
            yield return _point;
        }
    }

    private static int ConvertStringAmountToInt(string amount)
    {
        int sign = amount[0] switch
        {
            'L' => -1,
            'R' => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(amount), amount[0], "Invalid direction. Only L and R are allowed.")
        };
        
        return sign * int.Parse(amount[1..]);
    }

}