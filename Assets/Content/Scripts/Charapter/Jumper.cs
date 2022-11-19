using System;
using Unity.VisualScripting;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _height;
    [SerializeField] private AnimationCurve _curve;
    private readonly Jump _jump = new Jump();
    private Rigidbody2D r2d;
    private float _progress = 1f;
    private float _expiredTime = 0f;
    private Vector3 _startPos;
    
    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(_jump.DoJump(transform, _curve, _duration, _height));
        }
        
        if(_progress < 1)
        {
            _expiredTime += Time.deltaTime;
            _progress = _expiredTime / _duration;
            var value = _curve.Evaluate(_progress);
            transform.position = new Vector3(transform.position.x, _startPos.y + (value * _height),
                transform.position.z);
            var layerMask = LayerMask.GetMask("Mining");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10f, layerMask);
            if (hit.collider != null)
            {
                if (hit.distance < 1.09f)
                    _progress = 1f;
            }
        }
    }
}