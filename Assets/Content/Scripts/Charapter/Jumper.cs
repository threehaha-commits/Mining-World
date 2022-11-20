using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _height;
    [SerializeField] private AnimationCurve _curve;
    private readonly Jump _jump = new ();
    private float _distanceToGround;
    private bool _isGround => RayHelper.IsGround(transform, _distanceToGround);
    private bool _isKeySpaceDown => Input.GetKeyDown(KeyCode.Space);
    
    private void Start()
    {
        _distanceToGround = RayHelper.DistanceToGround(transform);
    }

    private void Update()
    {
        if (_isKeySpaceDown && _isGround)
            StartCoroutine(_jump.DoJump(transform, _curve, _duration, _height, _distanceToGround));
    }
}