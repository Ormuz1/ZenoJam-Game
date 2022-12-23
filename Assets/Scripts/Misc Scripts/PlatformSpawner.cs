using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 maxGapBetweenPlatforms;
    [SerializeField] private Vector2 minGapBetweenPlatforms;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private Platform[] platformPrefabs;
    [SerializeField] private int platformsBetweenLevelSegments = 10;
    [SerializeField] private GameObject[] levelSegments;
    private Platform lastPlatformSpawned;
    private enum Side {Left, Right};
    private Transform player;
    private float distanceUntilNextGeneration = 10f;
    private bool shouldGenerateLevelSegment = false;
    private int lastLevelSegmentGenerated;
    private Queue<GameObject> generatedStuff = new Queue<GameObject>();
    
    private void Start() {
        GameObject playerGO = GameObject.FindWithTag("Player") ?? throw new UnassignedReferenceException("There needs to be an object with the Player tag.");
        player = playerGO.transform;
        lastPlatformSpawned = transform.GetChild(0).GetComponent<Platform>();
    }


    private void Update()
    {
        if(Mathf.Abs(player.position.y - lastPlatformSpawned.transform.position.y) > distanceUntilNextGeneration)
            return;
        
        if(shouldGenerateLevelSegment)
            SpawnLevelSegment();
        else
            SpawnPlatforms();

        shouldGenerateLevelSegment = !shouldGenerateLevelSegment;
        if(generatedStuff.Count > 10)
        {
            Destroy(generatedStuff.Dequeue());
        }
    }

    private void SpawnLevelSegment()
    {
        Vector3 segmentSpawnPosition = lastPlatformSpawned.transform.position;
        segmentSpawnPosition.y += Random.Range(minGapBetweenPlatforms.y, maxGapBetweenPlatforms.y);
        segmentSpawnPosition.x = transform.position.x;
        int chosenSegment = Random.Range(0, levelSegments.Length);
        while(chosenSegment == lastLevelSegmentGenerated)
        {
            chosenSegment = Random.Range(0, levelSegments.Length);
        }
        lastLevelSegmentGenerated = chosenSegment;
        GameObject spawnedSegment = Instantiate(levelSegments[chosenSegment], segmentSpawnPosition, Quaternion.identity, transform);
        lastPlatformSpawned = spawnedSegment.transform.GetChild(0).GetChild(spawnedSegment.transform.GetChild(0).childCount - 1).GetComponent<Platform>();
        generatedStuff.Enqueue(spawnedSegment);
    }

    private void SpawnPlatforms()
    {
        for(int i = 0; i < platformsBetweenLevelSegments; i++)
        {
            GenerateNewPlatform();
        }
    }
    private void GenerateNewPlatform()
    {
        Vector3 newPlatformPosition = new Vector2();

        float minX, maxX;
        Side chosenSide = ChooseSideToMakeNewPlatform();
        Vector3 lastPlatformPosition = transform.InverseTransformPoint(lastPlatformSpawned.transform.position);
        if(chosenSide == Side.Left)
        {
            minX = lastPlatformPosition.x - lastPlatformSpawned.Size / 2 - maxGapBetweenPlatforms.x;
            maxX = lastPlatformPosition.x - lastPlatformSpawned.Size / 2 - minGapBetweenPlatforms.x;
            if (minX < 0)
                minX = 0 + lastPlatformSpawned.Size / 2;
        }
        else
        {
            minX = lastPlatformPosition.x + lastPlatformSpawned.Size / 2 + minGapBetweenPlatforms.x;
            maxX = lastPlatformPosition.x + lastPlatformSpawned.Size / 2 + maxGapBetweenPlatforms.x;
            if (maxX > maxSpawnDistance)
                maxX = maxSpawnDistance - lastPlatformSpawned.Size / 2;
        }
        newPlatformPosition.x = Random.Range(minX, maxX);
        newPlatformPosition.y = lastPlatformPosition.y + Random.Range(minGapBetweenPlatforms.y, maxGapBetweenPlatforms.y);
        
        Platform newPlatform;

        newPlatform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)], transform);            

        newPlatform.transform.localPosition = newPlatformPosition;
        newPlatform.SpawnObstacle();
        generatedStuff.Enqueue(newPlatform.gameObject);
        lastPlatformSpawned = newPlatform;
    }


    private Side ChooseSideToMakeNewPlatform()
    {
        if(!((lastPlatformSpawned.Left - minGapBetweenPlatforms.x - lastPlatformSpawned.Size / 2) > 0))
            return Side.Right;
        
        if(!((lastPlatformSpawned.Right + minGapBetweenPlatforms.x + lastPlatformSpawned.Size / 2) < maxSpawnDistance))
            return Side.Left;

        return Random.value < 0.5f ? Side.Left : Side.Right;  
    }


    private void OnDrawGizmos() {
        Vector3 linePosition = transform.position + Vector3.right * maxSpawnDistance;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 4);
        Gizmos.DrawLine(linePosition, linePosition + Vector3.up * 4);
    }
}
