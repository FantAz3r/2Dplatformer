using System;
using UnityEngine;

public class GroundDetecter : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private BoxCollider2D _groundDetecter;

    private int _counter = 0;
    public bool IsGrounded => _counter > 0;

    public event Action Grounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _groundLayer) != 0)
        {
            if(_counter == 0)
            {
                Grounded?.Invoke();
            }

            _counter++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _groundLayer) != 0)
        {
            _counter--;
        }
    }
}
