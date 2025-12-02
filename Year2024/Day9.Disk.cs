namespace AdventOfCode.Year2024;

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

                if (length > 0)
                {
                    disk._fragments.Add(new Fragment(fileId, length));
                }

                for (int j = 0; j < length; j++)
                {
                    disk._blocks.Add(fileId);
                }
            }

            return disk;
        }

        public void MergeFragments()
        {
            RecalculateBlocks();
            RecalculateFragments();
        }

        public void RecalculateFragments()
        {
            _fragments.Clear();
            Fragment? currentFragment = null;

            foreach (int? block in _blocks)
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
                if (i > j) break; // At this place, i and j can never be equal so > is sufficient. If i or j were out of bounds, an exception would have been thrown already
                _blocks[i] = _blocks[j];
                _blocks[j] = null;
            }
        }

        public void SqueezeFiles()
        {
            for (int? movingFileIndex = LastIndexOfFile(_fragments.Count - 1);
                 movingFileIndex != null;
                 movingFileIndex = LastIndexOfFile(movingFileIndex.Value - 1))
            {
                var movingFile = _fragments[movingFileIndex.Value];
                int? freeSpaceIndex = IndexOfLeftmostFreeSpaceFitting(movingFile.Length);
                if (freeSpaceIndex == null || freeSpaceIndex > movingFileIndex) continue;
                var freeSpace = _fragments[freeSpaceIndex.Value];

                if (freeSpace.Length > movingFile.Length)
                {
                    freeSpace.Length -= movingFile.Length;
                    _fragments.Insert(freeSpaceIndex.Value, (Fragment)movingFile.Clone());
                    movingFileIndex++;
                }
                else // ==
                {
                    freeSpace.FileId = movingFile.FileId;
                }

                movingFile.FileId = null;
            }
        }

        private int? LastIndexOfFile(int startIndex)
        {
            for (int i = startIndex; i >= 0; i--)
            {
                var fragment = _fragments[i];
                if (fragment.FileId != null) return i;
            }

            return null;
        }

        private int? IndexOfLeftmostFreeSpaceFitting(int length)
        {
            for (int i = 0; i < _fragments.Count; i++)
            {
                var fragment = _fragments[i];
                if (fragment.FileId == null && fragment.Length >= length) return i;
            }

            return null;
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
                position += (ulong)fragment.Length;
                ulong sum = CalcSumUpToExcl(position);
                if (fragment.FileId != null) checksum += (ulong)fragment.FileId * (sum - previousSum);
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
            string[] strings = _blocks
                .Select(block => (block == null) ? "." : ((block > 9) ? $"({block})" : $"{block}"))
                .ToArray();

            return string.Join("", strings);
        }

    }

}