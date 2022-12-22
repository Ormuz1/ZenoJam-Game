using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platform : MonoBehaviour
{
    public float Left {get => transform.localPosition.x - Size / 2;}
    public float Right {get => transform.localPosition.x + Size / 2;}
    public float Size { get => spriteRenderer.bounds.size.x; }
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Transform[] enemySpawnPositions;
    [SerializeField] private GameObject enemyToSpawn;
    public GameObject spawnedEnemy;
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SpawnObstacle()
    {
        if (!(enemyToSpawn == null))
        {
            spawnedEnemy = Instantiate(enemyToSpawn, enemySpawnPositions[Random.Range(0, enemySpawnPositions.Length - 1)].position, Quaternion.identity);
            spawnedEnemy.transform.SetParent(transform, true);
        }
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
