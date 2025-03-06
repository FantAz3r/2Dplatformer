using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private KeyCode JumpKey = KeyCode.Space;

    public event Action Jumped;
    public event Action<float> MovedRight;
    public event Action<float> MovedLeft;
    public float Direction { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal); 

        if (Input.GetKeyDown(JumpKey))
        {
            Jumped?.Invoke();
        }

        if (Direction < 0)
        {
            MovedRight?.Invoke(Direction);
        }

        if (Direction>0)
        {
            MovedLeft?.Invoke(Direction);
        }
    }
}
