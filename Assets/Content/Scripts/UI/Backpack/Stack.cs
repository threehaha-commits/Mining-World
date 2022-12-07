public class Stack : IIncreasable, IDecreasable
{
    private int _currentCount;
    private readonly int _maxCount = 24;
    public bool IsFull => _currentCount == _maxCount;

    public int Size()
    {
        return _currentCount;
    }
    
    public void Increase()
    {
        _currentCount++;
    }

    public void Increase(int value)
    {
        _currentCount += value;
    }

    public void Decrease()
    {
        _currentCount--;
    }
    
    public void Decrease(int value)
    {
        _currentCount -= value;
    }
}