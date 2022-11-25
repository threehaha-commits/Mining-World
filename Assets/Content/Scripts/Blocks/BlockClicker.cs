using System.Collections;
using System.Linq;
using UnityEngine;

public class BlockClicker : MonoBehaviour, IInitialize
{
    [Inject] private BlockFinder _blockFinder;
    private float _findTime;
    private Collider2D[] _blocks;
    private bool leftClick => Input.GetMouseButtonDown(0);
    [Inject] private Camera _camera;
    [Inject] private Player _player;
    private GameObject _block;
    private Anim _anim;
    [SerializeField] private float _weight;
    
    void IInitialize.Initialize()
    {
        _anim = _player.GetComponent<Anim>();
        _findTime = _blockFinder.FindTime;
        StartCoroutine(GetParams());
    }
    
    private void Update()
    {
        if (leftClick)
        {
            var clickPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(clickPosition, transform.forward);
            if (hit.collider && _blocks.Contains(hit.collider))
            {
                _anim.Get().SetBool("Mining", true);
                _anim.Get().SetLayerWeight(1, _weight);
                _block = hit.collider.gameObject;
            }
            else
            {
                _block = null;
            }
        }
    }

    private IEnumerator GetParams()
    {
        while (true)
        {
            _blocks = _blockFinder.blocks;
            yield return new WaitForSeconds(_findTime);
        }
    }

    public Block GetBlock()
    {
        return _block?.GetComponent<Block>();
    }
}