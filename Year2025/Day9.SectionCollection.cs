using System.Collections;

namespace AdventOfCode.Year2025;

public class SectionCollection<T> : IEnumerable<(T Item, int Size)>
{

    private readonly List<T> _sectionItems = [];

    private readonly List<int> _sectionEndIndices = [];
    
    public int SectionCount => _sectionItems.Count;

    public void AddSection(T item, int size)
    {
        if (size <= 0)
        {
            throw new InvalidOperationException("Size must be greater than zero.");
        }

        int lastIndex = (_sectionItems.Count == 0) ? -1 : _sectionEndIndices[^1];
        _sectionEndIndices.Add(lastIndex + size);
        _sectionItems.Add(item);
    }

    public T this[int index]
    {
        get
        {
            int sectionIndex = _sectionEndIndices.BinarySearch(index);
            return _sectionItems[(sectionIndex < 0) ? ~sectionIndex : sectionIndex];
        }
    }

    public IEnumerator<(T Item, int Size)> GetEnumerator()
    {
        int accumulatedLength = -1;
        
        for (int i = 0; i < SectionCount; i++)
        {
            yield return (_sectionItems[i], _sectionEndIndices[i] - accumulatedLength);
            accumulatedLength += _sectionEndIndices[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}