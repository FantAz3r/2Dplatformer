using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    public float direction { get; private set; }

    [SerializeField] private KeyCode JumpKey = KeyCode.Space;

    public event Action Jumped;
    public event Action<float> MovedRight;
    public event Action<float> MovedLeft;

    private void Update()
    {
        direction = Input.GetAxis(Horizontal); 

        if (Input.GetKeyDown(JumpKey))
        {
            Jumped?.Invoke();
        }

        if (direction < 0)
        {
            MovedRight?.Invoke(direction);
        }

        if (direction>0)
        {
            MovedLeft?.Invoke(direction);
        }
    }
}
