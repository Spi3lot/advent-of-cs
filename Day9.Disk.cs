namespace AdventOfCode;
public partial record Day9
{

    private sealed class Disk(ICollection<(int? id, int length)> files)
    {

        public readonly ICollection<(int? id, int length)> Files = files;

        public static Disk FromMap(string map)
        {
            ICollection<(int? id, int length)> files = [];

            for (int i = 0; i < map.Length; i++)
            {
                int? id = (i % 2 == 0) ? i / 2 : null;
                int length = map[i] - '0';
                files.Add((id, length));
                Console.WriteLine((id, length));
            }

            return new Disk(files);
        }

        public int CalculateChecksum()
        {
            int checksum = 0;

            for (int i = 0; ; i++)
            {
                var (id, length) = Files.ElementAt(i);
                //cumulativeSum += GetSum(i..(i + length));
                checksum += i * ;
            }

            return checksum;
        }

        private static int GetSum(Range range)
        {
            return GetSumUpTo(range.End.Value) - GetSumUpTo(range.Start.Value);
        }

        private static int GetSumUpTo(int n)
        {
            return n * (n + 1) / 2;
        }

        public override string ToString()
        {
            var chars = Files.SelectMany(file => new string(file.id == null ? '.' : $"{file.id}"[0], file.length)).ToArray();
            return new string(chars);
        }

    }

}