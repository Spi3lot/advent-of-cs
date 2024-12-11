namespace AdventOfCode;

public partial record Day9
{

    private sealed record Disk(IList<FileFragment> Fragments)
    {

        public static Disk FromMap(string map)
        {
            IList<FileFragment> files = [];

            for (int i = 0; i < map.Length; i++)
            {
                int? fileId = i % 2 == 0 ? i / 2 : null;
                int length = map[i] - '0';
                files.Add(new FileFragment(fileId, length));
            }

            return new Disk(files);
        }

        public void Defragment()
        {
            for (int i = Fragments.Count - 1; i >= 0; i--)
            {
                var movingFragment = Fragments[i];
                // TODO
            }
        }

        public int CalculateChecksum()
        {
            int checksum = 0;
            int position = 0;
            int previousSum = 0;

            foreach (var fragment in Fragments)
            {
                if (fragment.FileId == null) continue;
                position += fragment.Length;
                int sum = CalcSumUpToExcl(position);
                checksum += sum - previousSum;
                previousSum = sum;
            }

            return checksum;
        }

        private static int CalcSumUpToExcl(int n)
        {
            return n * (n - 1) / 2;
        }

        public override string ToString()
        {
            char[] chars = Fragments
                .SelectMany(fragment => new string(
                    (fragment.FileId == null)
                        ? '.'
                        : $"{fragment.FileId}"[0],
                    fragment.Length
                    ))
                .ToArray();

            return new string(chars);
        }

    }

}