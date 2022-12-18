using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Vector2 maxGapBetweenPlatforms;
    [SerializeField] private Vector2 minGapBetweenPlatforms;
    [SerializeField] private float maxSpawnDistance;
    [SerializeField] private Platform platformPrefab;
    private Platform lastPlatformSpawned;
    private enum Side {Left, Right};
    private void Awake() {
        lastPlatformSpawned = transform.GetChild(0).GetComponent<Platform>();
        for(int i = 0; i < 30; i++)
        {
            GenerateNewPlatform();
        }
    }

    private void GenerateNewPlatform()
    {
        Vector3 newPlatformPosition = new Vector2();

        float minX, maxX;
        Side chosenSide = chooseSideToMakeNewPlatform();
        if(chosenSide == Side.Left)
        {
            minX = lastPlatformSpawned.transform.localPosition.x - lastPlatformSpawned.Size / 2 - maxGapBetweenPlatforms.x;
            maxX = lastPlatformSpawned.transform.localPosition.x - lastPlatformSpawned.Size / 2 - minGapBetweenPlatforms.x;
            if (minX < 0)
                minX = 0 + lastPlatformSpawned.Size / 2;
        }
        else
        {
            minX = lastPlatformSpawned.transform.localPosition.x + lastPlatformSpawned.Size / 2 + minGapBetweenPlatforms.x;
            maxX = lastPlatformSpawned.transform.localPosition.x + lastPlatformSpawned.Size / 2 + maxGapBetweenPlatforms.x;
            if (maxX > maxSpawnDistance)
                maxX = maxSpawnDistance - lastPlatformSpawned.Size / 2;
        }
        newPlatformPosition.x = Random.Range(minX, maxX);
        newPlatformPosition.y = lastPlatformSpawned.transform.localPosition.y + Random.Range(minGapBetweenPlatforms.y, maxGapBetweenPlatforms.y);
        
        Debug.Log($"Last platform position: {lastPlatformSpawned.transform.localPosition.x} - New platform position: {newPlatformPosition.x}");
        Platform newPlatform = Instantiate(platformPrefab, transform);
        newPlatform.transform.localPosition = newPlatformPosition;
        lastPlatformSpawned = newPlatform;
    }


    private Side chooseSideToMakeNewPlatform()
    {
        if(!((lastPlatformSpawned.Left - minGapBetweenPlatforms.x - lastPlatformSpawned.Size / 2)> 0))
            return Side.Right;
        
        // (lastPlatformSpawned.Right + minGapBetweenPlatforms.x) < maxSpawnDistance
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
