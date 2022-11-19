public class Stack
{
    private int _currentCount;
    private readonly int _maxCount = 24;
    public bool IsFull => _currentCount == _maxCount;

    public int Size()
    {
        return _currentCount;
    }
    
    public void Add()
    {
        _currentCount++;
    }

    public void Remove()
    {
        _currentCount--;
    }
}