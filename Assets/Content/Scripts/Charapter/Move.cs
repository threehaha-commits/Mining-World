using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Transform _transform;
    
    public float Direction
    {
        get;
        private set;
    }

    private void Start()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        Direction = Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime;
        var flip = transform.rotation.y == 0 ? 1 : -1;
        _transform.Translate(Direction * flip, 0, 0);
        AnimHandler.Direction(Direction);
    }
}