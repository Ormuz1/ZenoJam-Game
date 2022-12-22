using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public float Left {get => transform.localPosition.x - Size / 2;}
    public float Right {get => transform.localPosition.x + Size / 2;}
    public float Size { get => spriteRenderer.bounds.size.x; }
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform positionToSpawnEnemies;
    [SerializeField] private GameObject enemyToSpawn;
    private GameObject spawnedEnemy;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SpawnObstacle()
    {
        if (!(enemyToSpawn == null))
            spawnedEnemy = Instantiate(enemyToSpawn, positionToSpawnEnemies.position, Quaternion.identity);
    }

    public void DespawnEnemy()
    {
        if(!(spawnedEnemy == null))
        {
            spawnedEnemy.SetActive(false);
            Destroy(spawnedEnemy);
        }
    }
}
