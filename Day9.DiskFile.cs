using System.Runtime.CompilerServices;

namespace AdventOfCode;

public partial record Day9
{

    public class FileFragment(int? fileId, int length)
    {

        public int? FileId { get; set; } = fileId;

        public int Length { get; set; } = length;

    }

}