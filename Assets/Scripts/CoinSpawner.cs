using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private int _coinsAmount;
    [SerializeField] private float _ditanceToGround;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Coin _coinPrefab;

    private int _positionX;
    private int _positionY;

    private void Start()
    {
        Create();
    }

    private void Create()
    {
        for (int i = 0; i < _coinsAmount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            if (spawnPosition != Vector2.zero)
            {
                Instantiate(_coinPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomSpawnPosition()
    {
        bool _isGetSpawnPosition = false;
        Vector2 spawnPosition;
        float groundCheckerRadius = 3f;
        float maxDistanceToGround = 7f;

        while (_isGetSpawnPosition == false)
        {
            _positionX = (int)Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
            _positionY = (int)Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.x + transform.localScale.y / 2);

            spawnPosition = new Vector2(_positionX, _positionY);

            if (Physics2D.Raycast(spawnPosition, Vector2.down, maxDistanceToGround, groundLayer))
            {
                if (Physics2D.OverlapCircle(spawnPosition, groundCheckerRadius, groundLayer) == false) 
                {
                    _isGetSpawnPosition = true;
                    return spawnPosition; 
                }
            }
        }

        return Vector2.zero;
    }
}
