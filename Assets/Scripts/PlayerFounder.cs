using UnityEngine;

public class PlayerFounder : MonoBehaviour
{
    [SerializeField] private float _searchRadius = 10f;

    public Transform GetTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _searchRadius);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Character character))
            {
                return character.transform;
            }
        }

        return null;
    }
}

