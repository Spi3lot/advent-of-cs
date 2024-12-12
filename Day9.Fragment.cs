namespace AdventOfCode;
public partial record Day9
{

    private sealed class Fragment(int? fileId, int length)
    {

        public int? FileId { get; } = fileId;

        public int Length { get; set; } = length;

    }

}
