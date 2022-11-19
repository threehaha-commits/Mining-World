using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField] private int _hitCount;

    public void SetHit(int value)
    {
        _hitCount = value;
    }

    public int GetHit()
    {
        return _hitCount;
    }
    
    public void Hit()
    {
        _hitCount--;
        if (_hitCount <= 0)
            gameObject.SetActive(false);
    }
}