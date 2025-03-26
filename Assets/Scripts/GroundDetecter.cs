using System;
using UnityEngine;

public class GroundDetecter : MonoBehaviour
{
    [SerializeField] private Collider2D _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private int _counter = 0;
    public bool IsGrounded => _counter > 0;

    public event Action Grounded; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == _groundLayer)
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
        if (collision.gameObject.layer == _groundLayer)
        {
            _counter--;
        }
    }
}
