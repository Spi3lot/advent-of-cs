namespace AdventOfCode;

public partial record Day9
{

    private sealed class Disk
    {

        private readonly IList<int?> _blocks = [];

        private readonly IList<Fragment> _fragments = [];

        public static Disk FromMap(string map)
        {
            var disk = new Disk();

            for (int i = 0; i < map.Length; i++)
            {
                int? fileId = (i % 2 == 0) ? i / 2 : null;
                int length = map[i] - '0';
                disk._fragments.Add(new Fragment(fileId, length));

                for (int j = 0; j < length; j++)
                {
                    disk._blocks.Add(fileId);
                }
            }

            return disk;
        }

        public void RecalculateFragments()
        {
            _fragments.Clear();
            Fragment? currentFragment = null;

            foreach (var block in _blocks)
            {
                if (currentFragment == null || currentFragment.FileId != block)
                {
                    currentFragment = new Fragment(block, 1);
                    _fragments.Add(currentFragment);
                }
                else
                {
                    currentFragment.Length++;
                }
            }
        }

        public void RecalculateBlocks()
        {
            _blocks.Clear();

            foreach (var fragment in _fragments)
            {
                for (int i = 0; i < fragment.Length; i++)
                {
                    _blocks.Add(fragment.FileId);
                }
            }
        }

        public void FragmentBlocks()
        {
            int i = 0;
            int j = _blocks.Count - 1;

            while (true)
            {
                while (_blocks[i] != null) i++;
                while (_blocks[j] == null) j--;
                if (i > j) break;  // At this place, i and j can never be equal so > is sufficient. If i or j were out of bounds, an exception would have been thrown already
                _blocks[i] = _blocks[j];
                _blocks[j] = null;
            }

        }

        public void Defragment()
        {
            RecalculateFragments();
        }

        public ulong CalculateChecksum()
        {
            ulong checksum = 0;

            for (int i = 0; i < _blocks.Count; i++)
            {
                int? block = _blocks[i];
                if (block == null) continue;
                checksum += (ulong)(block * i);
            }

            return checksum;
        }

        public ulong CalculateChecksumFast()
        {
            ulong checksum = 0;
            ulong previousSum = 0;
            ulong position = 0;

            foreach (var fragment in _fragments)
            {
                if (fragment.FileId == null) continue;
                position += (ulong)fragment.Length;
                ulong sum = CalcSumUpToExcl(position);
                checksum += (ulong)fragment.FileId * (sum - previousSum);
                previousSum = sum;
            }

            return checksum;
        }

        private static ulong CalcSumUpToExcl(ulong n)
        {
            return n * (n - 1) / 2;
        }

        public override string ToString()
        {
            char[] chars = _blocks.Select(block => (block == null) ? '.' : $"{block}"[0]).ToArray();
            return new string(chars);
        }

    }

}