namespace AdventOfCode.Year2024;

public partial record Day9
{

    private sealed class Fragment(int? fileId, int length) : ICloneable
    {

        public int? FileId { get; set; } = fileId;

        public int Length { get; set; } = length;

        public object Clone()
        {
            return new Fragment(FileId, Length);
        }

    }

}