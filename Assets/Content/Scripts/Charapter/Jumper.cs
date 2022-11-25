using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _power;
    private readonly Jump _jump = new ();
    private float _distanceToGround;
    private Rigidbody2D _r2d;
    private bool _isGround => RayHelper.IsGround(transform, _distanceToGround);
    private bool _isKeySpaceDown => Input.GetKeyDown(KeyCode.Space);
    
    private void Start()
    {
        _r2d = GetComponent<Rigidbody2D>();
        _distanceToGround = RayHelper.DistanceToGround(transform);
    }

    private void Update()
    {
        if (_isKeySpaceDown && _isGround)
            StartCoroutine(_jump.DoJump(_r2d, _power, _distanceToGround));
    }
}