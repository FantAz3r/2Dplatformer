using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private KeyCode JumpKey = KeyCode.Space;

    private int leftMouseButton = 0;

    public event Action Jumped;
    public event Action<float> MovedRight;
    public event Action<float> MovedLeft;
    public event Action<Vector2> MouseButtonPushed;
    public event Action<float> MouseMoved;
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

        if (Input.GetMouseButtonDown(leftMouseButton))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseButtonPushed?.Invoke(mousePosition);
        }

        if (Input.mousePosition != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MouseMoved?.Invoke(mousePosition.x);
        }
    }
}
