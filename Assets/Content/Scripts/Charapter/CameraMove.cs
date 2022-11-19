using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Inject] private Player _player;
    private Transform _playerTransform => _player.transform;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
    }
    
    private void FixedUpdate()
    {
        if (_player == null)
            return;
        _transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y, -10);
    }
}