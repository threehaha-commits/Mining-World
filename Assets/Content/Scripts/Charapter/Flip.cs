using UnityEngine;

[RequireComponent(typeof(Move))]
public class Flip : MonoBehaviour
{
    private Move _move;
    private SpriteRenderer[] _renderers;
    private Transform _transform;
    
    private void Start()
    {
        _transform = transform;
        _move = GetComponent<Move>();
        _renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _transform.eulerAngles = new Vector3(0, 0, 0);
        if(Input.GetKeyDown(KeyCode.D))
            _transform.eulerAngles = new Vector3(0, -180, 0);
    }
}