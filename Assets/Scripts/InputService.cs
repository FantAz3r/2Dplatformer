using System;
using UnityEngine;

public class InputService : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [SerializeField] private KeyCode JumpKey = KeyCode.Space;

    public event Action Jump;
    public event Action<float> MoveRight;
    public event Action<float> MoveLeft;

    private void Update()
    {
        float direction = Input.GetAxis(Horizontal); 

        if (Input.GetKeyDown(JumpKey))
        {
            Jump?.Invoke();
        }

        if (direction < 0)
        {
            MoveRight?.Invoke(direction);
        }

        if (direction>0)
        {
            MoveLeft?.Invoke(direction);
        }
    }
}
