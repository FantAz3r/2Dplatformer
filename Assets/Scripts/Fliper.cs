using UnityEngine;

public class Fliper : MonoBehaviour
{
    public void Flip(float direction)
    {
        if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
