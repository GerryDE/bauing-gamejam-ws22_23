using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FenceController : MonoBehaviour
{
    [Range(0, 1000)] public int maxHp = 100;

    private Rigidbody2D _rigidbody;
    private int _currentHp;

    private void Awake()
    {
        
        _currentHp = maxHp;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy"))) return;

        _currentHp -= 1;
        Debug.Log(_currentHp);

        if (_currentHp > 0) return;
        Debug.Log("Fence destroyed!");
        Destroy(gameObject);
    }
}